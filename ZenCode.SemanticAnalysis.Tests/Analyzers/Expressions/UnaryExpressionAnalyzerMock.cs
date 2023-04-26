using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.Tests.Common.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class UnaryExpressionAnalyzerMock
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly UnaryExpressionAnalyzer _sut = new();

    public static readonly IEnumerable<object[]> NonArithmeticTypes = new[]
    {
        new object[] { new VoidType() },
        new object[] { new BooleanType() },
        new object[] { new StringType() },
    };
    
    public static readonly IEnumerable<object[]> NonLogicalTypes = new[]
    {
        new object[] { new VoidType() },
        new object[] { new IntegerType() },
        new object[] { new FloatType() },
        new object[] { new StringType() }
    };

    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                null!,
                _semanticAnalyzerContextMock.Object,
                new UnaryExpression(new Token(TokenType.Not), new ExpressionMock())
            )
        );
    }
    
    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                null!,
                new UnaryExpression(new Token(TokenType.Not), new ExpressionMock())
            )
        );
    }
    
    [Fact]
    public void Analyze_NullExpression_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                _semanticAnalyzerContextMock.Object,
                null!
            )
        );
    }
    
    [Theory]
    [MemberData(nameof(NonArithmeticTypes))]
    public void Analyze_MinusNonArithmeticExpression_AddsUnaryOperatorUnsupportedTypeException(Type type)
    {
        // Arrange
        var expression = new ExpressionMock();
        var unaryExpression = new UnaryExpression(new Token(TokenType.Minus), expression);
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, expression))
            .Returns(type);
        
        // Act
        _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            unaryExpression
        );
        
        // Assert
        _semanticAnalyzerContextMock.Verify(x => x.AddError(It.IsAny<UnaryOperatorUnsupportedTypeException>()));
    }
    
    [Fact]
    public void Analyze_MinusIntegerExpression_ReturnsIntegerType()
    {
        // Arrange
        var expression = new ExpressionMock();
        var unaryExpression = new UnaryExpression(new Token(TokenType.Minus), expression);
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, expression))
            .Returns(new IntegerType());
        
        // Act
        var result = _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            unaryExpression
        );
        
        // Assert
        Assert.Equal(new IntegerType(), result);
    }
    
    [Fact]
    public void Analyze_MinusFloatExpression_ReturnsFloatType()
    {
        // Arrange
        var expression = new ExpressionMock();
        var unaryExpression = new UnaryExpression(new Token(TokenType.Minus), expression);
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, expression))
            .Returns(new FloatType());
        
        // Act
        var result = _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            unaryExpression
        );
        
        // Assert
        Assert.Equal(new FloatType(), result);
    }
    
    [Theory]
    [MemberData(nameof(NonLogicalTypes))]
    public void Analyze_NotNonLogicalExpression_AddsUnaryOperatorUnsupportedTypeException(Type type)
    {
        // Arrange
        var expression = new ExpressionMock();
        var unaryExpression = new UnaryExpression(new Token(TokenType.Not), expression);
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, expression))
            .Returns(type);
        
        // Act
        _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            unaryExpression
        );
        
        // Assert
        _semanticAnalyzerContextMock.Verify(x => x.AddError(It.IsAny<UnaryOperatorUnsupportedTypeException>()));
    }
    
    [Fact]
    public void Analyze_NotBooleanExpression_ReturnsBooleanType()
    {
        // Arrange
        var expression = new ExpressionMock();
        var unaryExpression = new UnaryExpression(new Token(TokenType.Not), expression);
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, expression))
            .Returns(new BooleanType());
        
        // Act
        var result = _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            unaryExpression
        );
        
        // Assert
        Assert.Equal(new BooleanType(), result);
    }
}
