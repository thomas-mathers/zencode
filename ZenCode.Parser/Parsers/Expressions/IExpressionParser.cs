using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions;

public interface IExpressionParser
{
    public Expression Parse(ITokenStream tokenStream, int precedence = 0);
}