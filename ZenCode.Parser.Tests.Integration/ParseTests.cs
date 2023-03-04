using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration;

public class ParseTests
{
    private readonly IParser _sut;

    public ParseTests()
    {
        _sut = new ParserFactory(new TokenizerFactory()).Create();
    }

    [Fact]
    public void Parse_VariableDeclarationStatement_ReturnsCorrectParseTree()
    {
        // Arrange
        var code = """var x := 2""";

        var expected = new Program(new[]
        {
            new VariableDeclarationStatement(new Token(TokenType.Identifier), new ConstantExpression(new Token(TokenType.IntegerLiteral)))
        });

        // Act
        var actual = _sut.Parse(code);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}