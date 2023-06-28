using System.IO;
using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.BusinessLogic.RequestHandlers;
using WebApi.Queue;
using WebApi.Storage;
using WebApi.Storage.Contracts.Repositories;

namespace WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.SetInMemorySagaRepositoryProvider();

                var assembly = Assembly.GetEntryAssembly();

                x.AddConsumer<UpdateTodoITemMessageConsumer>();
                x.AddSagaStateMachines(assembly);
                x.AddSagas(assembly);
                x.AddActivities(assembly);

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            RegisterHandlers(services, configuration);

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

        private void RegisterHandlers(IServiceCollection services, IConfigurationRoot configuration)
        {
            var postgresConnectionString = configuration.GetValue<string>("ConnectionStrings:postgres");

            // Хранилища
            var todoItemRepository = new TodoItemRepository(
                postgresConnectionString!);
            services.AddSingleton<ITodoItemRepository>(todoItemRepository);

            // Обработчики запросов
            services.AddSingleton<AddTodoItemRequestHandler>();
            services.AddSingleton<UpdateTodoItemRequestHandler>();
            services.AddSingleton<GetTodoItemRequestHandler>();
            services.AddSingleton<GetTodoItemListRequestHandler>();
        }
    }
}