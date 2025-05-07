using LoginHrSystems.Data;
using LoginHrSystems.Helpers;
using LoginHrSystems.Repositories.Contract;
using LoginHrSystems.Repositories.Implementation;
using LoginHrSystems.Repositories.UnitOfWork;
using LoginHrSystems.Services.Contract;
using LoginHrSystems.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
);

builder.Services.AddDbContext<ApplicationDbContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    }
);

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("Permission:AddEmployee", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "permission" && c.Value == "AddEmployee")));

    options.AddPolicy("Permission:UpdateEmployee", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "permission" && c.Value == "UpdateEmployee")));

    options.AddPolicy("Permission:DeleteEmployee", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "permission" && c.Value == "DeleteEmployee")));

    options.AddPolicy("Permission:GetAllEmployees", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "permission" && c.Value == "GetAllEmployees")));

    options.AddPolicy("Permission:GetEmployeeDetails", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "permission" && c.Value == "GetEmployeeDetails")));

});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigration>();


builder.Services.AddTransient<JwtHelper>();

builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
    await SeedsHelper.Seed(applicationDbContext);
}

app.ConfigureSwagger();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
