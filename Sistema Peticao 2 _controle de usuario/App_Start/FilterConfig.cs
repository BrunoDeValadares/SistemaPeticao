using System.Web;
using System.Web.Mvc;

namespace Sistema_Peticao_2__controle_de_usuario
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
