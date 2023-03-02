using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Integration.TestData;

namespace ZenCode.Parser.Tests.Integration;

public class ParserTests
{
    private readonly Parser _sut;

    public ParserTests()
    {
        _sut = new Parser();

        _sut.SetPrefixExpressionParsingStrategy(TokenType.BooleanLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixExpressionParsingStrategy(TokenType.IntegerLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixExpressionParsingStrategy(TokenType.FloatLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixExpressionParsingStrategy(TokenType.StringLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixExpressionParsingStrategy(TokenType.Identifier, new VariableReferenceParsingStrategy(_sut));
        _sut.SetPrefixExpressionParsingStrategy(TokenType.Subtraction, new UnaryExpressionParsingStrategy(_sut));
        _sut.SetPrefixExpressionParsingStrategy(TokenType.Not, new UnaryExpressionParsingStrategy(_sut));
        _sut.SetPrefixExpressionParsingStrategy(TokenType.LeftParenthesis, new ParenthesisParsingStrategy(_sut));

        _sut.SetInfixExpressionParsingStrategy(TokenType.Addition, new BinaryExpressionParsingStrategy(_sut, 4));
        _sut.SetInfixExpressionParsingStrategy(TokenType.Subtraction, new BinaryExpressionParsingStrategy(_sut, 4));
        _sut.SetInfixExpressionParsingStrategy(TokenType.Multiplication, new BinaryExpressionParsingStrategy(_sut, 5));
        _sut.SetInfixExpressionParsingStrategy(TokenType.Division, new BinaryExpressionParsingStrategy(_sut, 5));
        _sut.SetInfixExpressionParsingStrategy(TokenType.Modulus, new BinaryExpressionParsingStrategy(_sut, 5));
        _sut.SetInfixExpressionParsingStrategy(TokenType.Exponentiation,
            new BinaryExpressionParsingStrategy(_sut, 6, true));
        _sut.SetInfixExpressionParsingStrategy(TokenType.LessThan, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixExpressionParsingStrategy(TokenType.LessThanOrEqual, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixExpressionParsingStrategy(TokenType.Equals, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixExpressionParsingStrategy(TokenType.NotEquals, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixExpressionParsingStrategy(TokenType.GreaterThan, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixExpressionParsingStrategy(TokenType.GreaterThanOrEqual,
            new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixExpressionParsingStrategy(TokenType.And, new BinaryExpressionParsingStrategy(_sut, 2));
        _sut.SetInfixExpressionParsingStrategy(TokenType.Or, new BinaryExpressionParsingStrategy(_sut, 1));
        _sut.SetInfixExpressionParsingStrategy(TokenType.LeftParenthesis, new FunctionCallParsingStrategy(_sut, 7));
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void ParseExpression_LoPrecedenceOpThenHiPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
        TokenType loOp,
        TokenType hiOp)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.IntegerLiteral),
            new Token(loOp),
            new Token(TokenType.IntegerLiteral),
            new Token(hiOp),
            new Token(TokenType.IntegerLiteral)
        });

        var expected = new BinaryExpression(
            new ConstantExpression(new Token(TokenType.IntegerLiteral)),
            new Token(loOp),
            new BinaryExpression(
                new ConstantExpression(new Token(TokenType.IntegerLiteral)),
                new Token(hiOp),
                new ConstantExpression(new Token(TokenType.IntegerLiteral))));

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void ParseExpression_HiPrecedenceOpThenLoPrecedenceOp_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(
        TokenType loOp,
        TokenType hiOp)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.IntegerLiteral),
            new Token(hiOp),
            new Token(TokenType.IntegerLiteral),
            new Token(loOp),
            new Token(TokenType.IntegerLiteral)
        });

        var expected = new BinaryExpression(
            new BinaryExpression(
                new ConstantExpression(new Token(TokenType.IntegerLiteral)),
                new Token(hiOp),
                new ConstantExpression(new Token(TokenType.IntegerLiteral))),
            new Token(loOp),
            new ConstantExpression(new Token(TokenType.IntegerLiteral)));

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LeftAssociativeBinaryOperators))]
    public void ParseExpression_LeftAssociativeOperator_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(
        TokenType op)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.IntegerLiteral),
            new Token(op),
            new Token(TokenType.IntegerLiteral),
            new Token(op),
            new Token(TokenType.IntegerLiteral)
        });

        var expected = new BinaryExpression(
            new BinaryExpression(
                new ConstantExpression(new Token(TokenType.IntegerLiteral)),
                new Token(op),
                new ConstantExpression(new Token(TokenType.IntegerLiteral))),
            new Token(op),
            new ConstantExpression(new Token(TokenType.IntegerLiteral)));

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TokenType.Exponentiation)]
    public void ParseExpression_RightAssociativeOperator_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
        TokenType op)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.IntegerLiteral),
            new Token(op),
            new Token(TokenType.IntegerLiteral),
            new Token(op),
            new Token(TokenType.IntegerLiteral)
        });

        var expected = new BinaryExpression(
            new ConstantExpression(new Token(TokenType.IntegerLiteral)),
            new Token(op),
            new BinaryExpression(
                new ConstantExpression(new Token(TokenType.IntegerLiteral)),
                new Token(op),
                new ConstantExpression(new Token(TokenType.IntegerLiteral))));

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void
        ParseExpression_HiPrecedenceOpThenParenthesizedLoPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
            TokenType hiOp, TokenType loOp)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.IntegerLiteral),
            new Token(hiOp),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.IntegerLiteral),
            new Token(loOp),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightParenthesis)
        });

        var expected = new BinaryExpression(
            new ConstantExpression(new Token(TokenType.IntegerLiteral)),
            new Token(hiOp),
            new BinaryExpression(
                new ConstantExpression(new Token(TokenType.IntegerLiteral)),
                new Token(loOp),
                new ConstantExpression(new Token(TokenType.IntegerLiteral))));

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}