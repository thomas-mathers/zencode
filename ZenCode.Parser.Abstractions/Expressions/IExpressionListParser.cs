using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IExpressionListParser
{
    IReadOnlyList<Expression> Parse(ITokenStream tokenStream);
}