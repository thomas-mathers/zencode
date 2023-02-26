using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Expressions.Helpers;

public class TypeParser : ITypeParser
{
    public Type Parse(ITokenStream tokenStream)
    {
        throw new NotImplementedException();
    }
}