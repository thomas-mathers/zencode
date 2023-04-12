using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class FunctionCallParsingTests
{
    private readonly IParser _sut;

    public FunctionCallParsingTests()
    {
        _sut = new ParserFactory().Create();
    }
    
    [Fact]
    public void ParseExpression_FunctionCallNoParams_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_FunctionCallOneParam_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)))
        {
            Arguments = new ExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_FunctionCallThreeParams_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Comma),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Comma),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)))
        {
            Arguments = new ExpressionList
            (
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            )
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
