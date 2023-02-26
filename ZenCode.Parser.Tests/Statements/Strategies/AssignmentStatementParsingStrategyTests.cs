using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class AssignmentStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly AssignmentStatementParsingStrategy _sut;

    public AssignmentStatementParsingStrategyTests()
    {
        _sut = new AssignmentStatementParsingStrategy(_expressionParserMock.Object);
    }

    [Fact]
    public void Parse_AssignmentToConstant_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.None)
        });

        var expression = _fixture.Create<Expression>();
        var expected = new AssignmentStatement(
            new Token(TokenType.Identifier),
            expression);

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
            .Returns(expression)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}