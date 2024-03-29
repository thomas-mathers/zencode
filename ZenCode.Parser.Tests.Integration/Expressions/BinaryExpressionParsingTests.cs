using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Mappers;
using ZenCode.Parser.Tests.Integration.TestData;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class BinaryExpressionParsingTests
{
    private readonly IParser _sut;

    public BinaryExpressionParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Theory]
    [ClassData(typeof(LeftAssociativeBinaryOperators))]
    public void ParseExpression_LeftAssociativeOperator_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst
    (
        TokenType op
    )
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var expected = new BinaryExpression
        {
            Left = new BinaryExpression
            {
                Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                Operator = TokenTypeToBinaryOperatorTypeMapper.Map(op),
                Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            Operator = TokenTypeToBinaryOperatorTypeMapper.Map(op),
            Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TokenType.Exponentiation)]
    public void ParseExpression_RightAssociativeOperator_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst
    (
        TokenType op
    )
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var expected = new BinaryExpression
        {
            Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            Operator = TokenTypeToBinaryOperatorTypeMapper.Map(op),
            Right = new BinaryExpression
            {
                Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                Operator = TokenTypeToBinaryOperatorTypeMapper.Map(op),
                Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void ParseExpression_LoPrecedenceOpThenHiPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst
    (
        TokenType loOp,
        TokenType hiOp
    )
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(loOp),
                new Token(TokenType.IntegerLiteral),
                new Token(hiOp),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var expected = new BinaryExpression
        {
            Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            Operator = TokenTypeToBinaryOperatorTypeMapper.Map(loOp),
            Right = new BinaryExpression
            {
                Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                Operator = TokenTypeToBinaryOperatorTypeMapper.Map(hiOp),
                Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void ParseExpression_HiPrecedenceOpThenLoPrecedenceOp_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst
    (
        TokenType loOp,
        TokenType hiOp
    )
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(hiOp),
                new Token(TokenType.IntegerLiteral),
                new Token(loOp),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var expected = new BinaryExpression
        {
            Left = new BinaryExpression
            {
                Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                Operator = TokenTypeToBinaryOperatorTypeMapper.Map(hiOp),
                Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            Operator = TokenTypeToBinaryOperatorTypeMapper.Map(loOp),
            Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void
        ParseExpression_HiPrecedenceOpThenParenthesizedLoPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst
        (
            TokenType hiOp,
            TokenType loOp
        )
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(hiOp),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.IntegerLiteral),
                new Token(loOp),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expected = new BinaryExpression
        {
            Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            Operator = TokenTypeToBinaryOperatorTypeMapper.Map(hiOp),
            Right = new BinaryExpression
            {
                Left = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                Operator = TokenTypeToBinaryOperatorTypeMapper.Map(loOp),
                Right = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_MissingRightOperand_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Plus),
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}
