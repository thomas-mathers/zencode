using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class PrintStatementParsingTests
{
    private readonly IParser _sut;

    public PrintStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_PrintBinaryExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Print),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var expectedStatement = new PrintStatement
        (
            new BinaryExpression
            {
                Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                Operator = BinaryOperatorType.Addition,
                Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

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
    public void Parse_PrintLiteral_ReturnsPrintStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Print),
                new Token(tokenType)
            }
        );

        var expectedStatement = new PrintStatement(new LiteralExpression(new Token(tokenType)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintFunctionCallExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Print),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expectedStatement =
            new PrintStatement
            (
                new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier))
                }
            );

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintParenthesisExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Print),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.StringLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expectedStatement = new PrintStatement(new LiteralExpression(new Token(TokenType.StringLiteral)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintUnaryExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Print),
                new Token(TokenType.Minus),
                new Token(TokenType.FloatLiteral)
            }
        );

        var expectedStatement = new PrintStatement
        (
            new UnaryExpression
            (
                UnaryOperatorType.Negate,
                new LiteralExpression(new Token(TokenType.FloatLiteral))
            )
        );

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintVariableReference_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Print),
                new Token(TokenType.Identifier)
            }
        );

        var expectedStatement = new PrintStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}
