using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using ScheduleBackend.Services;
using System.Reflection;

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
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

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