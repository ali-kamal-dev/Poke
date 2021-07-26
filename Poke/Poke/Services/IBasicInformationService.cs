using System.Threading;
using System.Threading.Tasks;
using Poke.Models.ExternalApiResponses;

namespace Poke.Services
{
    public interface IBasicInformationService
    {
        ValueTask<FilteredBasicPokemonInfoResponse> GetFilteredPokemonInfo(string name, CancellationToken token = default);
    }
}
