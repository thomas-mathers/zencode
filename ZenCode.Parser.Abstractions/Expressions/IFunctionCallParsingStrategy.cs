using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IFunctionCallParsingStrategy
{
    FunctionCallExpression Parse(IParser parser, ITokenStream tokenStream, Expression lOperand);
}
