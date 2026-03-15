using BL.AutoMapper;
using BL.Managers.CompanyAuditLogManager;
using BL.Managers.CompanyDetManager;
using BL.Managers.UserManager;
using BL.Managers.DrugDataManager;
using DTO.Data.Cnotext;
using DTO.Repositories.CompanyAuditLogRepository;
using DTO.Repositories.CompanyDetRepository;
using DTO.Repositories.RoleRepository;
using DTO.Repositories.UserRepository;
using DTO.Repositories.UserRoleRepository;
using DTO.Repositories.DrugDataRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Advanced_Company_Management_System__MVC_.Filters;
using Advanced_Company_Management_System__MVC_.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container with Global Exception Filter
builder.Services.AddControllersWithViews(options =>
{
    // Add global exception filter
    options.Filters.Add<GlobalExceptionFilter>();
});

#region Dependency Injection
builder.Services.AddScoped<ICompanyDetRepository ,CompanyDetRepository>();
builder.Services.AddScoped<ICompanyDetManager ,CompanyDetManager>();

builder.Services.AddScoped<ICompanyAuditLogRepository ,CompanyAuditLogRepository>();
builder.Services.AddScoped<ICompanyAuditLogManager ,CompanyAuditLogManager>();

builder.Services.AddScoped<IUserRepository ,UserRepository>();
builder.Services.AddScoped<IUserManager ,UserManager>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

// Drug Data Services (GetDrugsData View)
builder.Services.AddScoped<IDrugDataRepository, DrugDataRepository>();
builder.Services.AddScoped<IDrugDataManager, DrugDataManager>();

#endregion

builder.Services.AddDbContext<EddbAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#region AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
#endregion


#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use custom error handling for production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // In development, use developer exception page for detailed errors
    app.UseDeveloperExceptionPage();
}

// Add status code pages for common HTTP errors
app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

#endregion

// Add request logging middleware
app.UseRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LoginForm}/{id?}");

app.Run();
