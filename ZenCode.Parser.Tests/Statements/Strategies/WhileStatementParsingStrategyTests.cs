using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class WhileStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly WhileStatementParsingStrategy _sut;

    public WhileStatementParsingStrategyTests()
    {
        _sut = new WhileStatementParsingStrategy();
    }

    [Fact]
    public void Parse_ValidInput_ReturnsWhileStatement()
    {
        // Arrange
        var conditionScope = _fixture.Create<ConditionScope>();
        var expected = new WhileStatement(conditionScope);

        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .Returns(conditionScope);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.While));
    }
}