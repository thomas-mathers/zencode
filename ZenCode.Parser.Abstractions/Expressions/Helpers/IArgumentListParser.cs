using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions.Helpers;

public interface IArgumentListParser
{
    IReadOnlyList<Expression> Parse(ITokenStream tokenStream);
}