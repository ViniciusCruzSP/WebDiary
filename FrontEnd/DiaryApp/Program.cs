using DiaryApp.Filters;
using DiaryApp.Services;

var builder = WebApplication.CreateBuilder(args);
var apiBaseUrl = builder.Configuration["ApiSettings:DiaryApiBaseUrl"];

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});
builder.Services.AddHttpClient("DiaryApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});
builder.Services.AddScoped<DiaryApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
