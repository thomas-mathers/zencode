using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class AnonymousFunctionExpressionParsingTests
{
    private readonly IParser _sut;

    public AnonymousFunctionExpressionParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void ParseExpression_AnonFuncNoParamsNoStatements_ReturnsAnonFuncDeclarationExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Function), new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis), new Token(TokenType.RightArrow), new Token(TokenType.Void),
            new Token(TokenType.LeftBrace), new Token(TokenType.RightBrace)
        });

        var expected = new AnonymousFunctionDeclarationExpression(new VoidType(), new ParameterList(), new Scope());

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_AnonFuncOneParamOneStatement_ReturnsAnonFuncDeclarationExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Function), new Token(TokenType.LeftParenthesis), new Token(TokenType.Identifier),
            new Token(TokenType.Colon), new Token(TokenType.Integer), new Token(TokenType.RightParenthesis),
            new Token(TokenType.RightArrow), new Token(TokenType.Integer), new Token(TokenType.LeftBrace),
            new Token(TokenType.Return), new Token(TokenType.Identifier), new Token(TokenType.Semicolon),
            new Token(TokenType.RightBrace)
        });

        var returnType = new IntegerType();

        var parameterList = new ParameterList
        {
            Parameters = new[] { new Parameter(new Token(TokenType.Identifier), new IntegerType()) }
        };

        var scope = new Scope
        {
            Statements = new[]
            {
                new ReturnStatement
                {
                    Expression = new VariableReferenceExpression(new Token(TokenType.Identifier))
                }
            }
        };

        var expected = new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_AnonFuncMultipleParamMultipleStatement_ReturnsAnonFuncDeclarationExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Function), new Token(TokenType.LeftParenthesis), new Token(TokenType.Identifier),
            new Token(TokenType.Colon), new Token(TokenType.Boolean), new Token(TokenType.Comma),
            new Token(TokenType.Identifier), new Token(TokenType.Colon), new Token(TokenType.Integer),
            new Token(TokenType.Comma), new Token(TokenType.Identifier), new Token(TokenType.Colon),
            new Token(TokenType.Float), new Token(TokenType.Comma), new Token(TokenType.Identifier),
            new Token(TokenType.Colon), new Token(TokenType.String), new Token(TokenType.RightParenthesis),
            new Token(TokenType.RightArrow), new Token(TokenType.Float), new Token(TokenType.LeftBrace),
            new Token(TokenType.Var), new Token(TokenType.Identifier), new Token(TokenType.Assignment),
            new Token(TokenType.Identifier), new Token(TokenType.Return), new Token(TokenType.Identifier),
            new Token(TokenType.Semicolon), new Token(TokenType.RightBrace)
        });

        var returnType = new FloatType();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier), new BooleanType()),
                new Parameter(new Token(TokenType.Identifier), new IntegerType()),
                new Parameter(new Token(TokenType.Identifier), new FloatType()),
                new Parameter(new Token(TokenType.Identifier), new StringType())
            }
        };

        var scope = new Scope
        {
            Statements = new Statement[]
            {
                new VariableDeclarationStatement(new Token(TokenType.Identifier),
                    new VariableReferenceExpression(new Token(TokenType.Identifier))),
                new ReturnStatement
                {
                    Expression = new VariableReferenceExpression(new Token(TokenType.Identifier))
                }
            }
        };

        var expected = new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}