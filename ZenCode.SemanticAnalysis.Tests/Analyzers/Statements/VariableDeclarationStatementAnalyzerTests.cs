using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class VariableDeclarationStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly VariableDeclarationStatementAnalyzer _sut = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange
        var variableDeclarationStatement = new VariableDeclarationStatement
        {
            VariableName = new Token(TokenType.Identifier),
            Value = new ExpressionMock()
        };

        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(null!, _semanticAnalyzerContextMock.Object, variableDeclarationStatement));
    }

    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange
        var variableDeclarationStatement = new VariableDeclarationStatement
        {
            VariableName = new Token(TokenType.Identifier),
            Value = new ExpressionMock()
        };

        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, null!, variableDeclarationStatement));
    }

    [Fact]
    public void Analyze_NullVariableDeclarationStatement_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, null!));
    }

    [Fact]
    public void Analyze_VariableDeclaration_DefinesVariableReturnsVoidType()
    {
        // Arrange
        var variableDeclarationStatement = new VariableDeclarationStatement
        {
            VariableName = new Token(TokenType.Identifier),
            Value = new ExpressionMock()
        };

        var type = new TypeMock();

        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, variableDeclarationStatement.Value))
            .Returns(type);

        // Act
        var result = _sut.Analyze
            (_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, variableDeclarationStatement);

        // Assert
        Assert.Equal(new VoidType(), result);

        _semanticAnalyzerContextMock
            .Verify
            (
                x => x.DefineSymbol
                    (It.Is<Symbol>(y => y.Token == variableDeclarationStatement.VariableName && y.Type == type))
            );
    }
}
