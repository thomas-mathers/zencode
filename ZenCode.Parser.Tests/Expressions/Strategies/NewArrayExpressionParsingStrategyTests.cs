using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class NewArrayExpressionParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly NewArrayExpressionParsingStrategy _sut;
    
    public NewArrayExpressionParsingStrategyTests()
    {
        _sut = new NewArrayExpressionParsingStrategy();
        
        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Expression),
                typeof(ExpressionMock)));
        
        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Type),
                typeof(TypeMock)));
    }

    [Fact]
    public void Parse_AnyTypeAnyExpressionList_ReturnsNewExpression()
    {
        // Arrange
        var expected = _fixture.Create<NewArrayExpression>();

        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Returns(expected.Type);

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(expected.Size);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.New));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.LeftBracket));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.RightBracket));
    }
}