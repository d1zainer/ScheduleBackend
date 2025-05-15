using Microsoft.OpenApi.Models;
using ScheduleBackend.Models;
using System.Reflection;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Repositories.Json;
using Swashbuckle.AspNetCore.Filters;
using ScheduleBackend.Services.Entity;

namespace ScheduleBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ITeacherRepository, JsonTeacherRepository>();
            builder.Services.AddScoped<ITeacherScheduleRepository, JsonTeacherScheduleRepository>();
            builder.Services.AddScoped<IScheduleRepository, JsonScheduleRepository>();
            builder.Services.AddScoped<IUserRepository, JsonUserRepository>();


            builder.Services.AddScoped<ScheduleService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<TeachersService>();
            builder.Services.AddScoped<TeacherScheduleService>();

            // ����������� CORS ��������
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // ��������� ������� � ����� ����������
                        .AllowAnyMethod()  // ��������� ��� HTTP-������
                        .AllowAnyHeader();  // ��������� ��� ���������
                });
            });

            // ���������� �������� ��� ������������ � Swagger
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

            // ���������� CORS ��������
            app.UseCors("AllowAllOrigins");

            // ��������� Swagger UI ������ ��� Development �����
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ���������� �����������, ���� �����
            // app.UseAuthorization(); // �������� ������������������, ���� ����������� �� ���������

            // ����� ������������
            app.MapControllers();

            // ������ ����������
            app.Run();
        }
    }
}