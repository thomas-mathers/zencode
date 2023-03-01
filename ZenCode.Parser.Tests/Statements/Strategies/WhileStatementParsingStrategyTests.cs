using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class WhileStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly WhileStatementParsingStrategy _sut;

    public WhileStatementParsingStrategyTests()
    {
        _sut = new WhileStatementParsingStrategy(_expressionParserMock.Object, _statementParserMock.Object);
    }

    [Fact]
    public void Parse_ValidInput_ReturnsWhileStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.While),
            new Token(TokenType.None),
            new Token(TokenType.None)
        });

        var condition = _fixture.Create<Expression>();
        var scope = _fixture.Create<Scope>();

        var expected = new WhileStatement(new ConditionScope(condition, scope));

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
            .Returns(condition)
            .ConsumesToken(tokenStream);
        
        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .Returns(scope)
            .ConsumesToken(tokenStream);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}