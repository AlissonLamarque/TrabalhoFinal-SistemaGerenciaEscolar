
using Microsoft.AspNetCore.Identity;

namespace PF_GerenciaEscolar.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Aluno"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Aluno";
                role.NormalizedName = "ALUNO";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Professor"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Professor";
                role.NormalizedName = "PROFESSOR";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUsersAsync()
        {
            if (await _userManager.FindByEmailAsync("aluno@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "aluno@localhost";
                user.Email = "aluno@localhost";
                user.NormalizedUserName = "ALUNO@LOCALHOST";
                user.NormalizedEmail = "ALUNO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;

                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Senha_123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Aluno");
                }
            }

            if (await _userManager.FindByEmailAsync("admin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMINO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;

                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Senha_123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if (await _userManager.FindByEmailAsync("professor@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "professor@localhost";
                user.Email = "professor@localhost";
                user.NormalizedUserName = "PROFESSOR@LOCALHOST";
                user.NormalizedEmail = "PROFESSOR@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;

                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Senha_123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Professor");
                }
            }
        }
    }
}
