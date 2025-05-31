using Project.BLL.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextService();
builder.Services.AddIdentityService();
builder.Services.AddRepositoryService();
builder.Services.AddManagerService();
builder.Services.AddHttpClient(); //Eger bir API consume edilecekse HTTP client tarafında oldugumuz ifadesini Middleware'e bildirmeliyiz...

builder.Services.AddDistributedMemoryCache(); //Eger Session kompleks yapılarla calısmak icin Extension metodu ekleme durumuna maruz kalacaksa bu kod projenizin o ilgili Session alanını saglıklı calıstırabilmesi icin gereklidir

builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromDays(1);
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); //wwwroot'a direkt ulasmanızı saglayan metottur...

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area}/{controller=Category}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Register}/{id?}");

app.Run();
