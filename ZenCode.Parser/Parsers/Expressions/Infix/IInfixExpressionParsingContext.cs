using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Infix;

public interface IInfixExpressionParsingContext
{
    Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator);
    int GetPrecedence(ITokenStream tokenStream);
}