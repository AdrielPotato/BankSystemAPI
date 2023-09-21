using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Models
{
    public abstract class AuthRequest<T> : IRequest<Result<T>>
    {
    }
}
