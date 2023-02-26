using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record ConstantExpression(Token Token) : Expression;