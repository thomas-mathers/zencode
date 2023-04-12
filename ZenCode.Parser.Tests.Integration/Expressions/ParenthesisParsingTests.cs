using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class ParenthesisParsingTests
{
    private readonly IParser _parser;

    public ParenthesisParsingTests()
    {
        _parser = new ParserFactory().Create();
    }

    [Fact]
    public void ParseExpression_ParenthesisExpression_ReturnsParenthesisExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_ParenthesisExpressionWithUnary_ReturnsParenthesisExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Minus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new UnaryExpression
            (new Token(TokenType.Minus), new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_ParenthesisExpressionWithBinary_ReturnsParenthesisExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new BinaryExpression
        (
            new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            new Token(TokenType.Plus),
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_ParenthesisExpressionWithFunctionCall_ReturnsParenthesisExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)))
        {
            Arguments = new ExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}