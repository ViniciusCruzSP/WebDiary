using DiaryApp.Filters;
using DiaryApp.Services;
using DiaryApp.Infrastructure.Http;

var builder = WebApplication.CreateBuilder(args);

var apiBaseUrl = builder.Configuration["ApiSettings:DiaryApiBaseUrl"];

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddHttpClient("DiaryApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped<DiaryApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DiaryEntries}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();