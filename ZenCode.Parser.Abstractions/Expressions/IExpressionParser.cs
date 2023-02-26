using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IExpressionParser
{
    public Expression Parse(ITokenStream tokenStream, int precedence = 0);
}