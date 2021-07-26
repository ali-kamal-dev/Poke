namespace Poke.Core.Translation
{
    public interface ITranslatorFactory
    {
        ITranslator Create(TranslatorEnum translator);
    }
}
