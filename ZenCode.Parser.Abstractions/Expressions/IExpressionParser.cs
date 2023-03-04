using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IExpressionParser
{
    Expression ParseExpression(ITokenStream tokenStream, int precedence = 0);
    ExpressionList ParseExpressionList(ITokenStream tokenStream);
}