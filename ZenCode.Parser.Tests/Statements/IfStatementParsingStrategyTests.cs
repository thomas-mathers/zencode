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

        var conditionalExpression = new Expression();
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(conditionalExpression)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new IfStatement(conditionalExpression);
        
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

        var conditionalExpression = _fixture.Create<Expression>();
        var statement = _fixture.Create<Statement>();
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(conditionalExpression)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });
        
        _statementParserMock.Setup(x => x.Parse(tokenStream))
            .Returns(statement)
            .Callback<ITokenStream>(_ =>
            {
                tokenStream.Consume();
            });        

        var expected = new IfStatement(conditionalExpression) { Statements = new[] { statement }};
        
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

        var conditionalExpression = _fixture.Create<Expression>();
        var statements = _fixture.CreateMany<Statement>(3).ToArray();
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(conditionalExpression)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });
        
        _statementParserMock.Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(statements)
            .Callback<ITokenStream>(_ =>
            {
                tokenStream.Consume();
            });        

        var expected = new IfStatement(conditionalExpression) { Statements = statements };
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}