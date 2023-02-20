using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Common.Testing.Extensions;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Statements;

namespace ZenCode.Parser.Tests.Statements;

public class WhileStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly WhileStatementParsingStrategy _sut;

    public WhileStatementParsingStrategyTests()
    {
        _sut = new WhileStatementParsingStrategy(_statementParserMock.Object, _expressionParserMock.Object);
    }

    [Fact]
    public void Parse_WhileStatementWithNoStatements_ReturnsWhileExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.While),
            new Token(TokenType.None),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.RightBrace)
        });

        var condition = _fixture.Create<Expression>();
        var expected = new WhileStatement(condition);

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
    public void Parse_WhileStatementWithOneStatement_ReturnsWhileExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.While),
            new Token(TokenType.None),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.None),
            new Token(TokenType.RightBrace)
        });

        var condition = _fixture.Create<Expression>();
        var bodyStatements = _fixture.CreateMany<Statement>(1).ToArray();
        var expected = new WhileStatement(condition) { Statements = bodyStatements };
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(condition)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });
        
        _statementParserMock.Setup(x => x.Parse(tokenStream))
            .Returns(bodyStatements[0])
            .Callback<ITokenStream>(_ =>
            {
                tokenStream.Consume();
            });

        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_WhileStatementWithMultipleStatements_ReturnsWhileExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.While),
            new Token(TokenType.None),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.RightBrace)
        });

        var condition = _fixture.Create<Expression>();
        var bodyStatements = _fixture.CreateMany<Statement>(3).ToArray();
        var expected = new WhileStatement(condition) { Statements = bodyStatements };
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(condition)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });
        
        _statementParserMock.Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(bodyStatements)
            .Callback<ITokenStream>(_ =>
            {
                tokenStream.Consume();
            });
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}