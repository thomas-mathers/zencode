using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class StatementParserTests
{
    private readonly IParser _sut;

    public StatementParserTests()
    {
        _sut = new ParserFactory().Create();
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
            new Token(TokenType.Plus),
            new Token(TokenType.FloatLiteral)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new BinaryExpression(
            new LiteralExpression(new Token(TokenType.FloatLiteral)),
            new Token(TokenType.Plus),
            new LiteralExpression(new Token(TokenType.FloatLiteral)));

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
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new LiteralExpression(new Token(tokenType));

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
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
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
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new LiteralExpression(new Token(TokenType.StringLiteral));

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
            new Token(TokenType.Minus),
            new Token(TokenType.IntegerLiteral)
        });

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            }
        };

        var expression = new UnaryExpression(new Token(TokenType.Minus),
            new LiteralExpression(new Token(TokenType.IntegerLiteral)));

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
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
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
            new Token(TokenType.Plus),
            new Token(TokenType.FloatLiteral)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new BinaryExpression(new LiteralExpression(new Token(TokenType.FloatLiteral)),
                    new Token(TokenType.Plus), new LiteralExpression(new Token(TokenType.FloatLiteral))));

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
                new LiteralExpression(new Token(tokenType)));

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
                new LiteralExpression(new Token(TokenType.StringLiteral)));

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
            new Token(TokenType.Minus),
            new Token(TokenType.IntegerLiteral)
        });

        var expectedStatement =
            new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new UnaryExpression(new Token(TokenType.Minus),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))));

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
    public void Parse_Break_ReturnsBreakStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Break),
        });

        var expectedStatement = new BreakStatement();

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_Continue_ReturnsBreakStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Continue),
        });

        var expectedStatement = new ContinueStatement();

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_ForStatement_ReturnsForStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.For),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.Semicolon),
            new Token(TokenType.Identifier),
            new Token(TokenType.LessThan),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.Semicolon),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier),
            new Token(TokenType.Plus),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightParenthesis),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBracket),
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.RightBrace)
        });

        var initialization = new VariableDeclarationStatement(
            new Token(TokenType.Identifier),
            new LiteralExpression(new Token(TokenType.IntegerLiteral)));
        
        var condition = new BinaryExpression(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.LessThan),
            new LiteralExpression(new Token(TokenType.IntegerLiteral)));
        
        var iterator = new AssignmentStatement(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new BinaryExpression(
                new VariableReferenceExpression(new Token(TokenType.Identifier)),
                new Token(TokenType.Plus),
                new LiteralExpression(new Token(TokenType.IntegerLiteral))));
        
        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))   
                }
            }
        };
        
        var expression = new LiteralExpression(new Token(TokenType.IntegerLiteral));
        
        var scopeStatement = new AssignmentStatement(variableReferenceExpression, expression);
        
        var scope = new Scope
        {
            Statements = new[]
            {
                scopeStatement
            }
        };
        
        var expectedStatement = new ForStatement(initialization, condition, iterator, scope);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_FunctionWithNoParameters_ReturnFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Function),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis),
            new Token(TokenType.RightArrow),
            new Token(TokenType.Void),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.RightBrace),
        });

        var expectedStatement = new FunctionDeclarationStatement(new VoidType(), new ParameterList(), new Scope());

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
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
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
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
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
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseIfCondition = new BinaryExpression(
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals),
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );
        var elseIfScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var elseIfConditionScope = new ConditionScope(elseIfCondition, elseIfScope);

        var elseScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
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
            new Token(TokenType.Plus),
            new Token(TokenType.IntegerLiteral)
        });

        var expectedStatement = new PrintStatement(
            new BinaryExpression(
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new Token(TokenType.Plus),
                new LiteralExpression(new Token(TokenType.IntegerLiteral))));

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

        var expectedStatement = new PrintStatement(new LiteralExpression(new Token(tokenType)));

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
            new PrintStatement(new LiteralExpression(new Token(TokenType.StringLiteral)));

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
            new Token(TokenType.Minus),
            new Token(TokenType.FloatLiteral)
        });

        var expectedStatement =
            new PrintStatement(new UnaryExpression(new Token(TokenType.Minus),
                new LiteralExpression(new Token(TokenType.FloatLiteral))));

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
    
    [Fact]
    public void Parse_ReturnNothing_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(TokenType.Semicolon)
        });

        var expectedStatement = new ReturnStatement();

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
            new Token(TokenType.Plus),
            new Token(TokenType.IntegerLiteral),
            new Token(TokenType.Semicolon)
        });

        var expectedStatement = new ReturnStatement
        {
            Expression = new BinaryExpression(
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new Token(TokenType.Plus),
                new LiteralExpression(new Token(TokenType.IntegerLiteral)))
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
            new Token(tokenType),
            new Token(TokenType.Semicolon)
        });

        var expectedStatement = new ReturnStatement
        {
            Expression = new LiteralExpression(new Token(tokenType))
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
            new Token(TokenType.RightParenthesis),
            new Token(TokenType.Semicolon)
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
            new Token(TokenType.RightParenthesis),
            new Token(TokenType.Semicolon)
        });

        var expectedStatement =
            new ReturnStatement
            {
                Expression = new LiteralExpression(new Token(TokenType.StringLiteral))
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
            new Token(TokenType.Minus),
            new Token(TokenType.FloatLiteral),
            new Token(TokenType.Semicolon)
        });

        var expectedStatement =
            new ReturnStatement
            {
                Expression = new UnaryExpression(new Token(TokenType.Minus),
                    new LiteralExpression(new Token(TokenType.FloatLiteral)))
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
            new Token(TokenType.Identifier),
            new Token(TokenType.Semicolon)
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
            new Token(TokenType.Plus),
            new Token(TokenType.IntegerLiteral)
        });
        
        var identifier = new Token(TokenType.Identifier);

        var expression = new BinaryExpression(
            new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            new Token(TokenType.Plus),
            new LiteralExpression(new Token(TokenType.IntegerLiteral)));

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

        var expression = new LiteralExpression(new Token(tokenType));

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

        var expression = new LiteralExpression(new Token(TokenType.StringLiteral));

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
            new Token(TokenType.Minus),
            new Token(TokenType.FloatLiteral)
        });

        var identifier = new Token(TokenType.Identifier);
        
        var expression = new UnaryExpression(
            new Token(TokenType.Minus),
            new LiteralExpression(new Token(TokenType.FloatLiteral)));

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
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );
        var scope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
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