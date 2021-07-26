using System.Threading;
using System.Threading.Tasks;

namespace Poke.Core
{
    public interface IHandler<in T1, T2> where T1 : IRequest where T2 : IResponse
    {
        ValueTask<T2> HandleAsync(T1 command, CancellationToken token = default);
    }
}
