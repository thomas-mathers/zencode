using ZenCode.Lexer;

namespace ZenCode.Parser.Grammar.Expressions;

public record ConstantExpression(Token Token) : Expression;
