using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ForStatement(VariableDeclarationStatement Initialization, Expression Condition, AssignmentStatement Iterator, Scope Scope) : Statement;