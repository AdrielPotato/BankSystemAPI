using BankSystem.Application.Commands.CreateAccount;
using BankSystem.Application.Commands.DepositMoney;
using BankSystemAPI.Functions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator, ILogger<BaseController> logger) : base(mediator, logger)
        {
        }

        [HttpPost("create")]
        public async Task<JsonResult> Create([FromBody] CreateAccountCommand command) => await HandleControllerActions.Execute(this, command);
    }
}
