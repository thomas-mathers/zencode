using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Statements;

namespace ZenCode.Parser.Tests.Statements;

public class IfStatementParsingStrategyTests
{
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly IfStatementParsingStrategy _sut;

    public IfStatementParsingStrategyTests()
    {
        _sut = new IfStatementParsingStrategy(_statementParserMock.Object, _expressionParserMock.Object);
    }

    [Fact]
    public void Parse_EmptyBody_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.If
            },
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.GreaterThan
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.LeftBrace
            },
            new Token
            {
                Type = TokenType.RightBrace
            }
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new BinaryExpression(
                new VariableReferenceExpression
                {
                    Identifier = new Token
                    {
                        Type = TokenType.Identifier
                    }
                },
                new Token
                {
                    Type = TokenType.GreaterThan
                },
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                })))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
                tokenStream.Consume();
                tokenStream.Consume();
            });

        var expected = new IfStatement(
            new BinaryExpression(
                new VariableReferenceExpression
                {
                    Identifier = new Token
                    {
                        Type = TokenType.Identifier
                    }
                },
                new Token
                {
                    Type = TokenType.GreaterThan
                },
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                })), 
            Array.Empty<Statement>());
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}