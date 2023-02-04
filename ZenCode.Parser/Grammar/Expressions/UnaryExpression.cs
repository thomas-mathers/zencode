using ZenCode.Lexer;

namespace ZenCode.Parser.Grammar.Expressions;

public record UnaryExpression(Token Operator, Expression Expression) : Expression;
