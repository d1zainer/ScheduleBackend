using Microsoft.OpenApi.Models;
using ScheduleBackend.Models;
using ScheduleBackend.Services;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;

namespace ScheduleBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ScheduleService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<TeachersService>();
            builder.Services.AddSingleton<LessonService>();

            // Регистрация CORS политики
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // Разрешить запросы с любых источников
                        .AllowAnyMethod()  // Разрешить все HTTP-методы
                        .AllowAnyHeader();  // Разрешить все заголовки
                });
            });

            // Добавление сервисов для контроллеров и Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "QR API",
                });
                c.ExampleFilters();
                
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddSwaggerExamplesFromAssemblyOf<CourseCheckOkResponseExample>();
            builder.Services.AddSwaggerExamplesFromAssemblyOf<CourseCheckErrorResponseExample>();

            var app = builder.Build();

            // Применение CORS политики
            app.UseCors("AllowAllOrigins");

            // Настройка Swagger UI только для Development среды
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Применение авторизации, если нужно
            // app.UseAuthorization(); // Оставьте закомментированным, если авторизация не требуется

            // Карты контроллеров
            app.MapControllers();

            // Запуск приложения
            app.Run();
        }
    }
}