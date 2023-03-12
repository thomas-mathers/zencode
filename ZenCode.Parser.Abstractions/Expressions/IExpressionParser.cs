using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IExpressionParser
{
    Expression ParseExpression(IParser parser, ITokenStream tokenStream, int precedence);
}