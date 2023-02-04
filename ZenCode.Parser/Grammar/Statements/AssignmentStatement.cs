using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Grammar.Statements;

public record AssignmentStatement(Token Identifier, Expression Expression) : Statement;
