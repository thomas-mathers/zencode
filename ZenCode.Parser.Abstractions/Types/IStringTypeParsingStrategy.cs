using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies
{
    public interface IStringTypeParsingStrategy
    {
        StringType Parse(ITokenStream tokenStream);
    }
}