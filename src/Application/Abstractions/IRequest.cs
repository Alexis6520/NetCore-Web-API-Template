using Application.Wrappers;

namespace Application.Abstractions
{
    public interface IRequest : MediatR.IRequest<Result>
    {
    }

    public interface IRequest<TValue> : MediatR.IRequest<Result<TValue>>
    {
    }
}
