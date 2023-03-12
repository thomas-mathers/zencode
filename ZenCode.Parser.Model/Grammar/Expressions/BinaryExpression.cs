using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions
{
    public record BinaryExpression(Expression LeftOperand, Token Operator, Expression RightOperand) : Expression;
}