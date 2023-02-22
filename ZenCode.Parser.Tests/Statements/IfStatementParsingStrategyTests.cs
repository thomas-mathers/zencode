using AutoFixture;
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
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements;

public class IfStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly IfStatementParsingStrategy _sut;

    public IfStatementParsingStrategyTests()
    {
        _sut = new IfStatementParsingStrategy(_statementParserMock.Object, _expressionParserMock.Object);
    }

    [Fact]
    public void Parse_IfStatementWithNoStatements_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.RightBrace)
        });

        var condition = new Expression();
        var expected = new IfStatement(condition);
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(condition)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_IfStatementWithOneStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.None),
            new Token(TokenType.RightBrace)
        });

        var condition = _fixture.Create<Expression>();
        var bodyStatements = _fixture.CreateMany<Statement>(1).ToArray();
        var expected = new IfStatement(condition) { Statements = bodyStatements };
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(condition)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        _statementParserMock.ReturnsStatement(bodyStatements[0]);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_IfStatementWithMultipleStatements_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.RightBrace)
        });

        var condition = _fixture.Create<Expression>();
        var bodyStatements = _fixture.CreateMany<Statement>(3).ToArray();
        var expected = new IfStatement(condition) { Statements = bodyStatements };
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(condition)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        _statementParserMock.ReturnsStatementSequence(bodyStatements);

        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}