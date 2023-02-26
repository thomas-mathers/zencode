using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record AssignmentStatement(Token Identifier, Expression Expression) : Statement;