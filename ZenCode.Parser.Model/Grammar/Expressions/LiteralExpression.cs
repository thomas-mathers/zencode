using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record LiteralExpression(Token Token) : Expression
{
    public override string ToString()
    {
        return Token.ToString();
    }
}