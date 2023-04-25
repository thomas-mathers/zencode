using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class FunctionCallExpressionAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly FunctionCallExpressionAnalyzer _sut = new();

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
                new FunctionCallExpression
                {
                    FunctionReference = new ExpressionMock(),
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
            () => _sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                null!,
                new FunctionCallExpression
                {
                    FunctionReference = new ExpressionMock(),
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
            () => _sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                _semanticAnalyzerContextMock.Object,
                null!
            )
        );
    }

    [Fact]
    public void Analyze_FunctionReferenceIsNotFunctionType_ThrowsInvokingNonFunctionTypeException()
    {
        // Arrange
        var functionReference = new ExpressionMock();

        var expression = new FunctionCallExpression
        {
            FunctionReference = functionReference,
        };

        _semanticAnalyzerMock
            .Setup(m => m.Analyze(_semanticAnalyzerContextMock.Object, functionReference))
            .Returns(new TypeMock());

        // Act + Assert
        Assert.Throws<InvokingNonFunctionTypeException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, expression));
    }
    
    [Fact]
    public void Analyze_IncorrectNumberOfParameters_ThrowsIncorrectNumberOfParametersException()
    {
        // Arrange
        var functionReference = new ExpressionMock();

        var expression = new FunctionCallExpression
        {
            FunctionReference = functionReference,
            Arguments = new ExpressionList
            (
                new ExpressionMock(),
                new ExpressionMock()
            )
        };

        _semanticAnalyzerMock
            .Setup(m => m.Analyze(_semanticAnalyzerContextMock.Object, functionReference))
            .Returns
            (
                new FunctionType
                (
                    new TypeMock(),
                    new TypeList()
                )
            );

        // Act + Assert
        Assert.Throws<IncorrectNumberOfParametersException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, expression));
    }
    
    [Fact]
    public void Analyze_IncorrectParameterType_ThrowsTypeMismatchException()
    {
        // Arrange
        var functionReference = new ExpressionMock();

        var expression = new FunctionCallExpression
        {
            FunctionReference = functionReference,
            Arguments = new ExpressionList
            (
                new ExpressionMock(),
                new ExpressionMock()
            )
        };

        _semanticAnalyzerMock
            .Setup(m => m.Analyze(_semanticAnalyzerContextMock.Object, functionReference))
            .Returns
            (
                new FunctionType
                (
                    new TypeMock(),
                    new TypeList
                    (
                        new TypeMock(),
                        new TypeMock()
                    )
                )
            );

        // Act + Assert
        Assert.Throws<TypeMismatchException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, expression));
    }
}
