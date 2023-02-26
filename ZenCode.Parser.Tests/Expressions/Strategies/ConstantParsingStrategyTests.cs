using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class ConstantParsingStrategyTests
{
    private readonly ConstantParsingStrategy _sut;

    public ConstantParsingStrategyTests()
    {
        _sut = new ConstantParsingStrategy();
    }

    [Theory]
    [ClassData(typeof(Constants))]
    public void Parse_Constant_ReturnsConstantExpression(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(tokenType)
        });

        var expected = new ConstantExpression(new Token(tokenType));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}