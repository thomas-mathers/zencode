using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Infix;

public interface IInfixExpressionParser
{
    Expression Parse(IParser parser, Expression lOperand, Token @operator);
    int GetPrecedence();
}