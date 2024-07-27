using Microsoft.AspNetCore.Identity;
using DisneyMoviesWatchlist.Src.DatabaseContext;
using DisneyMoviesWatchlist.Src.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DisneyMoviesDbContextConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DisneyMoviesDbContextConnection' not found.");

/*
// Add services to the container.
builder.Services.AddDbContext<DisneyMoviesDbContext>( options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DisneyMoviesDB"))
);*/

builder.Services.AddDbContext<DisneyMoviesDbContext>( options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => options.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<DisneyMoviesDbContext>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddRazorPages();

//Registering Google.
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = 
            builder.Configuration.GetSection("GoogleAuthSettings").GetValue<string>("ClientId");
        googleOptions.ClientSecret = 
            builder.Configuration.GetSection("GoogleAuthSettings").GetValue<string>("ClientSecret");
    });

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
