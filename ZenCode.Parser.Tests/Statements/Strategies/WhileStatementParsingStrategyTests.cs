using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class WhileStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
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
        var conditionScope = _fixture.Create<ConditionScope>();
        var expected = new WhileStatement(conditionScope);

        _expressionParserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(conditionScope.Condition);

        _statementParserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(conditionScope.Scope);

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.While));
    }
}