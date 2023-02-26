using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record AnonymousFunctionDeclarationExpression(IReadOnlyList<Parameter> Parameters, Scope Scope) : Expression;