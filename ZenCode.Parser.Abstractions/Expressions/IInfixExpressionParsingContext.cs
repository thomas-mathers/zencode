using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IInfixExpressionParsingContext
{
    Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator);
    int GetPrecedence(ITokenStream tokenStream);
}