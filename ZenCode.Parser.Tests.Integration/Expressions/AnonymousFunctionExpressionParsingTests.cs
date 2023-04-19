using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
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
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        var expected = new AnonymousFunctionDeclarationExpression
        {
            ReturnType = new VoidType()
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_AnonFuncOneParamOneStatement_ReturnsAnonFuncDeclarationExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        var returnType = new IntegerType();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier), new IntegerType())
            }
        };

        var scope = new Scope
        (
            new ReturnStatement
            {
                Expression = new VariableReferenceExpression(new Token(TokenType.Identifier))
            }
        );

        var expected = new AnonymousFunctionDeclarationExpression
        {
            ReturnType = returnType,
            Parameters = parameterList,
            Body = scope
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseExpression_AnonFuncMultipleParamMultipleStatement_ReturnsAnonFuncDeclarationExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Boolean),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Float),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.String),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Float),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

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
        (
            new VariableDeclarationStatement
            {
                Name = new Token(TokenType.Identifier),
                Value = new VariableReferenceExpression(new Token(TokenType.Identifier))
            },
            new ReturnStatement
            {
                Expression = new VariableReferenceExpression(new Token(TokenType.Identifier))
            }
        );

        var expected = new AnonymousFunctionDeclarationExpression
        {
            ReturnType = returnType,
            Parameters = parameterList,
            Body = scope
        };

        // Act
        var actual = _sut.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_MissingLeftParenthesis_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Expected '(', got 'Identifier'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingParameter_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Expected 'Identifier', got ':'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingRightParenthesis_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Expected ')', got '=>'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingRightArrow_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Expected '=>', got 'int'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingReturnType_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Unexpected token '{'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingLeftBrace_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Integer),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Expected '{', got 'return'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingRightBrace_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon)
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseExpression(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}
