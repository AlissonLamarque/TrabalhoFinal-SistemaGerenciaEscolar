using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;
using PF_GerenciaEscolar.Repositorio;
using static PF_GerenciaEscolar.Data.PF_GerenciaEscolarDbContext;

namespace PF_GerenciaEscolar
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IAdministradorRepositorio, AdministradorRepositorio>();
            builder.Services.AddScoped<IProfessorRepositorio, ProfessorRepositorio>();
            builder.Services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
            builder.Services.AddScoped<IAvaliacaoRepositorio, AvaliacaoRepositorio>();

            builder.Services.AddDbContext<PF_GerenciaEscolarDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddIdentity<Autenticacao, IdentityRole>()
            //    .AddEntityFrameworkStores<PF_GerenciaEscolarDbContext>();
            //builder.Services.AddMemoryCache();
            //builder.Services.AddSession();
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

            var app = builder.Build();

            //if (args.Length == 1 && args[0].ToLower() == "seeddata")
            //{
            //   await Seed.SeedUsersAndRolesAsync(app);
            //  Seed.SeedData(app);
            //}

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
