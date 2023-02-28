using ZenCode.Lexer.Abstractions;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions.Types;

public interface ITypeParser
{
    Type Parse(ITokenStream tokenStream, int precedence = 0);
}