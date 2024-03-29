using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class ReturnStatementParsingTests
{
    private readonly IParser _sut;

    public ReturnStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_ReturnNothing_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Return),
                new Token(TokenType.Semicolon)
            }
        );

        var expectedStatement = new ReturnStatement();

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnBinaryExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Return),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon)
            }
        );

        var expectedStatement = new ReturnStatement
        {
            Value = new BinaryExpression
            {
                Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                Operator = BinaryOperatorType.Addition,
                Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
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
    public void Parse_ReturnLiteral_ReturnsReturnStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Return),
                new Token(tokenType),
                new Token(TokenType.Semicolon)
            }
        );

        var expectedStatement = new ReturnStatement { Value = new LiteralExpression(new Token(tokenType)) };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnFunctionCallExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.Semicolon)
            }
        );

        var expectedStatement = new ReturnStatement
        {
            Value = new FunctionCallExpression
            {
                FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier))
            }
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnParenthesisExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Return),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.StringLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.Semicolon)
            }
        );

        var expectedStatement = new ReturnStatement
        {
            Value = new LiteralExpression(new Token(TokenType.StringLiteral))
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnUnaryExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Return),
                new Token(TokenType.Minus),
                new Token(TokenType.FloatLiteral),
                new Token(TokenType.Semicolon)
            }
        );

        var expectedStatement = new ReturnStatement
        {
            Value = new UnaryExpression 
            {
                Operator = UnaryOperatorType.Negate,
                Expression = new LiteralExpression(new Token(TokenType.FloatLiteral))
            }
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnVariableReference_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon)
            }
        );

        var expectedStatement = new ReturnStatement
        {
            Value = new VariableReferenceExpression(new Token(TokenType.Identifier))
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}
