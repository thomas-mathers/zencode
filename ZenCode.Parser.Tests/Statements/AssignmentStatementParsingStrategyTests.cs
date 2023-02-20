using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Statements;

public class AssignmentStatementParsingStrategyTests
{
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly AssignmentStatementParsingStrategy _sut;

    public AssignmentStatementParsingStrategyTests()
    {
        _sut = new AssignmentStatementParsingStrategy(_expressionParserMock.Object);
    }

    [Theory]
    [ClassData(typeof(ConstantTestData))]
    public void Parse_AssignmentToConstant_ReturnsAssignmentStatement(TokenType constantType)
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
                Type = constantType
            }
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token { Type = constantType }))
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new AssignmentStatement(
            new Token { Type = TokenType.Identifier },
            new ConstantExpression(new Token { Type = constantType }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}