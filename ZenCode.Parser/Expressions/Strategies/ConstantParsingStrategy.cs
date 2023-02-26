using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class ConstantParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(ITokenStream tokenStream)
    {
        return new ConstantExpression(tokenStream.Consume());
    }
}