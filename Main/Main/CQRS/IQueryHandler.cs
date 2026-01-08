using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse> where TResponse : notnull
    {
    }
}
