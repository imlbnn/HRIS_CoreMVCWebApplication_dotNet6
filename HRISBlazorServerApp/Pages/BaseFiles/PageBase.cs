using AutoMapper;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Services;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Services.Page;
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
        private IJSRuntime _jSRuntime { get; set; }

        [Inject]
        private IMapper _mapper { get; set; }

        [Inject]
        public IEmployeeService employeeService { get; set; }

        [Inject]
        public IAccountService accountService { get; set; }

        [Inject]
        public DialogService DialogService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager UriHelper { get; set; }
        [Inject]
        public IDateTime _dateTime { get; set; }

        //[Inject]
        //public ISessionStorageService _sessionStorageService { get; set; }

        [Inject]
        public ContextMenuService _contextMenuService { get; set; }

        [Inject]
        public ICurrentUserService _currentUserService { get; set; }


        public async Task BusyDialog(string message)
        {
            await DialogService.OpenAsync("", ds =>
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
    }
}
