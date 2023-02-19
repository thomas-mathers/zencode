using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Statements;

public class StatementParserAssignmentStatementTests
{
    private readonly StatementParser _sut;

    public StatementParserAssignmentStatementTests()
    {
        _sut = new StatementParser(new ExpressionParser());
    }

    [Theory]
    [ClassData(typeof(ConstantTestData))]
    public void Parse_AssignmentToConstant_ReturnsAssignmentStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.Assignment
            },
            new Token
            {
                Type = tokenType
            }
        });

        var expected = new AssignmentStatement(
            new Token { Type = TokenType.Identifier },
            new ConstantExpression(new Token { Type = tokenType }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}