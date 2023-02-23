using System.Data;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

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

    public class UserMenuRights
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }

        /*
         *
        //Optional Fields
        public bool CanAdd { get; set; }
        public string AddError { get; set; }
        public bool CanEdit { get; set; }
        public string EditError { get; set; }
        public bool CanDelete { get; set; }
        public string DeleteError { get; set; }
        public bool CanSearch { get; set; }
        public string SearchError { get; set; }
        public bool CanPrint { get; set; }
        public string PrintError { get; set; }
        *
        */
    }

    public class MenuBehaviour
    {
        public static string MenuName { get; set; }
        public static bool IsMenuOpen { get; set;}
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
                ParentMenuId = -1,
                ControllerName = "Home",
                ActionName = ""
            });

            list.Add(new MenuItem
            {
                MenuId = 2,
                MenuName = "Employees",
                MenuIndex = 1,
                ParentMenuId = -1,
                ControllerName = "Employee",
                ActionName = ""
            });

            list.Add(new MenuItem
            {
                MenuId = 3,
                MenuName = "Departments",
                MenuIndex = 2,
                ParentMenuId = -1,
                ControllerName = "Department",
                ActionName = ""
            });

            list.Add(new MenuItem
            {
                MenuId = 4,
                MenuName = "Settings",
                MenuIndex = 3,
                ParentMenuId = -1,
                ControllerName = "",
                ActionName = ""
            });

            list.Add(new MenuItem
            {
                MenuId = 5,
                MenuName = "Sample1",
                MenuIndex = 0,
                ParentMenuId = 4,
                ControllerName = "",
                ActionName = ""
            });

            list.Add(new MenuItem
            {
                MenuId = 6,
                MenuName = "Sample2",
                MenuIndex = 1,
                ParentMenuId = 4,
                ControllerName = "Employee",
                ActionName = ""
            });

            list.Add(new MenuItem
            {
                MenuId = 7,
                MenuName = "Sample3",
                MenuIndex = 2,
                ParentMenuId = 4,
                ControllerName = "",
                ActionName = ""
            });

            return list.AsEnumerable();
        }

        public static string GetMenu()
        {
            //You can create a table in the database using the model of MenuItem
            //For user authorization create table containing userid or username, and menuID
            var data = dtSource();
            StringBuilder html = new StringBuilder();
            PopulateMenu(data, -1, ref html);
            return html.ToString();
        }

        public static void PopulateMenu(IEnumerable<MenuItem> data, int? parentId, ref StringBuilder html)
        {
            var app = data.Where(x => x.ParentMenuId == parentId).OrderBy(x => x.MenuIndex);

            if (app.Count() > 0)
            {
                foreach (var item in app)
                {
                    //html.Append("<li class='nav-item menu-open'>");
                    
                    if (item.ParentMenuId == null || item.ParentMenuId == -1)
                    {
                        var link = "#";

                        if (!string.IsNullOrEmpty(item.ControllerName) && !string.IsNullOrEmpty(item.ActionName))
                        {
                            html.Append("<li class='nav-item'>");
                            link = $"/{item.ControllerName}/{item.ActionName}";
                            html.Append($"<a href='{link}' data-menu-id='{item.MenuId}' class='nav-link' style='font-size:13px;'><i class='left fas fa-angle-right'></i> {item.MenuName}</a>");
                        }
                        else if (!string.IsNullOrEmpty(item.ControllerName) && string.IsNullOrEmpty(item.ActionName))
                        {
                            html.Append("<li class='nav-item'>");
                            link = $"/{item.ControllerName}/";
                            html.Append($"<a href='{link}' data-menu-id='{item.MenuId}' class='nav-link' style='font-size:13px;'><i class='left fas fa-angle-right'></i> {item.MenuName}</a>");
                        }
                        else
                        {
                            html.Append($"<li class='nav-header'>{item.MenuName}");
                            //html.Append("<li class='nav-item menu-open'>");
                            //html.Append($"<a href='{link}' data-menu-id='{item.MenuId}' class='nav-link'><i class='right fas fa-angle-left'></i> {item.MenuName}</a>");
                        }
                        
                        
                    }
                    else
                    {
                        var link = "#";

                        if (!string.IsNullOrEmpty(item.ControllerName) && !string.IsNullOrEmpty(item.ActionName))
                        {
                            html.Append("<li class='nav-item'>");
                            link = $"/{item.ControllerName}/{item.ActionName}";
                            html.Append($"<a href='{link}' data-menu-id='{item.MenuId}' class='nav-link' style='font-size:13px;'><i class='left fas fa-angle-right'></i> {item.MenuName}</a>");
                        }
                        else if (!string.IsNullOrEmpty(item.ControllerName) && string.IsNullOrEmpty(item.ActionName))
                        {
                            html.Append("<li class='nav-item'>");

                            link = $"/{item.ControllerName}/";

                            html.Append($"<a href='{link}' data-menu-id='{item.MenuId}' class='nav-link' style='font-size:13px;'><i class='left fas fa-angle-right'></i> {item.MenuName}</a>");
                        }
                        else
                        {
                            html.Append($"<li class='nav-header'>{item.MenuName}");
                            //html.Append($"<a href='{link}' data-menu-id='{item.MenuId}' class='nav-link'><i class='right fas fa-angle-left'></i> {item.MenuName}</a>");
                        }
                    }

                    //html.Append("<ul class='nav nav-treeview'>");
                    
                    PopulateMenu(data, item.MenuId, ref html);
                    
                    //html.Append("</ul>");
                    
                    html.Append("</li>");
                    
                    html.Replace("<ul class='nav nav-treeview'></ul>", "");
                }
            }
        }
    }
}
