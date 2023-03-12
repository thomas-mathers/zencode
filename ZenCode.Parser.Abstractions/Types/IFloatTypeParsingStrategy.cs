using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies
{
    public interface IFloatTypeParsingStrategy
    {
        FloatType Parse(ITokenStream tokenStream);
    }
}