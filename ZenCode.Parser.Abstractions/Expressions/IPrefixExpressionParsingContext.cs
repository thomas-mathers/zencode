using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IPrefixExpressionParsingContext
{
    Expression Parse(ITokenStream tokenStream);
}