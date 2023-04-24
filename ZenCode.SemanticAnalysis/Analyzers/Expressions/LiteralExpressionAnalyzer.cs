using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class LiteralExpressionAnalyzer
{
    public static Type Analyze(ISemanticAnalyzerContext context, LiteralExpression literalExpression)
    {
        Type type = literalExpression.Token.Type switch
        {
            TokenType.BooleanLiteral => new BooleanType(),
            TokenType.IntegerLiteral => new IntegerType(),
            TokenType.FloatLiteral => new FloatType(),
            TokenType.StringLiteral => new StringType(),
            _ => throw new InvalidOperationException()
        };

        context.SetAstNodeType(literalExpression, type);

        return type;
    }
}
