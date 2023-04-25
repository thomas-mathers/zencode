using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Expressions.LiteralExpressionAnalyzer;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class LiteralExpressionAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();

    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze(null!, new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        );
    }

    [Fact]
    public void Analyze_NullExpression_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze(_semanticAnalyzerContextMock.Object, null!)
        );
    }
    
    [Fact]
    public void Analyze_BooleanLiteral_ReturnsBooleanType()
    {
        // Arrange
        var expression = new LiteralExpression(new Token(TokenType.BooleanLiteral));

        // Act
        var result = Sut.Analyze(_semanticAnalyzerContextMock.Object, expression);

        // Assert
        Assert.Equal(new BooleanType(), result);
    }

    [Fact]
    public void Analyze_IntegerLiteral_ReturnsIntegerType()
    {
        // Arrange
        var expression = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        // Act
        var result = Sut.Analyze(_semanticAnalyzerContextMock.Object, expression);

        // Assert
        Assert.Equal(new IntegerType(), result);
    }
    
    [Fact]
    public void Analyze_FloatLiteral_ReturnsFloatType()
    {
        // Arrange
        var expression = new LiteralExpression(new Token(TokenType.FloatLiteral));

        // Act
        var result = Sut.Analyze(_semanticAnalyzerContextMock.Object, expression);

        // Assert
        Assert.Equal(new FloatType(), result);
    }
    
    [Fact]
    public void Analyze_StringLiteral_ReturnsStringType()
    {
        // Arrange
        var expression = new LiteralExpression(new Token(TokenType.StringLiteral));

        // Act
        var result = Sut.Analyze(_semanticAnalyzerContextMock.Object, expression);

        // Assert
        Assert.Equal(new StringType(), result);
    }
}
