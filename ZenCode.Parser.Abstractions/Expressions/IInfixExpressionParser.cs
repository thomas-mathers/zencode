using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions
{
    public interface IInfixExpressionParser
    {
        Expression ParseInfixExpression(IParser parser, ITokenStream tokenStream, Expression lExpression);
    }
}