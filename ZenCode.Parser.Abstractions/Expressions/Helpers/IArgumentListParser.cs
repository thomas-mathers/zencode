using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions.Helpers;

public interface IArgumentListParser
{
    public IReadOnlyList<Expression> Parse(IExpressionParser expressionParser, ITokenStream tokenStream);
}