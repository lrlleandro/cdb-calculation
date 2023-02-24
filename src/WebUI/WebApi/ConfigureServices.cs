using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WebApi.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddInfrastructureServices();
            services.AddApplicationServices();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
              .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
               });

            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost4200",
                    builder =>
                    {
                        //builder.WithOrigins("http://localhost:4200")
                        //       .AllowAnyHeader()
                        //       .AllowAnyMethod();

                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            return services;
        }

        public static WebApplication UseWebApiConfigurations(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("AllowLocalhost4200");

            return app;
        }
    }
}
