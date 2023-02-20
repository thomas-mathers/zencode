using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Model;

namespace ZenCode.Grammar.Statements;

public record VariableDeclarationStatement(Token Identifier, Expression Expression) : Statement;