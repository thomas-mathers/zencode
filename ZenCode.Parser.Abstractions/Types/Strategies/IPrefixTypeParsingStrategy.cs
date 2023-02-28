using ZenCode.Lexer.Abstractions;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions.Types.Strategies;

public interface IPrefixTypeParsingStrategy
{
    Type Parse(ITokenStream tokenStream);
}