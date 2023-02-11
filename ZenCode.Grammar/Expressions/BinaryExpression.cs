using ZenCode.Lexer.Model;

namespace ZenCode.Grammar.Expressions;

public record BinaryExpression(Expression LeftOperand, Token Operator, Expression RightOperand) : Expression;