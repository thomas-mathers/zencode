using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Prefix;

public interface IPrefixExpressionParsingStrategy
{
    Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token);
}