using System.Data;
using System.Text;
using System.Web.Mvc;

namespace HRIS_CoreMVC_dotNet6.Helpers
{
    public class MenuItem
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int MenuIndex { get; set; }
        public int? ParentMenuId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }


    public class UserMenu
    {


        static IEnumerable<MenuItem> dtSource()
        {
            List<MenuItem> list = new List<MenuItem>();

            list.Add(new MenuItem
            {
                MenuId = 1,
                MenuName = "Home",
                MenuIndex = 0,
                ParentMenuId = null,
                ControllerName = "Home",
                ActionName = "Index"
            });

            list.Add(new MenuItem
            {
                MenuId = 2,
                MenuName = "Employees",
                MenuIndex = 1,
                ParentMenuId = null,
                ControllerName = "Employee",
                ActionName = "/"
            });

            list.Add(new MenuItem
            {
                MenuId = 3,
                MenuName = "Departments",
                MenuIndex = 2,
                ParentMenuId = null,
                ControllerName = "Department",
                ActionName = "/"
            });

            return list.AsEnumerable();
        }

        public static string GetMenu()
        {
            var data = dtSource();
            StringBuilder html = new StringBuilder();
            PopulateMenu(data, null, ref html);
            return html.ToString();
        }

        //public static void PopulateMenu(IEnumerable<MenuItem> data, int parentId, ref StringBuilder html)
        //{
        //    if (data.Any())
        //    {
        //        foreach (MenuItem item in data)
        //        {
        //            //check if menu is menu group
        //            if (item.ParentMenuId == null && !string.IsNullOrEmpty(item.ControllerName) && !string.IsNullOrEmpty(item.ActionName))
        //            {
        //                html.Append("<li class=\"nav-item\">");
        //                //html.Append("<a asp-controller=\"" + item.MenuName + "\" asp-action=\"" + item.ActionName + "\" class=\"nav-link\">");
        //                html.Append($"<a href='#' class='menu-parent' data-menu-id='{item.MenuId}'><i class='ion ion-ios-arrow-forward'></i> {item.MenuName}</a>");

        //                //html.Append("<i class=\"fas fa-angle-right left\"></i>");
        //                //html.Append("<p>" + item.MenuName + "</p>");
        //                //html.Append("</a></li>");
        //                html.Append("</li>");
        //            }
        //            else
        //            {

        //            }


        //        }
        //    }

        //}


        public static void PopulateMenu(IEnumerable<MenuItem> data, int? parentId, ref StringBuilder html)
        {
            var app = data.Where(x => x.ParentMenuId == parentId).OrderBy(x => x.MenuIndex);

            if (app.Count() > 0)
            {
                foreach (var item in app)
                {
                    html.Append("<li>");
                    if (item.ParentMenuId == null && string.IsNullOrEmpty(item.ControllerName))
                    {
                        html.Append($"<a href='#' class='menu-parent' data-menu-id='{item.MenuId}'><i class='ion ion-ios-arrow-forward'></i> {item.MenuName}</a>");
                    }
                    else
                    {
                        var link = "";
                        html.Append($"<a href='/{item.ControllerName}' class='menu-parent' data-menu-id='{item.MenuId}'><i class='ion ion-ios-arrow-forward'></i> {item.MenuName}</a>");
                        //html.Append($"<a asp-controller='{item.ControllerName}' asp-action='{item.ActionName}'  data-menu-id='{item.MenuId}' >{item.MenuName}</a>");
                    }
                    html.Append("<ul class='ul-child'>");
                    PopulateMenu(data, item.MenuId, ref html);
                    html.Append("</ul>");
                    html.Append("</li>");
                    html.Replace("<ul class='ul-child'></ul>", "");
                }
            }
        }
    }
}
