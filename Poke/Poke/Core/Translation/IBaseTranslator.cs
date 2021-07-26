using System.Threading;
using System.Threading.Tasks;

namespace Poke.Core.Translation
{
    public interface IBaseTranslator
    {
        Task<string> Translate(string text, CancellationToken token = default);
    }
}
