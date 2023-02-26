using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model;

namespace ZenCode.Parser.Abstractions.Expressions.Helpers;

public interface IParameterListParser
{
    IReadOnlyList<Parameter> Parse(ITokenStream tokenStream);
}