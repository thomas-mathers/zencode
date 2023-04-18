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
            new[]
            {
                new VariableDeclarationStatement
                    (new Token(TokenType.Identifier, "x"), new LiteralExpression(new Token(TokenType.IntegerLiteral))),
                new VariableDeclarationStatement
                    (new Token(TokenType.Identifier, "x"), new LiteralExpression(new Token(TokenType.IntegerLiteral)))
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
            new[]
            {
                new VariableDeclarationStatement
                (
                    new Token(TokenType.Identifier, "x"),
                    new VariableReferenceExpression(new Token(TokenType.Identifier, "y"))
                )
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
            new[]
            {
                new VariableDeclarationStatement
                (
                    new Token(TokenType.Identifier, "y"),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                ),
                new VariableDeclarationStatement
                (
                    new Token(TokenType.Identifier, "x"),
                    new FunctionCallExpression
                    (
                        new VariableReferenceExpression(new Token(TokenType.Identifier, "y"))
                    )
                )
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
            new Statement[]
            {
                new FunctionDeclarationStatement
                (
                    new VoidType(),
                    new Token(TokenType.Identifier, "f"),
                    new ParameterList
                    (
                        new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                    ),
                    new Scope()
                ),
                new VariableDeclarationStatement
                (
                    new Token(TokenType.Identifier, "x"),
                    new FunctionCallExpression
                    (
                        new VariableReferenceExpression(new Token(TokenType.Identifier, "f"))
                    )
                    {
                        Arguments = new ExpressionList
                        (
                            new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                            new LiteralExpression(new Token(TokenType.IntegerLiteral))
                        )
                    }
                )
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
            new Statement[]
            {
                new FunctionDeclarationStatement
                (
                    new VoidType(),
                    new Token(TokenType.Identifier, "f"),
                    new ParameterList
                    (
                        new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                    ),
                    new Scope()
                ),
                new VariableDeclarationStatement
                (
                    new Token(TokenType.Identifier, "x"),
                    new FunctionCallExpression
                    (
                        new VariableReferenceExpression(new Token(TokenType.Identifier, "f"))
                    )
                    {
                        Arguments = new ExpressionList
                        (
                            new LiteralExpression(new Token(TokenType.StringLiteral))
                        )
                    }
                )
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
}
