using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Tests;

public class TokenizerTests
{
    private readonly ITokenizer _sut = new TokenizerFactory().Create();

    [Theory]
    [InlineData("!=", TokenType.NotEquals)]
    [InlineData("(", TokenType.LeftParenthesis)]
    [InlineData(")", TokenType.RightParenthesis)]
    [InlineData("*", TokenType.Multiplication)]
    [InlineData("+", TokenType.Addition)]
    [InlineData("-", TokenType.Subtraction)]
    [InlineData("/", TokenType.Division)]
    [InlineData(":", TokenType.Colon)]
    [InlineData(":=", TokenType.Assignment)]
    [InlineData("<", TokenType.LessThan)]
    [InlineData("<=", TokenType.LessThanOrEqual)]
    [InlineData("=", TokenType.Equals)]
    [InlineData("=>", TokenType.RightArrow)]
    [InlineData(">", TokenType.GreaterThan)]
    [InlineData(">=", TokenType.GreaterThanOrEqual)]
    [InlineData("FALSE", TokenType.BooleanLiteral)]
    [InlineData("TRUE", TokenType.BooleanLiteral)]
    [InlineData("[", TokenType.LeftBracket)]
    [InlineData("]", TokenType.RightBracket)]
    [InlineData("^", TokenType.Exponentiation)]
    [InlineData("and", TokenType.And)]
    [InlineData("bool", TokenType.Boolean)]
    [InlineData("false", TokenType.BooleanLiteral)]
    [InlineData("float", TokenType.Float)]
    [InlineData("function", TokenType.Function)]
    [InlineData("int", TokenType.Integer)]
    [InlineData("mod", TokenType.Modulus)]
    [InlineData("not", TokenType.Not)]
    [InlineData("or", TokenType.Or)]
    [InlineData("print", TokenType.Print)]
    [InlineData("return", TokenType.Return)]
    [InlineData("string", TokenType.String)]
    [InlineData("true", TokenType.BooleanLiteral)]
    [InlineData("var", TokenType.Var)]
    [InlineData("while", TokenType.While)]
    public void Tokenize_ValidToken_ReturnsToken(string text, TokenType expectedTokenType)
    {
        // Arrange
        var expectedToken = new Token(expectedTokenType)
        {
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
        var expectedToken = new Token(TokenType.Identifier)
        {
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
        var expectedToken = new Token(TokenType.IntegerLiteral)
        {
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
        var expectedToken = new Token(TokenType.FloatLiteral)
        {
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

    [Theory]
    [InlineData("''")]
    [InlineData("'Hello world'")]
    [InlineData("'Thomas's compiler'")]
    public void Tokenize_ValidString_ReturnsString(string text)
    {
        // Arrange
        var expectedToken = new Token(TokenType.StringLiteral)
        {
            Text = text
        };

        // Act
        var tokens = _sut.Tokenize(text).ToList();

        // Assert
        Assert.NotNull(tokens);
        Assert.Single(tokens);
        Assert.Equal(expectedToken, tokens.First());
    }

    [Fact]
    public void Tokenize_SingleLineOfTokens_ReturnsCorrectSequenceOfTokens()
    {
        // Arrange
        var expectedTokens = new[]
        {
            new Token(TokenType.Identifier)
            {
                Line = 0,
                StartingColumn = 0,
                Text = "x"
            },
            new Token(TokenType.Assignment)
            {
                Line = 0,
                StartingColumn = 2,
                Text = ":="
            },
            new Token(TokenType.Identifier)
            {
                Line = 0,
                StartingColumn = 5,
                Text = "a"
            },
            new Token(TokenType.Addition)
            {
                Line = 0,
                StartingColumn = 7,
                Text = "+"
            },
            new Token(TokenType.Identifier)
            {
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
            new Token(TokenType.Identifier)
            {
                Line = 0,
                StartingColumn = 0,
                Text = "a"
            },
            new Token(TokenType.Assignment)
            {
                Line = 0,
                StartingColumn = 2,
                Text = ":="
            },
            new Token(TokenType.FloatLiteral)
            {
                Line = 0,
                StartingColumn = 5,
                Text = "1.25"
            },
            new Token(TokenType.Identifier)
            {
                Line = 1,
                StartingColumn = 0,
                Text = "b"
            },
            new Token(TokenType.Assignment)
            {
                Line = 1,
                StartingColumn = 2,
                Text = ":="
            },
            new Token(TokenType.FloatLiteral)
            {
                Line = 1,
                StartingColumn = 5,
                Text = "3.75"
            },
            new Token(TokenType.Identifier)
            {
                Line = 2,
                StartingColumn = 0,
                Text = "c"
            },
            new Token(TokenType.Assignment)
            {
                Line = 2,
                StartingColumn = 2,
                Text = ":="
            },
            new Token(TokenType.LeftParenthesis)
            {
                Line = 2,
                StartingColumn = 5,
                Text = "("
            },
            new Token(TokenType.Identifier)
            {
                Line = 2,
                StartingColumn = 6,
                Text = "a"
            },
            new Token(TokenType.Multiplication)
            {
                Line = 2,
                StartingColumn = 8,
                Text = "*"
            },
            new Token(TokenType.Identifier)
            {
                Line = 2,
                StartingColumn = 10,
                Text = "b"
            },
            new Token(TokenType.RightParenthesis)
            {
                Line = 2,
                StartingColumn = 11,
                Text = ")"
            },
            new Token(TokenType.Exponentiation)
            {
                Line = 2,
                StartingColumn = 13,
                Text = "^"
            },
            new Token(TokenType.IntegerLiteral)
            {
                Line = 2,
                StartingColumn = 15,
                Text = "3"
            },
            new Token(TokenType.Addition)
            {
                Line = 2,
                StartingColumn = 17,
                Text = "+"
            },
            new Token(TokenType.IntegerLiteral)
            {
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