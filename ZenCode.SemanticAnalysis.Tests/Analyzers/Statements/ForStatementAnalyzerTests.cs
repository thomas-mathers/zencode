using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.Tests.Common.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Statements.ForStatementAnalyzer;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class ForStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();

    public static readonly IEnumerable<object[]> NonBooleanType = new[]
    {
        new object[] { new IntegerType() },
        new object[] { new FloatType() },
        new object[] { new StringType() }
    };

    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange
        var forStatement = new ForStatement
        {
            Initializer = new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier),
                Value = new ExpressionMock()
            },
            Condition = new ExpressionMock(),
            Iterator = new AssignmentStatement
            {
                VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new ExpressionMock()
            }
        };
        
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                null!,
                _semanticAnalyzerContextMock.Object,
                forStatement
            )
        );
    }
    
    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange
        var forStatement = new ForStatement
        {
            Initializer = new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier),
                Value = new ExpressionMock()
            },
            Condition = new ExpressionMock(),
            Iterator = new AssignmentStatement
            {
                VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new ExpressionMock()
            }
        };
        
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                null!,
                forStatement
            )
        );
    }
    
    [Fact]
    public void Analyze_NullForStatement_ThrowsArgumentNullException()
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
    [MemberData(nameof(NonBooleanType))]
    public void Analyze_NonBooleanCondition_ThrowsTypeMismatchException(Type type)
    {
        // Arrange
        var forStatement = new ForStatement
        {
            Initializer = new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier),
                Value = new ExpressionMock()
            },
            Condition = new ExpressionMock(),
            Iterator = new AssignmentStatement
            {
                VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new ExpressionMock()
            }
        };
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, forStatement.Condition))
            .Returns(type);
        
        // Act + Assert
        Assert.Throws<TypeMismatchException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                _semanticAnalyzerContextMock.Object,
                forStatement
            )
        );
    }
}
