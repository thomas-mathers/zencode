using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements;

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

        _expressionParserMock.ReturnsExpression(expression);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}