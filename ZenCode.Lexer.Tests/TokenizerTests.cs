using Xunit;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Tests;

public class TokenizerTests
{
    private readonly Tokenizer _sut = new();

    [Theory]
    [InlineData(":=", TokenType.Assignment)]
    [InlineData("+", TokenType.Addition)]
    [InlineData("-", TokenType.Subtraction)]
    [InlineData("*", TokenType.Multiplication)]
    [InlineData("/", TokenType.Division)]
    [InlineData("mod", TokenType.Modulus)]
    [InlineData("^", TokenType.Exponentiation)]
    [InlineData("<", TokenType.LessThan)]
    [InlineData("<=", TokenType.LessThanOrEqual)]
    [InlineData("=", TokenType.Equals)]
    [InlineData("!=", TokenType.NotEquals)]
    [InlineData(">", TokenType.GreaterThan)]
    [InlineData(">=", TokenType.GreaterThanOrEqual)]
    [InlineData("and", TokenType.And)]
    [InlineData("or", TokenType.Or)]
    [InlineData("not", TokenType.Not)]
    [InlineData("(", TokenType.LeftParenthesis)]
    [InlineData(")", TokenType.RightParenthesis)]
    [InlineData("true", TokenType.Boolean)]
    [InlineData("TRUE", TokenType.Boolean)]
    [InlineData("false", TokenType.Boolean)]
    [InlineData("FALSE", TokenType.Boolean)]
    [InlineData("[", TokenType.LeftBracket)]
    [InlineData("]", TokenType.RightBracket)]
    public void Tokenize_ValidToken_ReturnsToken(string text, TokenType expectedTokenType)
    {
        // Arrange
        var expectedToken = new Token
        {
            Type = expectedTokenType,
            Text = text
        };

        // Act
        var tokens = _sut.Tokenize(text).ToList();

        // Assert
        Assert.NotNull(tokens);
        Assert.Single(tokens);
        Assert.Equal(expectedToken, tokens.First());
    }

    [Theory]
    [InlineData("abc123")]
    public void Tokenize_ValidIdentifier_ReturnsIdentifier(string text)
    {
        // Arrange
        var expectedToken = new Token
        {
            Type = TokenType.Identifier,
            Text = text
        };

        // Act
        var tokens = _sut.Tokenize(text).ToList();

        // Assert
        Assert.NotNull(tokens);
        Assert.Single(tokens);
        Assert.Equal(expectedToken, tokens.First());
    }

    [Theory]
    [InlineData("abc123.")]
    [InlineData("a$bc")]
    [InlineData("abc&123")]
    public void Tokenize_InvalidIdentifier_ThrowsTokenParseException(string code)
    {
        // Arrange + Act + Assert
        Assert.Throws<TokenParseException>(() => _sut.Tokenize(code).ToList());
    }

    [Theory]
    [InlineData("-2147483648")]
    [InlineData("0")]
    [InlineData("2147483647")]
    public void Tokenize_ValidInteger_ReturnsInteger(string text)
    {
        // Arrange
        var expectedToken = new Token
        {
            Type = TokenType.Integer,
            Text = text
        };

        // Act
        var tokens = _sut.Tokenize(text).ToList();

        // Assert
        Assert.NotNull(tokens);
        Assert.Single(tokens);
        Assert.Equal(expectedToken, tokens.First());
    }

    [Theory]
    [InlineData("-3.40282346638528859e+38")]
    [InlineData("3.1415926535897932385")]
    [InlineData("3.40282346638528859e+38")]
    public void Tokenize_ValidFloat_ReturnsFloat(string text)
    {
        // Arrange
        var expectedToken = new Token
        {
            Type = TokenType.Float,
            Text = text
        };

        // Act
        var tokens = _sut.Tokenize(text).ToList();

        // Assert
        Assert.NotNull(tokens);
        Assert.Single(tokens);
        Assert.Equal(expectedToken, tokens.First());
    }

    [Theory]
    [InlineData("123!")]
    [InlineData("1$23")]
    public void Tokenize_InvalidNumber_ThrowsTokenParseException(string text)
    {
        // Arrange + Act + Assert
        Assert.Throws<TokenParseException>(() => _sut.Tokenize(text).ToList());
    }

    [Fact]
    public void Tokenize_SingleLineOfTokens_ReturnsCorrectSequenceOfTokens()
    {
        // Arrange
        var expectedTokens = new[]
        {
            new Token
            {
                Type = TokenType.Identifier,
                Line = 0,
                StartingColumn = 0,
                Text = "x"
            },
            new Token
            {
                Type = TokenType.Assignment,
                Line = 0,
                StartingColumn = 2,
                Text = ":="
            },
            new Token
            {
                Type = TokenType.Identifier,
                Line = 0,
                StartingColumn = 5,
                Text = "a"
            },
            new Token
            {
                Type = TokenType.Addition,
                Line = 0,
                StartingColumn = 7,
                Text = "+"
            },
            new Token
            {
                Type = TokenType.Identifier,
                Line = 0,
                StartingColumn = 9,
                Text = "b"
            }
        };

        // Act
        var actualTokens = _sut.Tokenize("x := a + b").ToList();

        // Assert
        Assert.Equal(expectedTokens, actualTokens);
    }

    [Theory]
    [InlineData("a := 1.25\rb := 3.75\rc := (a * b) ^ 3 + 2")]
    [InlineData("a := 1.25\nb := 3.75\nc := (a * b) ^ 3 + 2")]
    [InlineData("a := 1.25\r\nb := 3.75\r\nc := (a * b) ^ 3 + 2")]
    public void Tokenize_MultipleLinesOfTokens_ReturnsCorrectSequenceOfTokens(string code)
    {
        // Arrange
        var expectedTokens = new[]
        {
            new Token
            {
                Type = TokenType.Identifier,
                Line = 0,
                StartingColumn = 0,
                Text = "a"
            },
            new Token
            {
                Type = TokenType.Assignment,
                Line = 0,
                StartingColumn = 2,
                Text = ":="
            },
            new Token
            {
                Type = TokenType.Float,
                Line = 0,
                StartingColumn = 5,
                Text = "1.25"
            },
            new Token
            {
                Type = TokenType.Identifier,
                Line = 1,
                StartingColumn = 0,
                Text = "b"
            },
            new Token
            {
                Type = TokenType.Assignment,
                Line = 1,
                StartingColumn = 2,
                Text = ":="
            },
            new Token
            {
                Type = TokenType.Float,
                Line = 1,
                StartingColumn = 5,
                Text = "3.75"
            },
            new Token
            {
                Type = TokenType.Identifier,
                Line = 2,
                StartingColumn = 0,
                Text = "c"
            },
            new Token
            {
                Type = TokenType.Assignment,
                Line = 2,
                StartingColumn = 2,
                Text = ":="
            },
            new Token
            {
                Type = TokenType.LeftParenthesis,
                Line = 2,
                StartingColumn = 5,
                Text = "("
            },
            new Token
            {
                Type = TokenType.Identifier,
                Line = 2,
                StartingColumn = 6,
                Text = "a"
            },
            new Token
            {
                Type = TokenType.Multiplication,
                Line = 2,
                StartingColumn = 8,
                Text = "*"
            },
            new Token
            {
                Type = TokenType.Identifier,
                Line = 2,
                StartingColumn = 10,
                Text = "b"
            },
            new Token
            {
                Type = TokenType.RightParenthesis,
                Line = 2,
                StartingColumn = 11,
                Text = ")"
            },
            new Token
            {
                Type = TokenType.Exponentiation,
                Line = 2,
                StartingColumn = 13,
                Text = "^"
            },
            new Token
            {
                Type = TokenType.Integer,
                Line = 2,
                StartingColumn = 15,
                Text = "3"
            },
            new Token
            {
                Type = TokenType.Addition,
                Line = 2,
                StartingColumn = 17,
                Text = "+"
            },
            new Token
            {
                Type = TokenType.Integer,
                Line = 2,
                StartingColumn = 19,
                Text = "2"
            }
        };

        // Act
        var actualTokens = _sut.Tokenize(code).ToList();

        // Assert
        Assert.Equal(expectedTokens, actualTokens);
    }
}