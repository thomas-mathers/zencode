using ZenCode.Lexer.Abstractions;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions.Expressions.Helpers;

public interface ITypeParser
{
    Type Parse(ITokenStream tokenStream);
}