using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements;

public class WhileStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IConditionScopeParser> _conditionScopeParserMock = new();
    private readonly WhileStatementParsingStrategy _sut;

    public WhileStatementParsingStrategyTests()
    {
        _sut = new WhileStatementParsingStrategy(_conditionScopeParserMock.Object);
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

        _conditionScopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(conditionScope)
            .ConsumesToken(tokenStream);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}