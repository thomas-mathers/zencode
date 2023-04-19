using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;

namespace ZenCode.SemanticAnalysis.Tests;

public class SemanticAnalyzerTests
{
    private readonly SemanticAnalyzer _sut = new();

    [Fact]
    public void Analyze_NullProgram_ThrowsArgumentNullException()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _sut.Analyze(null));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_DuplicateVariableDeclaration_ThrowsDuplicateIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        // Act
        var exception = Assert.Throws<DuplicateIdentifierException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_UndeclaredVariableReference_ThrowsUndeclaredIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                Value = new VariableReferenceExpression(new Token(TokenType.Identifier, "y"))
            }
        );

        // Act
        var exception = Assert.Throws<UndeclaredIdentifierException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_InvokingNonFunction_ThrowsInvokingNonFunctionTypeException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "y"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "y"))
                }
            }
        );

        // Act
        var exception = Assert.Throws<InvokingNonFunctionTypeException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_FunctionCallWithIncorrectNumberOfParameters_ThrowsIncorrectNumberOfParametersException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                ReturnType = new VoidType(),
                Name = new Token(TokenType.Identifier, "f"),
                Parameters = new ParameterList
                (
                    new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                )
            },
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "f")),
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                        new LiteralExpression(new Token(TokenType.IntegerLiteral))
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<IncorrectNumberOfParametersException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_FunctionCallWithIncorrectParameterType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                ReturnType = new VoidType(),
                Name = new Token(TokenType.Identifier, "f"),
                Parameters = new ParameterList
                (
                    new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                )
            },
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "f")),
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.StringLiteral))
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
}
