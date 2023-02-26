using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions.Strategies;

public interface IInfixExpressionParsingStrategy
{
    int Precedence { get; }
    Expression Parse(ITokenStream tokenStream, Expression lOperand);
}