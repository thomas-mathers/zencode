using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
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
        {
            Name = new Token(TokenType.Identifier),
            ReturnType = new VoidType(),
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionWithOneParameter_ReturnFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        {
            ReturnType = new VoidType(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList
            (
                new Parameter(new Token(TokenType.Identifier), new IntegerType())
            ),
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionWithThreeParameters_ReturnFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        {
            ReturnType = new VoidType(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList
            (
                new Parameter(new Token(TokenType.Identifier), new IntegerType()),
                new Parameter(new Token(TokenType.Identifier), new IntegerType()),
                new Parameter(new Token(TokenType.Identifier), new IntegerType())
            )
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionReturningFunction_ReturnsFunctionDeclarationStatement()
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
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace),
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        {
            ReturnType = new FunctionType
            { 
                ReturnType = new VoidType(),
                ParameterTypes = new TypeList()
            },
            Name = new Token(TokenType.Identifier)
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionParameter_ReturnsFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace),
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        {
            ReturnType = new VoidType(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList
            (
                new Parameter
                (
                    new Token(TokenType.Identifier),
                    new FunctionType
                    {
                        ReturnType = new VoidType(),
                        ParameterTypes = new TypeList()
                    }
                )
            )
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionParameterReturnsFunction_ReturnsFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace),
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        {
            ReturnType = new FunctionType
            {
                ReturnType = new VoidType(),
                ParameterTypes = new TypeList()
            },
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList
            (
                new Parameter
                (
                    new Token(TokenType.Identifier),
                    new FunctionType
                    {
                        ReturnType = new VoidType(),
                        ParameterTypes = new TypeList()
                    }
                )
            )
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionWithAssignmentStatement_ReturnsFunctionDeclarationStatement()
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
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        {
            ReturnType = new VoidType(),
            Name = new Token(TokenType.Identifier),
            Body = new Scope
            (
                new AssignmentStatement 
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            )
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionWithReturnStatement_ReturnsFunctionDeclarationStatement()
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
                new Token(TokenType.Return),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            }
        );

        var expectedStatement = new FunctionDeclarationStatement
        {
            ReturnType = new VoidType(),
            Name = new Token(TokenType.Identifier),
            Body = new Scope
            (
                new ReturnStatement
                {
                    Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            )
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_MissingFunctionName_ThrowsException()
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

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected 'Identifier', got '('", exception.Message);
    }

    [Fact]
    public void Parse_MissingLeftParenthesis_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected '(', got ')'", exception.Message);
    }

    [Fact]
    public void Parse_MissingParameterName_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected 'Identifier', got ':'", exception.Message);
    }

    [Fact]
    public void Parse_MissingParameterType_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Unexpected token ')'", exception.Message);
    }

    [Fact]
    public void Parse_MissingRightParenthesis_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected 'Identifier', got '=>'", exception.Message);
    }

    [Fact]
    public void Parse_MissingReturnType_ThrowsException()
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
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Unexpected token '{'", exception.Message);
    }

    [Fact]
    public void Parse_MissingRightArrow_ThrowsException()
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
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected '=>', got 'void'", exception.Message);
    }

    [Fact]
    public void Parse_MissingLeftBrace_ThrowsException()
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
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected '{', got '}'", exception.Message);
    }

    [Fact]
    public void Parse_MissingRightBrace_ThrowsException()
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
                new Token(TokenType.LeftBrace)
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}
