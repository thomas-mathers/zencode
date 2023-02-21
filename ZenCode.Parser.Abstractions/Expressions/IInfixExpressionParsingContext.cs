using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IInfixExpressionParsingContext
{
    Expression Parse(ITokenStream tokenStream, Expression lOperand);
    int GetPrecedence(ITokenStream tokenStream);
}