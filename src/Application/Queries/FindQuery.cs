using Application.Abstractions;

namespace Application.Queries
{
    public class FindQuery<TKey, TValue>(TKey key) : IRequest<TValue>
    {
        public TKey Key { get; set; } = key;
    }
}
