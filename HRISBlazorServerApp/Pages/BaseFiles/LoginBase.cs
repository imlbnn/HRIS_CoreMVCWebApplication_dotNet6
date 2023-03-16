using HRISBlazorServerApp.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace HRISBlazorServerApp.Pages.BaseFiles
{
    public class LoginBase : PageBase
    {
        public LoginRequest loginModel = new LoginRequest();
        public bool ShowErrors;
        public string Error = "";

        [Inject]
        private TokenConfig Config { get; set; }


        public async Task HandleLogin()
        {
            try
            {
                ShowErrors = false;

                var result = await accountService.Login(loginModel);

                if (result.Success)
                {
                    Config.SetToken(result.Token);
                    Config.SetUsername(loginModel.Username);
                    UriHelper.NavigateTo("/main");
                }
                else
                {
                    Error = "Invalid Login";
                    ShowErrors = true;

                    ShowNotification(NotificationSeverity.Error, "Invalid Login", "Error");
                }
            }
            catch (Exception ex)
            {
                Error = "Invalid Login";
                ShowErrors = true;
                ShowNotification(NotificationSeverity.Error, "Invalid Login", "Error", 3000, "background-color: lightblue");
            }
        }
    }
}
