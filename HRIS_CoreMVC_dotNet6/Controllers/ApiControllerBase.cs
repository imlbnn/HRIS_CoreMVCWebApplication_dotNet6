using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace HRIS.API.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();


        private IHttpContextAccessor accessor;

        protected IHttpContextAccessor contextAccessor => accessor ??= HttpContext.RequestServices.GetService<IHttpContextAccessor>();


        public async Task<bool> VerifySession()
        {
            var data = contextAccessor.HttpContext.Session.GetString("UserId");
            if (data == null)
            {
                ViewBag._isError = true;
                ViewBag._message = "Session Expired";

                await HttpContext.SignOutAsync();

                return false;
            }

            return true;
        }



    }
}
