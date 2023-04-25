using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Expressions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class AnonymousFunctionDeclarationExpressionAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly AnonymousFunctionDeclarationExpressionAnalyzer _sut = new();
    
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
                new AnonymousFunctionDeclarationExpression { ReturnType = new TypeMock() }
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
                new AnonymousFunctionDeclarationExpression { ReturnType = new TypeMock() }
            )
        );
    }

    [Fact]
    public void Analyze_NullExpression_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, null!));
    }

    [Fact]
    public void Analyze_AnonymousFunctionNoParameters_ReturnsCorrectType()
    {
        // Arrange
        var expression = new AnonymousFunctionDeclarationExpression
        {
            ReturnType = new TypeMock()
        };

        var expected = new FunctionType(new TypeMock(), new TypeList());

        // Act
        var actual = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, expression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Analyze_AnonymousFunctionOneParameter_ReturnsCorrectType()
    {
        // Arrange
        var expression = new AnonymousFunctionDeclarationExpression
        {
            ReturnType = new TypeMock(),
            Parameters = new ParameterList(new Parameter(new Token(TokenType.Identifier, "x"), new TypeMock()))
        };

        var expected = new FunctionType(new TypeMock(), new TypeList(new TypeMock()));

        // Act
        var actual = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, expression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Analyze_AnonymousFunctionThreeParameters_ReturnsCorrectType()
    {
        // Arrange
        var expression = new AnonymousFunctionDeclarationExpression
        {
            ReturnType = new TypeMock(),
            Parameters = new ParameterList
            (
                new Parameter(new Token(TokenType.Identifier, "x"), new TypeMock()),
                new Parameter(new Token(TokenType.Identifier, "y"), new TypeMock()),
                new Parameter(new Token(TokenType.Identifier, "z"), new TypeMock())
            )
        };

        var expected = new FunctionType(new TypeMock(), new TypeList(new TypeMock(), new TypeMock(), new TypeMock()));

        // Act
        var actual = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, expression);

        // Assert
        Assert.Equal(expected, actual);
    }
}
