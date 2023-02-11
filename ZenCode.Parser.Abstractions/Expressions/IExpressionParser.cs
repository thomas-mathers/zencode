using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IExpressionParser
{
    public Expression Parse(ITokenStream tokenStream, int precedence = 0);
}