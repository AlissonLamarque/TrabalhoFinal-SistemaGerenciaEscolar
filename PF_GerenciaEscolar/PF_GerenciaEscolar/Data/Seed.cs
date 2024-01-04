using Microsoft.AspNetCore.Identity;
using PF_GerenciaEscolar.Models;
using System.Diagnostics;
using System.Net;

namespace PF_GerenciaEscolar.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<PF_GerenciaEscolarDbContext>();

                context.Database.EnsureCreated();

                if (!context.Administradores.Any())
                {
                    context.Administradores.AddRange(new List<Administrador>()
                    {
                        new Administrador()
                        {
                            Nome = "André",
                            Email = "andre@gmail.com",
                            Autenticacao = new Autenticacao()
                            {
                                Cpf = "000.000.000-00",
                                Senha = "senha0",
                                Cargo = Enum.Cargo.Administrador
                            }
                         },
                    });
                    context.SaveChanges();
                }
                
                if (!context.Professores.Any())
                {
                    context.Professores.AddRange(new List<Professor>()
                    {
                        new Professor()
                        {
                            Nome = "Bruno",
                            Email = "bruno@gmail.com",
                            Autenticacao= new Autenticacao()
                            {
                                Cpf = "111.111.111-11",
                                Senha = "senha1",
                                Cargo = Enum.Cargo.Professor
                            },
                            Disciplina = Enum.Disciplina.Matematica
                        },
                    });
                    context.SaveChanges();
                }

                if (!context.Alunos.Any())
                {
                    context.Alunos.AddRange(new List<Aluno>()
                    {
                        new Aluno()
                        {
                            Nome = "Carlos",
                            Email = "carlos@gmail.com",
                            Autenticacao = new Autenticacao
                            {
                                Cpf = "222.222.222-22",
                                Senha = "senha2",
                                Cargo = Enum.Cargo.Aluno
                            },
                            Turma = Enum.Turma.T160
                        },
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Cargos
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Administrador))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Administrador));
                if (!await roleManager.RoleExistsAsync(UserRoles.Professor))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Professor));
                if (!await roleManager.RoleExistsAsync(UserRoles.Aluno))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Aluno));

                //Usuários
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Autenticacao>>();
                string administradorEmail = "alissonlmq@gmail.com";

                var administrador = await userManager.FindByEmailAsync(administradorEmail);
                if (administrador == null)
                {
                    var newAdministrador = new Autenticacao()
                    {
                        Email = administradorEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdministrador, "senhaAdministrador");
                    await userManager.AddToRoleAsync(newAdministrador, UserRoles.Administrador);
                }

                string professorEmail = "jorge@gmail.com";

                var professor = await userManager.FindByEmailAsync(professorEmail);
                if (professor == null)
                {
                    var newProfessor = new Autenticacao()
                    {
                        Email = professorEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newProfessor, "senhaProfessor");
                    await userManager.AddToRoleAsync(newProfessor, UserRoles.Professor);
                }

                string alunoEmail = "pedro@gmail.com";

                var aluno = await userManager.FindByEmailAsync(alunoEmail);
                if (aluno == null)
                {
                    var newAluno = new Autenticacao()
                    {
                        Email = alunoEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAluno, "senhaProfessor");
                    await userManager.AddToRoleAsync(newAluno, UserRoles.Aluno);
                }
            }
        }
    }
}
