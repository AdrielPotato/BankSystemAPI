using BankSystem.Application.Commands.DepositMoney;
using BankSystem.Application.Commands.TransferMoney;
using BankSystem.Application.Commands.WithdrawMoney;
using BankSystem.Application.Queries.GetBalance;
using BankSystemAPI.Functions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemAPI.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : BaseController
    {
        public TransactionController(IMediator mediator, ILogger<BaseController> logger) : base(mediator, logger)
        {

            
        }

        [HttpPost("deposit")]
        public async Task<JsonResult> Deposit([FromBody] DepositMoneyCommand command) => await HandleControllerActions.Execute(this, command);

        [HttpPost("fund-transfer")]
        public async Task<JsonResult> Deposit([FromBody] TransferMoneyCommand command) => await HandleControllerActions.Execute(this, command);

        [HttpPost("withdraw")]
        public async Task<JsonResult> Withdraw([FromBody] WithdrawMoneyCommand command) => await HandleControllerActions.Execute(this, command);

        [HttpGet("balance")]
        public async Task<JsonResult> Checkbalance([FromQuery] GetBalanceQuery query) => await HandleControllerActions.Execute(this, query);
    }
}
