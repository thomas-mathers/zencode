using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class NewExpressionParsingStrategyTests
{
    private readonly Mock<ITypeParser> _typeParserMock = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly NewExpressionParsingStrategy _sut;
    private readonly Fixture _fixture = new();
    
    public NewExpressionParsingStrategyTests()
    {
        _sut = new NewExpressionParsingStrategy(_typeParserMock.Object, _expressionParserMock.Object);
    }

    [Fact]
    public void Parse_AnyTypeAnyExpressionList_ReturnsNewExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.New),
            new Token(TokenType.Unknown),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.Unknown),
            new Token(TokenType.RightBracket),
        });

        var expected = _fixture.Create<NewExpression>();
        
        _typeParserMock
            .Setup(x => x.ParseType(tokenStream, 0))
            .Returns(expected.Type)
            .ConsumesToken(tokenStream);

        _expressionParserMock
            .Setup(x => x.ParseExpressionList(tokenStream))
            .Returns(expected.ExpressionList)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}