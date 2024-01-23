using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Reflection;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            builder.Services.AddDbContext<ExoticPlacesContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionString"]));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
           {
               options.SwaggerDoc("v1", new OpenApiInfo
               {
                   Version = "v1",
                   Title = "Интрернет-магазин API",
                   Description = "...",
                   Contact = new OpenApiContact
                   {
                       Name = "Контакты",
                       Url = new Uri("https://example.com/contact")
                   },
                   License = new OpenApiLicense
                   {
                       Name = "Лицензия",
                       Url = new Uri("https://example.com/license")
                   }
               });
               var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
           });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}