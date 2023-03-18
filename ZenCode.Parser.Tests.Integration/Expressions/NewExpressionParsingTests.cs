using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class NewExpressionParsingTests
{
    private readonly IParser _sut;

    public NewExpressionParsingTests()
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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new BooleanType(), expressionList);

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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new IntegerType(), expressionList);

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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new FloatType(), expressionList);

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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new StringType(), expressionList);

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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new ArrayType(new ArrayType(new BooleanType())), expressionList);

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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new ArrayType(new ArrayType(new IntegerType())), expressionList);

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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new ArrayType(new ArrayType(new FloatType())), expressionList);

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

        var expressionList = new ExpressionList
        {
            Expressions = new[]
            {
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var expected = new NewExpression(new ArrayType(new ArrayType(new StringType())), expressionList);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}