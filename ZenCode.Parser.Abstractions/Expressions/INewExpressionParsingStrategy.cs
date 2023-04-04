using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface INewExpressionParsingStrategy
{
    NewArrayExpression Parse(IParser parser, ITokenStream tokenStream);
}