using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions.Strategies;

public interface IPrefixExpressionParsingStrategy
{
    Expression Parse(ITokenStream tokenStream);
}