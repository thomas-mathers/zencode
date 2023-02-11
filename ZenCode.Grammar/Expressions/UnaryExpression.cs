using ZenCode.Lexer.Model;

namespace ZenCode.Grammar.Expressions;

public record UnaryExpression(Token Operator, Expression Expression) : Expression;