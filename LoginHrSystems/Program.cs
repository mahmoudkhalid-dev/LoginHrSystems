using LoginHrSystems.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoginHrSystems
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
                            });

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
