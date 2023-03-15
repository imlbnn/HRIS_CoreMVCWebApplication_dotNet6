using AutoMapper;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Services.PageServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;
using System.Text;

namespace HRISBlazorServerApp.Pages.BaseFiles
{
    public class PageBase : ComponentBase
    {
        [Inject]
        public IJSRuntime _jSRuntime { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }

        [Inject]
        public IEmployeeService employeeService { get; set; }

        [Inject]
        public IAccountService accountService { get; set; }

        [Inject]
        public IDepartmentService departmentService { get; set; }

        [Inject]
        public IDepartmentSectionService departmentSectionService { get; set; }

        [Inject]
        public ICivilStatusService civilStatusService { get; set; }


        [Inject]
        public DialogService dialogService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager UriHelper { get; set; }
       
        [Inject]
        public ContextMenuService _contextMenuService { get; set; }
        [Inject]
        public NotificationService notificationService { get; set; }


        public async Task BusyDialog(string message)
        {
            await dialogService.OpenAsync("", ds =>
            {
                RenderFragment content = b =>
                {
                    b.OpenElement(0, "div");
                    b.AddAttribute(1, "class", "row");

                    b.OpenElement(2, "div");
                    b.AddAttribute(3, "class", "col-md-12");

                    b.AddContent(4, message);

                    b.CloseElement();
                    b.CloseElement();
                };
                return content;
            }, new DialogOptions() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto" });
        }

        public string DataFromBase64(string data)
        {
            byte[] b64String = Convert.FromBase64String(data);
            string decodedString = Encoding.UTF8.GetString(b64String);
            return decodedString;
        }


        public void ShowNotification(NotificationSeverity severity, string detail, string summary, double duration = 3000, string style = "")
        {
            var message = new NotificationMessage
            {
                Severity = severity,
                Style = style,
                Detail = detail,
                Duration = duration,
                Summary = summary
            };


            notificationService.Notify(message);
        }
    }
}
