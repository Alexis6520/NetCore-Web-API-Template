using Application.Abstractions;

namespace Application.Commands
{
    public class DeleteCommand<TKey, TResource>(TKey key) : IRequest
    {
        public TKey Key { get; set; } = key;
    }
}
