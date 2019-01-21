using Sistema_Advocacia.Models;
//using estoque00.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Advocacia.ViewModels;

namespace Sistema_Advocacia.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();
            foreach (var user in users)
            {
                var userView = new UserView { Email = user.Email, UserId = user.Id, Nome = user.UserName };
                usersView.Add(userView);
            }

            return View(usersView);
        }

        public ActionResult Roles(string userId)
        {
            //saber qual usuario eu quero editar. //obter lista de usuarios
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            //variavel pegará o usuarios userId
            var user = users.Find(x => x.Id == userId);

            //obter lista de roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                //User.Roles é a lista de roles apenas do usuario. Portanto o que tenho em user.Roles é apenas o RoleId. //Mas quero o nome também.  //Então vou na tabela Roles pra buscar pelo user.RoleId esse nome e RoleId e jogar na variavel var RoleView
                var role = roles.Find(x => x.Id == item.RoleId);
                var roleView = new RoleView { Name = role.Name, RoleId = role.Id };

                //o roleView agora tem os atributos do Role atual (var item in user.Role). Agora é só jogar em na lista rolesView
                rolesView.Add(roleView);
            }

            var userView = new UserView { Email = user.Email, Nome = user.UserName, UserId = user.Id, Roles = rolesView };
            return View(userView);
        }


        public ActionResult AddRole(string UserId)
        {
            //capturar informações do usuario (string userId)
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == UserId);
            var userView = new UserView { Email = user.Email, Nome = user.UserName, UserId = user.Id };

            //obter lista de roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();

            //colocar essa lista na combobox
            //list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente!]" });
            //Colocar nela selecione um registro
            list.Add(new IdentityRole { Id = "", Name = "[Selecione uma permissão!]" });
            //ordenar
            list = list.OrderBy(c => c.Name).ToList();
            //por a lista na viewBag
            ViewBag.RoleId = new SelectList(list, "Id", "Name");
            return View(userView);
        }





        //formColetcion: coleção de parametros
        [HttpPost]
        public ActionResult AddRole(string userId, FormCollection form)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userId);
            var userView = new UserView { Email = user.Email, Nome = user.UserName, UserId = user.Id };



            //guarda informações que vem via metodo post. Se seleciono algo no combo eu seleciono um RoleId que vem pra cá via post
            var roleId = Request["RoleId"];

            //se o roleId que vier via post for vazio, é que vc não selecionou nada na combo permissões
            if (string.IsNullOrEmpty(roleId))
            {
                //limpar a lista                
                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Selecione uma permissão!]" });
                list = list.OrderBy(c => c.Name).ToList();
                ViewBag.RoleId = new SelectList(list, "Id", "Name");

                //gera um ViewBag com erro, colocado na View
                ViewBag.Error = "Você precisa selecionar uma permissão.";
                return View(userView);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(x => x.Id == roleId);
            if (!userManager.IsInRole(userId, role.Name))
            {
                userManager.AddToRole(userId, role.Name);
            }

            //Agora que adicionou a role, recrie a visão, tanto de usuario quanto de roles. 
            //var que guardará as roles do meu usuario no for logo abaixo
            var rolesViews = new List<RoleView>();

            //pegue o codigo de cada User.Rolers (porque elas não são roleViews que ai sim são recebida em uma userView). 
            //Resumo: Pegue uma role que (não é viewTipada e não pode ser vista).Isole e jogue e uma Viewrole (ViwTipada). Adicione em uma Lista de ViewRoles
            foreach (var item in user.Roles)
            {
                //isole o item role do user.Role (ele não é um model ou ViewModel, portanto não poderá ser visto em uma ViewTipada. 
                role = roles.Find(x => x.Id == item.RoleId); //erro: comparar com = e não com "=="
                //roleView sim podem ser vistas porque é ViewModel. Pegue os dados da role acima e jogue numa roleView
                var roleView = new RoleView { RoleId = role.Id, Name = role.Name };
                rolesViews.Add(roleView);
            }

            /*
            var roles2 = new List<RoleView>();
            foreach (var item in user.Roles)
            {
                role = roles.Find(x => x.Id == item.RoleId);
                var roleView2 = new RoleView { RoleId = role.Id, Name = role.Name };
                roles2.Add(roleView2);
                userView = new UserView { UserId = user.Id, Nome = user.UserName, Roles = roles2 };
            }
            */

            userView = new UserView { Roles = rolesViews, Email = user.Email, Nome = user.UserName, UserId = user.Id };

            return View("Roles", userView);
        }

        public ActionResult Delete(string userId, string roleId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.Users.ToList().Find(u => u.Id == userId);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleId);

            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            var users = userManager.Users.ToList();





            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }




            var userView = new UserView
            {
                Email = user.Email,
                Nome = user.UserName,
                UserId = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);

        }



        public ActionResult Delete1(string userId, string roleId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userId);
            var role = roleManager.Roles.ToList().Find(x => x.Id == roleId);

            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            var rolesView = new List<RoleView>();
            var roles = roleManager.Roles.ToList();

            foreach (var item in roles)
            {
                //var role = roles.Find(x => x.Id == roleId);
                if (userManager.IsInRole(userId, item.Name))
                {
                    var roleView = new RoleView { RoleId = item.Id, Name = item.Name };
                    rolesView.Add(roleView);
                }

            }
            var userView = new UserView { UserId = user.Id, Nome = user.UserName, Email = user.Email, Roles = rolesView };

            return View("Roles", userView);
        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
