using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class StringLiteralParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(ITokenStream tokenStream)
    {
        return new ConstantExpression(tokenStream.Consume(TokenType.StringLiteral));
    }
}