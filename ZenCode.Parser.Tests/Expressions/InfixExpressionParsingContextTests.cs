using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class InfixExpressionParsingContextTests
{
    private readonly IReadOnlyDictionary<TokenType, Mock<IInfixExpressionParsingStrategy>> _strategyMocks;
    private readonly InfixExpressionParsingContext _sut;

    public InfixExpressionParsingContextTests()
    {
        _strategyMocks = new Dictionary<TokenType, Mock<IInfixExpressionParsingStrategy>>()
        {
            [TokenType.Addition] = new(),
            [TokenType.Subtraction] = new(),
            [TokenType.Multiplication] = new(),
            [TokenType.Division] = new(),
            [TokenType.Modulus] = new(),
            [TokenType.Exponentiation] = new(),
            [TokenType.LessThan] = new(),
            [TokenType.LessThanOrEqual] = new(),
            [TokenType.Equals] = new(),
            [TokenType.NotEquals] = new(),
            [TokenType.GreaterThan] = new(),
            [TokenType.GreaterThanOrEqual] = new(),
            [TokenType.And] = new(),
            [TokenType.Or] = new(),
            [TokenType.LeftParenthesis] = new()
        };

        _sut = new InfixExpressionParsingContext
        {
            Strategies = _strategyMocks.ToDictionary(k => k.Key, v => v.Value.Object)
        };
    }

    [Fact]
    public void Parse_UnknownTokenType_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Not)
        });
        var lOperand = new Expression();
        
        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, lOperand));
    }
    
    [Theory]
    [ClassData(typeof(BinaryOperators))]
    public void Parse_KnownTokenType_ReturnsSameValueAsStrategy(TokenType operatorToken)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(operatorToken)
        });
        
        var lOperand = new Expression();
        var expected = new Expression();

        _strategyMocks[operatorToken].Setup(x => x.Parse(tokenStream, lOperand)).Returns(expected);
        
        // Act
        var actual = _sut.Parse(tokenStream, lOperand);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}