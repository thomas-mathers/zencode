using ZenCode.Lexer.Abstractions;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions.Types.Strategies;

public interface IInfixTypeParsingStrategy
{
    int Precedence { get; }
    Type Parse(ITokenStream tokenStream, Type type);
}