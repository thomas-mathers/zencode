using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class ExpressionParserTests
{
    private readonly ExpressionParser _sut;

    public ExpressionParserTests()
    {
        _sut = new ExpressionParser();
        
        var expressionListParser = new ArgumentListParser(_sut);

        _sut.PrefixStrategies = new Dictionary<TokenType, IPrefixExpressionParsingStrategy>
        {
            [TokenType.BooleanLiteral] = new ConstantParsingStrategy(),
            [TokenType.IntegerLiteral] = new ConstantParsingStrategy(),
            [TokenType.FloatLiteral] = new ConstantParsingStrategy(),
            [TokenType.StringLiteral] = new ConstantParsingStrategy(),
            [TokenType.Identifier] = new VariableReferenceParsingStrategy(expressionListParser),
            [TokenType.Subtraction] = new UnaryExpressionParsingStrategy(_sut),
            [TokenType.Not] = new UnaryExpressionParsingStrategy(_sut),
            [TokenType.LeftParenthesis] = new ParenthesisParsingStrategy(_sut)
        };

        _sut.InfixStrategies = new Dictionary<TokenType, IInfixExpressionParsingStrategy>
        {
            [TokenType.Addition] = new BinaryExpressionParsingStrategy(_sut, 4),
            [TokenType.Subtraction] = new BinaryExpressionParsingStrategy(_sut, 4),
            [TokenType.Multiplication] = new BinaryExpressionParsingStrategy(_sut, 5),
            [TokenType.Division] = new BinaryExpressionParsingStrategy(_sut, 5),
            [TokenType.Modulus] = new BinaryExpressionParsingStrategy(_sut, 5),
            [TokenType.Exponentiation] = new BinaryExpressionParsingStrategy(_sut, 6, true),
            [TokenType.LessThan] = new BinaryExpressionParsingStrategy(_sut, 3),
            [TokenType.LessThanOrEqual] = new BinaryExpressionParsingStrategy(_sut, 3),
            [TokenType.Equals] = new BinaryExpressionParsingStrategy(_sut, 3),
            [TokenType.NotEquals] = new BinaryExpressionParsingStrategy(_sut, 3),
            [TokenType.GreaterThan] = new BinaryExpressionParsingStrategy(_sut, 3),
            [TokenType.GreaterThanOrEqual] = new BinaryExpressionParsingStrategy(_sut, 3),
            [TokenType.And] = new BinaryExpressionParsingStrategy(_sut, 2),
            [TokenType.Or] = new BinaryExpressionParsingStrategy(_sut, 1),
            [TokenType.LeftParenthesis] = new FunctionCallParsingStrategy(expressionListParser, 7)
        };
    }
    
    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void Parse_LoPrecedenceOpThenHiPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
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
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void Parse_HiPrecedenceOpThenLoPrecedenceOp_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(
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
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LeftAssociativeBinaryOperators))]
    public void Parse_LeftAssociativeOperator_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(TokenType op)
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
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TokenType.Exponentiation)]
    public void Parse_RightAssociativeOperator_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(TokenType op)
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
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
    public void Parse_HiPrecedenceOpThenParenthesizedLoPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
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
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}