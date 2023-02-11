using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Prefix;

public class ConstantParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        return new ConstantExpression(token);
    }
}