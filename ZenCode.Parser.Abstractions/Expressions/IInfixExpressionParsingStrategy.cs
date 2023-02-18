using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IInfixExpressionParsingStrategy
{
    int Precedence { get; }
    Expression Parse(ITokenStream tokenStream, Expression lOperand);
}