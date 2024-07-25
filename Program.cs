
using EmprestimosAPI.Data;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Interfaces.Services;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using EmprestimosAPI.Repositories;
using EmprestimosAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Microsoft.IdentityModel.Tokens;
using System.Runtime;
using System.Text;
using EmprestimosAPI.Interfaces.Account;
using EmprestimosAPI.Token;
using EmprestimosAPI.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace EmprestimosAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IAssociacaoRepository, AssociacaoRepository>();
            builder.Services.AddScoped<IAssociacaoService, AssociacaoService>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
            builder.Services.AddScoped<IPessoaService, PessoaService>();
            builder.Services.AddScoped<ILocalRepository, LocalRepository>();
            builder.Services.AddScoped<ILocalService, LocalService>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
            builder.Services.AddScoped<IEquipamentoService, EquipamentoService>();
            builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();
            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IFeedbackService,  FeedbackService>();
            builder.Services.AddScoped<HashingService>();
            builder.Services.AddScoped<IAuthenticate, AuthenticateService>();
            builder.Services.AddCors(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("https://wheelshare.up.railway.app", "http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
                }
                else
                {
                    options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("https://wheelshare.up.railway.app")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
                }
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructureSwagger();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                ?? builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DbEmprestimosContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["jwt:issuer"],
                    ValidAudience = builder.Configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            }
            );

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AssociacaoPolicy", policy => policy.RequireRole("Associacao"));
                options.AddPolicy("UsuarioPolicy", policy => policy.RequireRole("Usuario"));
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
