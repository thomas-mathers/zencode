using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public class LiteralExpressionAnalyzer : ILiteralExpressionAnalyzer
{
    public Type Analyze(ISemanticAnalyzerContext context, LiteralExpression literalExpression)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(literalExpression);
        
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
