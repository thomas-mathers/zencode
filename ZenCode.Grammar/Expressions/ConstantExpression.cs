using ZenCode.Lexer.Model;

namespace ZenCode.Grammar.Expressions;

public record ConstantExpression(Token Token) : Expression;