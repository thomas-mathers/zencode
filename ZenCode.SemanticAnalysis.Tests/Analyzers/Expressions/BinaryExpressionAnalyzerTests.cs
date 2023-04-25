using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.SemanticAnalysis.Tests.TestData;
using ZenCode.Tests.Common.Mocks;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Expressions.BinaryExpressionAnalyzer;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class BinaryExpressionAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                null!,
                _semanticAnalyzerContextMock.Object,
                new BinaryExpression
                {
                    Left = new ExpressionMock(),
                    Operator = new Token(TokenType.Plus),
                    Right = new ExpressionMock()
                }
            )
        );
    }
    
    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                null!,
                new BinaryExpression
                {
                    Left = new ExpressionMock(),
                    Operator = new Token(TokenType.Plus),
                    Right = new ExpressionMock()
                }
            )
        );
    }
    
    [Fact]
    public void Analyze_NullExpression_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                _semanticAnalyzerContextMock.Object,
                null!
            )
        );
    }
    
    [Theory]
    [ClassData(typeof(BinaryOperatorUnsupportedTypes))]
    public void Analyze_UnsupportedTypes_ThrowsBinaryOperatorUnsupportedTypesException
    (
        TokenType op, 
        Type leftType, 
        Type rightType
    )
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        {
            Left = new ExpressionMock(),
            Operator = new Token(op),
            Right = new ExpressionMock()
        };

        _semanticAnalyzerMock
            .SetupSequence(x => x.Analyze(_semanticAnalyzerContextMock.Object, It.IsAny<Expression>()))
            .Returns(leftType)
            .Returns(rightType);
        
        // Act + Assert
        Assert.Throws<BinaryOperatorUnsupportedTypesException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                _semanticAnalyzerContextMock.Object,
                binaryExpression
            )
        );
    }
    
    [Theory]
    [ClassData(typeof(BinaryOperatorSupportedTypes))]
    public void Analyze_SupportedTypes_ReturnsCorrectType
    (
        TokenType op, 
        Type leftType, 
        Type rightType,
        Type expectedType
    )
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        {
            Left = new ExpressionMock(),
            Operator = new Token(op),
            Right = new ExpressionMock()
        };

        _semanticAnalyzerMock
            .SetupSequence(x => x.Analyze(_semanticAnalyzerContextMock.Object, It.IsAny<Expression>()))
            .Returns(leftType)
            .Returns(rightType);
        
        // Act
        var result = Sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            binaryExpression
        );
        
        // Assert
        Assert.Equal(expectedType, result);
    }
}
