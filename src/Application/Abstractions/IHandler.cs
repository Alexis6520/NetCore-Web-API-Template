using Application.Wrappers;

namespace Application.Abstractions
{
    public interface IHandler<TRequest> :
        MediatR.IRequestHandler<TRequest, Result> where TRequest : IRequest
    {
    }

    public interface IHandler<TRequest, TValue> :
        MediatR.IRequestHandler<TRequest, Result<TValue>> where TRequest : IRequest<TValue>
    {
    }
}
