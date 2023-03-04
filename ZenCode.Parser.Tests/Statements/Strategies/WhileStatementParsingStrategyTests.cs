using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class WhileStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IStatementParser> _parserMock = new();
    private readonly WhileStatementParsingStrategy _sut;

    public WhileStatementParsingStrategyTests()
    {
        _sut = new WhileStatementParsingStrategy(_parserMock.Object);
    }

    [Fact]
    public void Parse_ValidInput_ReturnsWhileStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.While),
            new Token(TokenType.None)
        });

        var conditionScope = _fixture.Create<ConditionScope>();

        var expected = new WhileStatement(conditionScope);

        _parserMock
            .Setup(x => x.ParseConditionScope(tokenStream))
            .Returns(conditionScope)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}