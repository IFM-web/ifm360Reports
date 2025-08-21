var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
 
    options.IdleTimeout = TimeSpan.FromMinutes(240);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
   
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
