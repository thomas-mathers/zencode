using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions.Types;

public interface ITypeParser
{
    Type ParseType(ITokenStream tokenStream, int precedence = 0);
    ParameterList ParseParameterList(ITokenStream tokenStream);
}