using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class NewArrayExpressionParsingTests
{
    private readonly IParser _sut;

    public NewArrayExpressionParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void ParseExpression_NewBooleanArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Boolean),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new BooleanType(),
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewIntegerArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new IntegerType(),
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewFloatArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Float),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new FloatType(),
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewStringArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.String),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new StringType(),
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewBooleanJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Boolean),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new ArrayType
            {
                BaseType = new ArrayType
                {
                    BaseType = new BooleanType()
                }
            },
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewIntegerJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new ArrayType
            {
                BaseType = new ArrayType
                {
                    BaseType = new IntegerType()
                }
            },
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewFloatJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Float),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new ArrayType
            {
                BaseType = new ArrayType
                {
                    BaseType = new FloatType()
                }
            },
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewStringJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.String),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression
        {
            Type = new ArrayType
            {
                BaseType = new ArrayType
                {
                    BaseType = new StringType()
                }
            },
            Size = size
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_MissingType_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Unexpected token '['", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingRightBracket_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.LeftBracket),
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Expected ']', got '['", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingArraySize_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}
