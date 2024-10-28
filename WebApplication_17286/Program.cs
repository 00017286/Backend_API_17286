using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WebApplication_17286.DBContexts;
using WebApplication_17286.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Register MusicContext as a service and configure it to use SQL Server
builder.Services.AddDbContext<MusicContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString("MusicDB")));

// Register ISongRepository and IPerformerRepository with their concrete implementation
// Transient means new repository instance will be created each time it's requested
builder.Services.AddTransient<ISongRepository, SongRepository>();
builder.Services.AddTransient<IPerformerRepository, PerformerRepository>();

// Add Swagger service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Turn on Swagger in Development env
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Urls.Add("http://*:44312");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
