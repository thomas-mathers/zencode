using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Expressions;

namespace ZenCode.Parser.Tests.Expressions;

public class ExpressionParserVariableReferenceExpressionTests
{
    private readonly ExpressionParser _sut;

    public ExpressionParserVariableReferenceExpressionTests()
    {
        _sut = new ExpressionParser();
    }

    [Fact]
    public void Parse_Identifier_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            }
        });

        var expected = new VariableReferenceExpression(
            new Token
            {
                Type = TokenType.Identifier
            },
            Array.Empty<Expression>());

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_ZeroDimensionalArrayReference_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.LeftBracket
            },
            new Token
            {
                Type = TokenType.RightBracket
            }
        });

        // Act + Assert
        Assert.Throws<MissingIndexExpressionException>(() => _sut.Parse(tokenStream));
    }

    [Fact]
    public void Parse_SingleDimensionalArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.LeftBracket
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.RightBracket
            }
        });

        var expected = new VariableReferenceExpression(
            new Token
            {
                Type = TokenType.Identifier
            },
            new[]
            {
                new ConstantExpression(new Token { Type = TokenType.Integer })
            });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_MultiDimensionalArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.LeftBracket
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.Comma
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.Comma
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.RightBracket
            }
        });

        var expected = new VariableReferenceExpression(
            new Token
            {
                Type = TokenType.Identifier
            },
            new[]
            {
                new ConstantExpression(new Token { Type = TokenType.Integer }),
                new ConstantExpression(new Token { Type = TokenType.Integer }),
                new ConstantExpression(new Token { Type = TokenType.Integer })
            });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}