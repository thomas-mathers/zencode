using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class VariableDeclarationStatementParsingTests
{
    private readonly IParser _sut;

    public VariableDeclarationStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignBinaryExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var identifier = new Token(TokenType.Identifier);

        var expression = new BinaryExpression
        {
            Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            Operator = BinaryOperatorType.Addition,
            Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var expectedStatement = new VariableDeclarationStatement
        {
            VariableName = identifier,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Theory]
    [InlineData(TokenType.BooleanLiteral)]
    [InlineData(TokenType.IntegerLiteral)]
    [InlineData(TokenType.FloatLiteral)]
    [InlineData(TokenType.StringLiteral)]
    public void Parse_DeclareVariableAndAssignLiteral_ReturnsVariableDeclarationStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(tokenType)
            }
        );

        var identifier = new Token(TokenType.Identifier);

        var expression = new LiteralExpression(new Token(tokenType));

        var expectedStatement = new VariableDeclarationStatement
        {
            VariableName = identifier,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignFunctionCallExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis)
            }
        );

        var identifier = new Token(TokenType.Identifier);

        var expression = new FunctionCallExpression
        {
            FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier))
        };

        var expectedStatement = new VariableDeclarationStatement
        {
            VariableName = identifier,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignParenthesisExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.StringLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var identifier = new Token(TokenType.Identifier);

        var expression = new LiteralExpression(new Token(TokenType.StringLiteral));

        var expectedStatement = new VariableDeclarationStatement
        {
            VariableName = identifier,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignUnaryExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Minus),
                new Token(TokenType.FloatLiteral)
            }
        );

        var identifier = new Token(TokenType.Identifier);

        var expression = new UnaryExpression
        (
            UnaryOperatorType.Negate,
            new LiteralExpression(new Token(TokenType.FloatLiteral))
        );

        var expectedStatement = new VariableDeclarationStatement
        {
            VariableName = identifier,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignVariableReferenceExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier)
            }
        );

        var identifier = new Token(TokenType.Identifier);

        var expression = new VariableReferenceExpression(new Token(TokenType.Identifier));

        var expectedStatement = new VariableDeclarationStatement
        {
            VariableName = identifier,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_MissingIdentifier_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected 'Identifier', got ':='", exception.Message);
    }

    [Fact]
    public void Parse_MissingAssignment_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.IntegerLiteral)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected ':=', got 'IntegerLiteral'", exception.Message);
    }

    [Fact]
    public void Parse_MissingExpression_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment)
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}
