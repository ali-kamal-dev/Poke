namespace Poke.Core.Translation
{
    public interface IValidTranslator : ITranslator
    {
        bool IsValidTranslator(TranslatorEnum translatorType);
    }
}
