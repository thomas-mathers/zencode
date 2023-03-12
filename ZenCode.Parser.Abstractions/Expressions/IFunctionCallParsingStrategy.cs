using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies
{
    public interface IFunctionCallParsingStrategy
    {
        FunctionCallExpression Parse(IParser parser, ITokenStream tokenStream, Expression lOperand);
    }
}