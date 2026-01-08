using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
