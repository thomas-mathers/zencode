using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.Tests.Common.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class AssignmentStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly AssignmentStatementAnalyzer _sut = new();

    public static IEnumerable<object[]> NonMatchingTypes = new[]
    {
        new object[] { new BooleanType(), new IntegerType() },
        new object[] { new BooleanType(), new FloatType() },
        new object[] { new BooleanType(), new StringType() },
        new object[] { new IntegerType(), new BooleanType() },
        new object[] { new IntegerType(), new FloatType() },
        new object[] { new IntegerType(), new StringType() },
        new object[] { new FloatType(), new BooleanType() },
        new object[] { new FloatType(), new IntegerType() },
        new object[] { new FloatType(), new StringType() },
        new object[] { new StringType(), new BooleanType() },
        new object[] { new StringType(), new IntegerType() },
        new object[] { new StringType(), new FloatType() },
    };

    public static IEnumerable<object[]> PrimitiveTypes = new[]
    {
        new object[] { new BooleanType() },
        new object[] { new IntegerType() },
        new object[] { new FloatType() },
        new object[] { new StringType() },
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
                new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    Value = new ExpressionMock()
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
                new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    Value = new ExpressionMock()
                }
            )
        );
    }
    
    [Fact]
    public void Analyze_NullStatement_ThrowsArgumentNullException()
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
    public void Analyze_UndeclaredVariable_ThrowsUndeclaredIdentifierException()
    {
        // Arrange
        var statement = new AssignmentStatement
        {
            VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Value = new ExpressionMock()
        };
        
        // Act + Assert
        Assert.Throws<UndeclaredIdentifierException>
        (
            () => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, statement)
        );
    }
    
    [Theory]
    [MemberData(nameof(NonMatchingTypes))]
    public void Analyze_VariableTypeDoesNotMatchExpressionType_ThrowsTypeMismatchException(Type variableType, Type expressionType)
    {
        // Arrange
        var statement = new AssignmentStatement
        {
            VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
            Value = new ExpressionMock()
        };
        
        _semanticAnalyzerContextMock
            .Setup(x => x.ResolveSymbol(statement.VariableReference.Identifier.Text))
            .Returns(new Symbol(statement.VariableReference.Identifier, variableType));
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, It.IsAny<Expression>()))
            .Returns(expressionType);
        
        // Act + Assert
        Assert.Throws<TypeMismatchException>
        (
            () => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, statement)
        );
    }
    
    [Theory]
    [MemberData(nameof(PrimitiveTypes))]
    public void Analyze_VariableTypeMatchesExpressionType_ReturnsVoidType(Type type)
    {
        // Arrange
        var statement = new AssignmentStatement
        {
            VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
            Value = new ExpressionMock()
        };
        
        _semanticAnalyzerContextMock
            .Setup(x => x.ResolveSymbol(statement.VariableReference.Identifier.Text))
            .Returns(new Symbol(statement.VariableReference.Identifier, type));
        
        _semanticAnalyzerMock
            .Setup(x => x.Analyze(_semanticAnalyzerContextMock.Object, It.IsAny<Expression>()))
            .Returns(type);
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, statement);
        
        // Assert
        Assert.IsType<VoidType>(result);
    }
}
