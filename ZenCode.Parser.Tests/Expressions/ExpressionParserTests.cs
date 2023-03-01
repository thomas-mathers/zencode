using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
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
        
        _sut.SetPrefixStrategy(TokenType.BooleanLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixStrategy(TokenType.IntegerLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixStrategy(TokenType.FloatLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixStrategy(TokenType.StringLiteral, new ConstantParsingStrategy());
        _sut.SetPrefixStrategy(TokenType.Identifier, new VariableReferenceParsingStrategy(_sut));
        _sut.SetPrefixStrategy(TokenType.Subtraction, new UnaryExpressionParsingStrategy(_sut));
        _sut.SetPrefixStrategy(TokenType.Not, new UnaryExpressionParsingStrategy(_sut));
        _sut.SetPrefixStrategy(TokenType.LeftParenthesis, new ParenthesisParsingStrategy(_sut));
        
        _sut.SetInfixStrategy(TokenType.Addition, new BinaryExpressionParsingStrategy(_sut, 4));
        _sut.SetInfixStrategy(TokenType.Subtraction, new BinaryExpressionParsingStrategy(_sut, 4));
        _sut.SetInfixStrategy(TokenType.Multiplication, new BinaryExpressionParsingStrategy(_sut, 5));
        _sut.SetInfixStrategy(TokenType.Division, new BinaryExpressionParsingStrategy(_sut, 5));
        _sut.SetInfixStrategy(TokenType.Modulus, new BinaryExpressionParsingStrategy(_sut, 5));
        _sut.SetInfixStrategy(TokenType.Exponentiation, new BinaryExpressionParsingStrategy(_sut, 6, true));
        _sut.SetInfixStrategy(TokenType.LessThan, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixStrategy(TokenType.LessThanOrEqual, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixStrategy(TokenType.Equals, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixStrategy(TokenType.NotEquals, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixStrategy(TokenType.GreaterThan, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixStrategy(TokenType.GreaterThanOrEqual, new BinaryExpressionParsingStrategy(_sut, 3));
        _sut.SetInfixStrategy(TokenType.And, new BinaryExpressionParsingStrategy(_sut, 2));
        _sut.SetInfixStrategy(TokenType.Or, new BinaryExpressionParsingStrategy(_sut, 1));
        _sut.SetInfixStrategy(TokenType.LeftParenthesis, new FunctionCallParsingStrategy(_sut, 7));
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