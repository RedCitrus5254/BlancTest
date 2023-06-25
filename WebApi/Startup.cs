using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.BusinessLogic.RequestHandlers;
using WebApi.Storage;

namespace WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterHandlers(services);

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void RegisterHandlers(IServiceCollection services)
        {
            var postgresConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:postgres");

            // Хранилища
            var todoItemRepository = new TodoItemRepository(
                postgresConnectionString!);

            // Обработчики запросов
            services.AddSingleton<AddTodoItemRequestHandler>(new AddTodoItemRequestHandler(
                todoItemRepository: todoItemRepository));
            services.AddSingleton<UpdateTodoItemRequestHandler>(new UpdateTodoItemRequestHandler(
                todoItemRepository: todoItemRepository));
        }
    }
}