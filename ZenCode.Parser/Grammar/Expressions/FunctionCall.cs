using ZenCode.Lexer;

namespace ZenCode.Parser.Grammar.Expressions;

public record FunctionCall(Token Identifier, IReadOnlyList<Expression> Parameters) : Expression;