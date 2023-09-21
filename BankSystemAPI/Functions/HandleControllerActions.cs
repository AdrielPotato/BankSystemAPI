using BankSystem.Application.Models;
using BankSystemAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemAPI.Functions
{
    public static class HandleControllerActions
    {
        public static async Task<JsonResult> Execute<T>(BaseController controller, AuthRequest<T> request)
        {
            var result = await controller._mediator.Send(request);

            return HandleResponse.Execute(result, controller);
        }
    }
}
