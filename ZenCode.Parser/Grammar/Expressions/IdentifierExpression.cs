using ZenCode.Lexer;

namespace ZenCode.Parser.Grammar.Expressions;

public record IdentifierExpression(Token Identifier) : Expression;
