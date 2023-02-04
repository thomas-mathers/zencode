using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Prefix;

public interface IPrefixExpressionParser
{
    Expression Parse(IParser parser, Token token);
}