using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
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
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.New),
            new Token(TokenType.Boolean),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket)
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new BooleanType(), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewIntegerArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.New),
            new Token(TokenType.Integer),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket)
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new IntegerType(), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewFloatArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.New),
            new Token(TokenType.Float),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket)
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new FloatType(), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewStringArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.New),
            new Token(TokenType.String),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket)
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new StringType(), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewBooleanJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
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
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new ArrayType(new ArrayType(new BooleanType())), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewIntegerJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
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
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new ArrayType(new ArrayType(new IntegerType())), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewFloatJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
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
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new ArrayType(new ArrayType(new FloatType())), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_NewStringJaggedArray_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
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
        });

        var size = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var expected = new NewArrayExpression(new ArrayType(new ArrayType(new StringType())), size);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}