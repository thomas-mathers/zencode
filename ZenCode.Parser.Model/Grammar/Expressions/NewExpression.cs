using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record NewExpression(Type Type, ExpressionList ExpressionList) : Expression;