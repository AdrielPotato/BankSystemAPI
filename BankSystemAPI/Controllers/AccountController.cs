using BankSystem.Application.Commands.CreateAccount;
using BankSystem.Application.Commands.DepositMoney;
using BankSystem.Application.Commands.TransferMoney;
using BankSystem.Application.Models;
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
        [ProducesResponseType(typeof(Result<CreateAccountViewModel>), 200)]
        [ProducesResponseType(typeof(Result<>), 500)]
        [ProducesResponseType(typeof(Result<>), 404)]
        public async Task<JsonResult> Create([FromBody] CreateAccountCommand command) => await HandleControllerActions.Execute(this, command);
    }
}
