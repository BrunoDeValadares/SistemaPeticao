using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sistema_Advocacia.Models;

namespace Sistema_Advocacia.ViewModels
{
    //ViewModels são modelos utilizados apenas pra visão, não geram tabelas 
    //Aqui temos uma ViewModel com o idUsuario e com todos os roles dele. 
    public class UserView
    {
        public string UserId { get; set; }

        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RoleView Role { get; set; }

        public List<RoleView> Roles { get; set; }

        public static explicit operator UserView(List<ApplicationUser> v)
        {
            throw new NotImplementedException();
        }
    }
}