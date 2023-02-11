using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IPrefixExpressionParsingStrategy
{
    Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token);
}