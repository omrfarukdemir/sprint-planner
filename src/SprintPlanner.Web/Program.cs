var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddControllersWithViews();

var app = builder.Build();

await app.InitialiseDatabaseAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sprint}/{action=Index}/{id?}");

app.Run();