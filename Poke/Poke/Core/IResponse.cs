using Poke.Models.Responses;

namespace Poke.Core
{
    public interface IResponse
    {
        Status Status { get; set; }
    }
}
