using AppPortal.Data;
using AppPortal.Data.Interfaces;
using AppPortal.Data.Services;
using ManagementPortal.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("CanViewAll", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin, Assigner, Reviewer, FormCreator"));
    options.AddPolicy("CanAssign", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin, Assigner"));
    options.AddPolicy("CanTransition", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin, Reviewer"));
    options.AddPolicy("CanCrud", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin, Creator"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddScoped<DataContext>();
builder.Services.AddTransient<MapperService>();
builder.Services.AddTransient<ICycleService, CycleService>();
builder.Services.AddTransient<IFormService, FormService>();
builder.Services.AddTransient<IFormQuestionService, FormQuestionService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IApplicationService, ApplicationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cycle}/{action=Dashboard}/{id?}");
app.MapRazorPages();

app.Run();
