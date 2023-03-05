using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Types;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class StatementParserTests
{
    private readonly IStatementParser _sut;

    public StatementParserTests()
    {
        _sut = new StatementParserFactory(new ExpressionParserFactory(), new TypeParserFactory()).Create();
    }

    [Fact]
    public void Parse_AssignBinaryExpressionToArray_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
            new Token(TokenType.Assignment),
            new Token(TokenType.FloatLiteral),
            new Token(TokenType.Addition),
            new Token(TokenType.FloatLiteral)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new ConstantExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new BinaryExpression(
            new ConstantExpression(new Token(TokenType.FloatLiteral)),
            new Token(TokenType.Addition),
            new ConstantExpression(new Token(TokenType.FloatLiteral)));

        var expectedStatement = new AssignmentStatement(variableReferenceExpression, expression);

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
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
            new Token(TokenType.Assignment),
            new Token(tokenType)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new ConstantExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new ConstantExpression(new Token(tokenType));

        var expectedStatement = new AssignmentStatement(variableReferenceExpression, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignFunctionCallToArray_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new ConstantExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        var expectedStatement = new AssignmentStatement(variableReferenceExpression, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignParenthesisExpressionToArray_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
            new Token(TokenType.Assignment),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.StringLiteral),
            new Token(TokenType.RightParenthesis)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new ConstantExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new ConstantExpression(new Token(TokenType.StringLiteral));

        var expectedStatement = new AssignmentStatement(variableReferenceExpression, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignUnaryExpressionToArray_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
            new Token(TokenType.Assignment),
            new Token(TokenType.Subtraction),
            new Token(TokenType.IntegerLiteral)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new ConstantExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new UnaryExpression(new Token(TokenType.Subtraction),
            new ConstantExpression(new Token(TokenType.IntegerLiteral)));

        var expectedStatement = new AssignmentStatement(variableReferenceExpression, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignVariableToArray_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new ConstantExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new VariableReferenceExpression(new Token(TokenType.Identifier));

        var expectedStatement = new AssignmentStatement(variableReferenceExpression, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignBinaryExpressionToVariable_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.FloatLiteral),
            new Token(TokenType.Addition),
            new Token(TokenType.FloatLiteral)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new BinaryExpression(new ConstantExpression(new Token(TokenType.FloatLiteral)),
                    new Token(TokenType.Addition), new ConstantExpression(new Token(TokenType.FloatLiteral))));

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
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(tokenType)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new ConstantExpression(new Token(tokenType)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignFunctionCallToVariable_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier))));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignParenthesisExpressionToVariable_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.StringLiteral),
            new Token(TokenType.RightParenthesis)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new ConstantExpression(new Token(TokenType.StringLiteral)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignUnaryExpressionToVariable_ReturnsAssignmentStatement()
    {
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Subtraction),
            new Token(TokenType.IntegerLiteral)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new UnaryExpression(new Token(TokenType.Subtraction),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral))));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_AssignVariableToVariable_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new VariableReferenceExpression(new Token(TokenType.Identifier)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_IfBinaryExpressionThenAssignmentStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.Identifier),
            new Token(TokenType.Equals),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace)
        });

        var thenCondition = new BinaryExpression(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals),
            new ConstantExpression(new Token(TokenType.IntegerLiteral))
        );
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var expectedStatement = new IfStatement(thenConditionScope);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_IfBinaryExpressionThenAssignmentStatementElseAssignmentStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.Identifier),
            new Token(TokenType.Equals),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace),
            new Token(TokenType.Else),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace)
        });

        var thenCondition = new BinaryExpression(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals),
            new ConstantExpression(new Token(TokenType.IntegerLiteral))
        );
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral)))
            }
        };

        var expectedStatement = new IfStatement(thenConditionScope)
        {
            ElseScope = elseScope
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void
        Parse_IfBinaryExpressionThenAssignmentStatementElseIfBinaryExpressionThenAssignmentStatementElseAssignmentStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.Identifier),
            new Token(TokenType.Equals),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace),
            new Token(TokenType.ElseIf),
            new Token(TokenType.Identifier),
            new Token(TokenType.Equals),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace),
            new Token(TokenType.Else),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace)
        });

        var thenCondition = new BinaryExpression(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals),
            new ConstantExpression(new Token(TokenType.IntegerLiteral))
        );
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseIfCondition = new BinaryExpression(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals),
            new ConstantExpression(new Token(TokenType.IntegerLiteral))
        );
        var elseIfScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var elseIfConditionScope = new ConditionScope(elseIfCondition, elseIfScope);

        var elseScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral)))
            }
        };

        var expectedStatement = new IfStatement(thenConditionScope)
        {
            ElseIfScopes = new[]
            {
                elseIfConditionScope
            },
            ElseScope = elseScope
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintBinaryExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.Addition),
            new Token(TokenType.IntegerLiteral)
        });

        var expectedStatement = new PrintStatement(
            new BinaryExpression(
                new ConstantExpression(new Token(TokenType.IntegerLiteral)),
                new Token(TokenType.Addition),
                new ConstantExpression(new Token(TokenType.IntegerLiteral))));

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
    public void Parse_PrintLiteral_ReturnsPrintStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print),
            new Token(tokenType)
        });

        var expectedStatement = new PrintStatement(new ConstantExpression(new Token(tokenType)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintFunctionCallExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var expectedStatement =
            new PrintStatement(
                new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier))));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintParenthesisExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.StringLiteral),
            new Token(TokenType.RightParenthesis)
        });

        var expectedStatement =
            new PrintStatement(new ConstantExpression(new Token(TokenType.StringLiteral)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintUnaryExpression_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print),
            new Token(TokenType.Subtraction),
            new Token(TokenType.FloatLiteral)
        });

        var expectedStatement =
            new PrintStatement(new UnaryExpression(new Token(TokenType.Subtraction),
                new ConstantExpression(new Token(TokenType.FloatLiteral))));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_PrintVariableReference_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print),
            new Token(TokenType.Identifier)
        });

        var expectedStatement = new PrintStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnBinaryExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.Addition),
            new Token(TokenType.IntegerLiteral)
        });

        var expectedStatement = new ReturnStatement
        {
            Expression = new BinaryExpression(
                new ConstantExpression(new Token(TokenType.IntegerLiteral)),
                new Token(TokenType.Addition),
                new ConstantExpression(new Token(TokenType.IntegerLiteral)))
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
    public void Parse_ReturnLiteral_ReturnsReturnStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(tokenType)
        });

        var expectedStatement = new ReturnStatement
        {
            Expression = new ConstantExpression(new Token(tokenType))
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnFunctionCallExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var expectedStatement =
            new ReturnStatement
            {
                Expression =
                    new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)))
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnParenthesisExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.StringLiteral),
            new Token(TokenType.RightParenthesis)
        });

        var expectedStatement =
            new ReturnStatement
            {
                Expression = new ConstantExpression(new Token(TokenType.StringLiteral))
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnUnaryExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(TokenType.Subtraction),
            new Token(TokenType.FloatLiteral)
        });

        var expectedStatement =
            new ReturnStatement
            {
                Expression = new UnaryExpression(new Token(TokenType.Subtraction),
                    new ConstantExpression(new Token(TokenType.FloatLiteral)))
            };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_ReturnVariableReference_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(TokenType.Identifier)
        });

        var expectedStatement = new ReturnStatement
        {
            Expression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_DeclareVariableAndAssignBinaryExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.Addition),
            new Token(TokenType.IntegerLiteral)
        });
        
        var identifier = new Token(TokenType.Identifier);

        var expression = new BinaryExpression(
            new ConstantExpression(new Token(TokenType.IntegerLiteral)),
            new Token(TokenType.Addition),
            new ConstantExpression(new Token(TokenType.IntegerLiteral)));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

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
    public void Parse_DeclareVariableAndAssignLiteral_ReturnsVariableDeclarationStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(tokenType)
        });
        
        var identifier = new Token(TokenType.Identifier);

        var expression = new ConstantExpression(new Token(tokenType));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignFunctionCallExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var identifier = new Token(TokenType.Identifier);

        var expression = new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignParenthesisExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.StringLiteral),
            new Token(TokenType.RightParenthesis)
        });
        
        var identifier = new Token(TokenType.Identifier);

        var expression = new ConstantExpression(new Token(TokenType.StringLiteral));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignUnaryExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Subtraction),
            new Token(TokenType.FloatLiteral)
        });

        var identifier = new Token(TokenType.Identifier);
        
        var expression = new UnaryExpression(
            new Token(TokenType.Subtraction),
            new ConstantExpression(new Token(TokenType.FloatLiteral)));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignVariableReferenceExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier)
        });
        
        var identifier = new Token(TokenType.Identifier);

        var expression = new VariableReferenceExpression(new Token(TokenType.Identifier));
        
        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_WhileBinaryExpressionAssignmentStatement_ReturnsWhileStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.While),
            new Token(TokenType.Identifier),
            new Token(TokenType.GreaterThan),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace)
        });

        var condition = new BinaryExpression(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.GreaterThan),
            new ConstantExpression(new Token(TokenType.IntegerLiteral))
        );
        var scope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new ConstantExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var conditionScope = new ConditionScope(condition, scope);

        var expectedStatement = new WhileStatement(conditionScope);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}