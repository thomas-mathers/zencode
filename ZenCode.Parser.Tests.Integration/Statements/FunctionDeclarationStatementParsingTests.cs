using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class FunctionDeclarationStatementParsingTests
{
    private readonly IParser _sut;

    public FunctionDeclarationStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_FunctionWithNoParameters_ReturnFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        (
            new VoidType(),
            new Token(TokenType.Identifier),
            new ParameterList(),
            new Scope()
        );

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}
