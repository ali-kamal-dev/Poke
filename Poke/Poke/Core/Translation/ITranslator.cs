using System.Threading;
using System.Threading.Tasks;

namespace Poke.Core.Translation
{
    public interface ITranslator
    {
        Task<string> Translate(string text, CancellationToken token = default);
    }
}
