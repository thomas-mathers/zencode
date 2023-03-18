using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class ReadStatementParsingTests
{
    private readonly IParser _sut;

    public ReadStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }
    
    [Fact]
    public void Parse_ReadIntoVariable_ReturnsReadStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Read),
            new Token(TokenType.Identifier)
        });

        var expectedStatement = new ReadStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_ReadIntoArrayElement_ReturnsReadStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Read),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
        });

        var expectedStatement = new ReadStatement(new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList()
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        });

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}