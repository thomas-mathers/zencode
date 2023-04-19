using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class AssignmentStatementParsingTests
{
    private readonly IParser _sut;

    public AssignmentStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_AssignBinaryExpressionToArray_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.FloatLiteral),
                new Token(TokenType.Plus),
                new Token(TokenType.FloatLiteral)
            }
        );

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        var expression = new BinaryExpression
        {
            LeftOperand = new LiteralExpression(new Token(TokenType.FloatLiteral)),
            Operator = new Token(TokenType.Plus),
            RightOperand = new LiteralExpression(new Token(TokenType.FloatLiteral))
        };

        var expectedStatement = new AssignmentStatement
        {
            Variable = variableReferenceExpression, 
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Theory]
    [InlineData(TokenType.BooleanLiteral)]
    [InlineData(TokenType.IntegerLiteral)]
    [InlineData(TokenType.FloatLiteral)]
    [InlineData(TokenType.StringLiteral)]
    public void Parse_AssignConstantToArray_ReturnsAssignmentStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(tokenType)
            }
        );

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        var expectedStatement = new AssignmentStatement
        {
            Variable = variableReferenceExpression, 
            Value = new LiteralExpression(new Token(tokenType))
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignFunctionCallToArray_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis)
            }
        );

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new FunctionCallExpression
        {
            FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier))
        };

        var expectedStatement = new AssignmentStatement
        {
            Variable = variableReferenceExpression, 
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignParenthesisExpressionToArray_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.StringLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        var expression = new LiteralExpression(new Token(TokenType.StringLiteral));

        var expectedStatement = new AssignmentStatement
        {
            Variable = variableReferenceExpression,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignUnaryExpressionToArray_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.Minus),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        var expression = new UnaryExpression
        (
            new Token(TokenType.Minus),
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );

        var expectedStatement = new AssignmentStatement
        {
            Variable = variableReferenceExpression,
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignVariableToArray_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier)
            }
        );

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        var expression = new VariableReferenceExpression(new Token(TokenType.Identifier));

        var expectedStatement = new AssignmentStatement
        {
            Variable = variableReferenceExpression, 
            Value = expression
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignBinaryExpressionToVariable_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.FloatLiteral),
                new Token(TokenType.Plus),
                new Token(TokenType.FloatLiteral)
            }
        );

        var expectedStatement = new AssignmentStatement
        {
            Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Value = new BinaryExpression
            {
                LeftOperand = new LiteralExpression(new Token(TokenType.FloatLiteral)),
                Operator = new Token(TokenType.Plus),
                RightOperand = new LiteralExpression(new Token(TokenType.FloatLiteral))
            }
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Theory]
    [InlineData(TokenType.BooleanLiteral)]
    [InlineData(TokenType.IntegerLiteral)]
    [InlineData(TokenType.FloatLiteral)]
    [InlineData(TokenType.StringLiteral)]
    public void Parse_AssignConstantToVariable_ReturnsAssignmentStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(tokenType)
            }
        );

        var expectedStatement =
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(tokenType))
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignFunctionCallToVariable_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expectedStatement =
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier))
                }
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignParenthesisExpressionToVariable_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.StringLiteral),
                new Token(TokenType.RightParenthesis)
            }
        );

        var expectedStatement =
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.StringLiteral))
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignUnaryExpressionToVariable_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Minus),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var expectedStatement =
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new UnaryExpression
                (
                    new Token(TokenType.Minus),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                )
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignVariableToVariable_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier)
            }
        );

        var expectedStatement =
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new VariableReferenceExpression(new Token(TokenType.Identifier))
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_MissingRightHandSide_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment)
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}
