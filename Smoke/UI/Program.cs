using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using UI.Auth;
using UI.Components;
using UI.Contracts;
using UI.Middlewares;
using UI.Services;
using UI.Auth;
using UI.Components;
using UI.Services.Base;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddBlazoredLocalStorage();

//Todo: split this whole registration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddSingleton(new HttpClient
{
    BaseAddress = new Uri("https://localhost:7090")
});


builder.Services.AddTransient<CustomAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IClient, Client>(client => new Client("https://localhost:7051", new HttpClient()
{
  //  BaseAddress = new Uri("https://localhost:7051")
}))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

//Hax:{
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();
//}

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IStateChangeService, StateChangeService>();
builder.Services.AddScoped<IChangeNotificationService, ChangeNotificationService>();

builder.Services.AddScoped<IApiRequestDataService, ApiRequestDataService>();

builder.Services.AddOptions();
builder.Services.AddBlazoredToast();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorComponents<App>().
    AddInteractiveServerRenderMode();

app.Run();
