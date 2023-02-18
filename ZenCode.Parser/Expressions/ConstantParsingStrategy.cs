using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class ConstantParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(ITokenStream tokenStream)
    {
        return new ConstantExpression(tokenStream.Consume());
    }
}