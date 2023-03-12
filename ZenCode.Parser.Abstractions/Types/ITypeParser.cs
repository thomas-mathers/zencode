using ZenCode.Lexer.Abstractions;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions.Types;

public interface ITypeParser
{
    Type ParseType(ITokenStream tokenStream, int precedence = 0);
}