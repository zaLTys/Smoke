using Application.Abstractions.Services;
using Domain.Abstractions.Repositories;
using Infrastructure.Repositories.InMemory;
using Microsoft.OpenApi.Models;
using Web.Middleware;

namespace WebApi;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

        services.AddControllers()
            .AddApplicationPart(presentationAssembly);

        var applicationAssembly = typeof(Application.AssemblyReference).Assembly;

        var mediatrConfig = new MediatRServiceConfiguration();
        mediatrConfig.RegisterServicesFromAssembly(applicationAssembly);

        services.AddMediatR(mediatrConfig);

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        //services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddSwaggerGen(c =>
        {
            var presentationDocumentationFile = $"{presentationAssembly.GetName().Name}.xml";

            var presentationDocumentationFilePath =
                Path.Combine(AppContext.BaseDirectory, presentationDocumentationFile);

            c.IncludeXmlComments(presentationDocumentationFilePath);

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smoke", Version = "v1" });
        });

        services.AddSingleton<IRequestRepository, InMemoryRequestRepository>();
        services.AddSingleton<IScenarioRepository, InMemoryScenarioRepository>();

        services.AddHttpClient();
        services.AddScoped<IHttpRequestService, HttpRequestService>();
        services.AddScoped<ICurlParserService, CurlParserService>();
        services.AddScoped<IExecutionService, ExecutionService>();

        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<CurlStringEscapeMiddleware>();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}