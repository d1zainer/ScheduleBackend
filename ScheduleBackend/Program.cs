using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ScheduleBackend.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using ScheduleBackend.Services.Entity;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScheduleBackend.Db;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Repositories;
using ScheduleBackend.Services.Auth;
using ScheduleBackend.Models.Settings;
using ScheduleBackend.Services.Interfaces;
using ScheduleBackend.Services.Messages;

namespace ScheduleBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);

            // Регистрация DbContext
            builder.Services.AddDbContext<ScheduleDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                        .EnableSensitiveDataLogging()        // чтобы видеть параметры
                        .EnableDetailedErrors()              // более детальные ошибки
            );

            var rabbitSettings = builder.Configuration
                .GetSection("RabbitMQ") ?? throw new InvalidOperationException("RabbitMQ settings not found");

            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            builder.Services.Configure<RabbitMqSettings>(rabbitSettings);
            if (jwtSettings == null) throw new Exception("JwtSettings section is missing in configuration.");
            
            // Регистрируемрепозитории
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<ITeacherScheduleRepository, TeacherScheduleRepository>();
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<INotificationSender, NotificationSender>();
            builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // DbContext
            builder.Services.AddDbContext<ScheduleDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Сервисы
            builder.Services.AddScoped<ScheduleService>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<TeachersService>();
            builder.Services.AddScoped<TeacherScheduleService>();
            builder.Services.AddScoped<AdminService>();
            builder.Services.AddScoped<RegistrationService>();
            

            // Параметры валидации токена
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),

                ValidateIssuerSigningKey = true,

                ClockSkew = TimeSpan.Zero
            };

            builder.Services.AddSingleton(tokenValidationParameters);

            // Регистрируем JwtService (если есть интерфейс IJwtService, лучше использовать его)
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddAuthorization();
              builder.Services.AddScoped<AuthService>();
            // Добавляем аутентификацию и настраиваем JWT Bearer
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });
            builder.Services.AddScoped<AuthService>();
            // CORS политика
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Контроллеры и Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QR API" });

                // Поддержка JWT в Swagger UI
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] {}
                    }
                });

                c.ExampleFilters();

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddSwaggerExamplesFromAssemblyOf<CourseCheckOkResponseExample>();
            builder.Services.AddSwaggerExamplesFromAssemblyOf<CourseCheckErrorResponseExample>();

            var app = builder.Build();

            app.UseCors("AllowAllOrigins");

            // Обязательно включаем аутентификацию и авторизацию
            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
