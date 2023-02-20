using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class BinaryExpressionParsingStrategyTests
{
    public static readonly IEnumerable<object[]> BinaryOperatorConstantTokenTypePairs =
        from c1 in TokenTypeGroups.GetConstants()
        from op in TokenTypeGroups.GetBinaryOperators()
        from c2 in TokenTypeGroups.GetConstants()
        select new object[] { c1, op, c2 };

    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly BinaryExpressionParsingStrategy _sut;

    public BinaryExpressionParsingStrategyTests()
    {
        _sut = new BinaryExpressionParsingStrategy(_expressionParserMock.Object, 0);
    }

    [Theory]
    [MemberData(nameof(BinaryOperatorConstantTokenTypePairs))]
    public void Parse_ConstantOpConstant_ReturnsBinaryExpression(TokenType leftConstantTokenType,
        TokenType operatorTokenType, TokenType rightConstantTokenType)
    {
        // Arrange
        var leftExpression = new ConstantExpression(new Token(leftConstantTokenType));

        var tokenStream = new TokenStream(new[]
        {
            new Token(operatorTokenType),
            new Token(rightConstantTokenType)
        });

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(rightConstantTokenType)))
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new BinaryExpression(
            leftExpression,
            new Token(operatorTokenType),
            new ConstantExpression(new Token(rightConstantTokenType)));

        // Act
        var actual = _sut.Parse(tokenStream, leftExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}