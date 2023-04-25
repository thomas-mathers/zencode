using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class ContinueStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly ContinueStatementAnalyzer _sut = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze(null!)
        );
    }
    
    [Fact]
    public void Analyze_NoLoopStatement_ThrowsInvalidBreakException()
    {
        // Arrange
        _semanticAnalyzerContextMock.Setup(e => e.AncestorAstNodes()).Returns(Array.Empty<AstNode>());
        
        // Act + Assert
        Assert.Throws<InvalidContinueException>
        (
            () => _sut.Analyze(_semanticAnalyzerContextMock.Object)
        );
    }
    
    [Fact]
    public void Analyze_InsideWhileLoop_ReturnsVoidType()
    {
        // Arrange
        _semanticAnalyzerContextMock
            .Setup(e => e.AncestorAstNodes())
            .Returns(new AstNode[]
            {
                new WhileStatement
                {
                    ConditionScope = new ConditionScope
                    {
                        Condition = new ExpressionMock()
                    }
                }
            });
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerContextMock.Object);
        
        // Assert
        Assert.Equal(new VoidType(), result);
    }
    
    [Fact]
    public void Analyze_InsideForLoop_ReturnsVoidType()
    {
        // Arrange
        _semanticAnalyzerContextMock
            .Setup(e => e.AncestorAstNodes())
            .Returns(new AstNode[]
            {
                new ForStatement
                {
                    Initializer = new VariableDeclarationStatement
                    {
                        VariableName = new Token(TokenType.Identifier, "i"),
                        Value = new ExpressionMock()
                    },
                    Condition = new ExpressionMock(),
                    Iterator = new AssignmentStatement
                    {
                        VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "i")),
                        Value = new ExpressionMock()
                    }
                }
            });
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerContextMock.Object);
        
        // Assert
        Assert.Equal(new VoidType(), result);
    }
}
