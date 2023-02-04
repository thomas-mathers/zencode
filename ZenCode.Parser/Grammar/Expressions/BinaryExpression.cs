using ZenCode.Lexer;

namespace ZenCode.Parser.Grammar.Expressions;

public record BinaryExpression(Expression LeftOperand, Token Operator, Expression RightOperand) : Expression;
