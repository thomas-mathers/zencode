using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.SemanticAnalysis.Tests.TestData;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Tests;

public class SemanticAnalyzerTests
{
    private readonly SemanticAnalyzer _sut = new();

    [Fact]
    public void Analyze_NullProgram_ThrowsArgumentNullException()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _sut.Analyze(null!));

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [ClassData(typeof(BinaryOperatorUnsupportedTypes))]
    public void Analyze_BinaryOperatorOperatorUnsupportedTypes_ThrowsBinaryOperatorUnsupportedTypesException
        (TokenType op, TokenType leftType, TokenType rightType)
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new BinaryExpression
                {
                    Left = new LiteralExpression(new Token(leftType)),
                    Operator = new Token(op),
                    Right = new LiteralExpression(new Token(rightType))
                }
            }
        );

        // Act
        var exception = Assert.Throws<BinaryOperatorUnsupportedTypesException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [ClassData(typeof(BinaryOperatorSupportedTypes))]
    public void Analyze_BinaryOperatorSupportedTypes_ReturnsCorrectType
        (TokenType op, TokenType leftType, TokenType rightType, Type expectedType)
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new BinaryExpression
                {
                    Left = new LiteralExpression(new Token(leftType)),
                    Operator = new Token(op),
                    Right = new LiteralExpression(new Token(rightType))
                }
            }
        );

        // Act
        _sut.Analyze(program);

        // TODO: Assert that the type of the variable is correct
    }

    [Fact]
    public void Analyze_AssignIncorrectType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            new AssignmentStatement
            {
                VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                Value = new LiteralExpression(new Token(TokenType.StringLiteral))
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_AssignCorrectType_DoesNotThrow()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            new AssignmentStatement
            {
                VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_DuplicateVariableDeclarationInDifferentScope_DoesNotThrow()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            new Scope
            (
                new VariableDeclarationStatement
                {
                    VariableName = new Token(TokenType.Identifier, "x"),
                    Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            )
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_DuplicateVariableDeclaration_ThrowsDuplicateIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        // Act
        var exception = Assert.Throws<DuplicateIdentifierException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_UndeclaredVariableReference_ThrowsUndeclaredIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new VariableReferenceExpression(new Token(TokenType.Identifier, "y"))
            }
        );

        // Act
        var exception = Assert.Throws<UndeclaredIdentifierException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_InvokingNonFunction_ThrowsInvokingNonFunctionTypeException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "y"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            },
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "y"))
                }
            }
        );

        // Act
        var exception = Assert.Throws<InvokingNonFunctionTypeException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void
        Analyze_AnonymousFunctionCallWithIncorrectNumberOfParameters_ThrowsIncorrectNumberOfParametersException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new AnonymousFunctionDeclarationExpression
                    {
                        ReturnType = new VoidType(),
                        Parameters = new ParameterList
                        (
                            new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                        )
                    },
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                        new LiteralExpression(new Token(TokenType.IntegerLiteral))
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<IncorrectNumberOfParametersException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_FunctionCallWithIncorrectNumberOfParameters_ThrowsIncorrectNumberOfParametersException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                ReturnType = new VoidType(),
                Name = new Token(TokenType.Identifier, "f"),
                Parameters = new ParameterList
                (
                    new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                )
            },
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "f")),
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                        new LiteralExpression(new Token(TokenType.IntegerLiteral))
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<IncorrectNumberOfParametersException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_FunctionCallWithIncorrectParameterType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                ReturnType = new VoidType(),
                Name = new Token(TokenType.Identifier, "f"),
                Parameters = new ParameterList
                (
                    new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                )
            },
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "f")),
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.StringLiteral))
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_AnonymousFunctionCallWithIncorrectParameterType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new AnonymousFunctionDeclarationExpression
                    {
                        ReturnType = new VoidType(),
                        Parameters = new ParameterList
                        (
                            new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                        )
                    },
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.StringLiteral))
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_FunctionCallWithCorrectParameterType_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                ReturnType = new VoidType(),
                Name = new Token(TokenType.Identifier, "f"),
                Parameters = new ParameterList
                (
                    new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                )
            },
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "f")),
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.IntegerLiteral))
                    )
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_AnonymousFunctionCallWithCorrectParameterType_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new FunctionCallExpression
                {
                    FunctionReference = new AnonymousFunctionDeclarationExpression
                    {
                        ReturnType = new VoidType(),
                        Parameters = new ParameterList
                        (
                            new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
                        )
                    },
                    Arguments = new ExpressionList
                    (
                        new LiteralExpression(new Token(TokenType.IntegerLiteral))
                    )
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_IfStatementWithNonBooleanConditionType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new IfStatement
            {
                ThenScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.StringLiteral)),
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_IfStatementWithBooleanConditionType_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new IfStatement
            {
                ThenScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.BooleanLiteral)),
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_ElseIfWithNonBooleanConditionType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new IfStatement
            {
                ThenScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.BooleanLiteral)),
                },
                ElseIfScopes = new List<ConditionScope>
                {
                    new()
                    {
                        Condition = new LiteralExpression(new Token(TokenType.StringLiteral)),
                    }
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_ElseIfWithBooleanConditionType_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new IfStatement
            {
                ThenScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.BooleanLiteral)),
                },
                ElseIfScopes = new List<ConditionScope>
                {
                    new()
                    {
                        Condition = new LiteralExpression(new Token(TokenType.BooleanLiteral)),
                    }
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_WhileStatementWithNonBooleanConditionType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new WhileStatement
            {
                ConditionScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.StringLiteral)),
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_WhileStatementWithBooleanCondition_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new WhileStatement
            {
                ConditionScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.BooleanLiteral)),
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_ForStatementNonBooleanCondition_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new ForStatement
            {
                Initializer = new VariableDeclarationStatement
                {
                    VariableName = new Token(TokenType.Identifier, "x"),
                    Value = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Condition = new LiteralExpression(new Token(TokenType.StringLiteral)),
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = new Token(TokenType.Plus),
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_ForStatementBooleanCondition_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new ForStatement
            {
                Initializer = new VariableDeclarationStatement
                {
                    VariableName = new Token(TokenType.Identifier, "x"),
                    Value = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Condition = new BinaryExpression
                {
                    Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Operator = new Token(TokenType.LessThan),
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = new Token(TokenType.Plus),
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_BreakStatementNotInsideLoop_ThrowsInvalidBreakException()
    {
        // Arrange
        var program = new Program
        (
            new BreakStatement()
        );

        // Act
        var exception = Assert.Throws<InvalidBreakException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Analyze_BreakStatementInsideForLoop_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new ForStatement
            {
                Initializer = new VariableDeclarationStatement
                {
                    VariableName = new Token(TokenType.Identifier, "x"),
                    Value = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Condition = new BinaryExpression
                {
                    Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Operator = new Token(TokenType.LessThan),
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = new Token(TokenType.Plus),
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
                Body = new Scope
                (
                    new BreakStatement()
                )
            }
        );

        // Act
        _sut.Analyze(program);
    }

    [Fact]
    public void Analyze_BreakStatementInsideWhileLoop_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new WhileStatement
            {
                ConditionScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.BooleanLiteral)),
                    Scope = new Scope
                    (
                        new BreakStatement()
                    )
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }
    
    [Fact]
    public void Analyze_ContinueStatementNotInsideLoop_ThrowsInvalidContinueException()
    {
        // Arrange
        var program = new Program
        (
            new ContinueStatement()
        );

        // Act
        var exception = Assert.Throws<InvalidContinueException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Analyze_ContinueStatementInsideForLoop_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new ForStatement
            {
                Initializer = new VariableDeclarationStatement
                {
                    VariableName = new Token(TokenType.Identifier, "x"),
                    Value = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Condition = new BinaryExpression
                {
                    Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Operator = new Token(TokenType.LessThan),
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = new Token(TokenType.Plus),
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
                Body = new Scope
                (
                    new ContinueStatement()
                )
            }
        );

        // Act
        _sut.Analyze(program);
    }
    
    [Fact]
    public void Analyze_ContinueStatementInsideWhileLoop_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new WhileStatement
            {
                ConditionScope = new ConditionScope
                {
                    Condition = new LiteralExpression(new Token(TokenType.BooleanLiteral)),
                    Scope = new Scope
                    (
                        new ContinueStatement()
                    )
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }
    
    [Fact]
    public void Analyze_ReturnStatementNotInsideFunction_ThrowsInvalidReturnException()
    {
        // Arrange
        var program = new Program
        (
            new ReturnStatement()
        );

        // Act
        var exception = Assert.Throws<InvalidReturnException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Analyze_ReturnsIncorrectType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "foo"),
                ReturnType = new IntegerType(),
                Body = new Scope
                (
                    new ReturnStatement
                    {
                        Value = new LiteralExpression(new Token(TokenType.StringLiteral))
                    }
                )
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Analyze_MissingReturnExpression_ThrowsMissingReturnExpressionException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "foo"),
                ReturnType = new IntegerType(),
                Body = new Scope
                (
                    new ReturnStatement()
                )
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Analyze_ReturnStatementInsideFunction_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "foo"),
                ReturnType = new IntegerType(),
                Body = new Scope
                (
                    new ReturnStatement
                    {
                        Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
                    }
                )
            }
        );

        // Act
        _sut.Analyze(program);
    }
    
    [Fact]
    public void Analyze_ReturnStatementInsideFunctionWithVoidReturnType_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "foo"),
                ReturnType = new VoidType(),
                Body = new Scope
                (
                    new ReturnStatement()
                )
            }
        );

        // Act
        _sut.Analyze(program);
    }
    
    [Fact]
    public void Analyze_AnonymousFunctionReturnsIncorrectType_ThrowsTypeMismatchException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "foo"),
                Value = new AnonymousFunctionDeclarationExpression
                {
                    ReturnType = new IntegerType(),
                    Body = new Scope
                    (
                        new ReturnStatement
                        {
                            Value = new LiteralExpression(new Token(TokenType.StringLiteral))
                        }
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Analyze_AnonymousFunctionMissingReturnExpression_ThrowsMissingReturnExpressionException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "foo"),
                Value = new AnonymousFunctionDeclarationExpression
                {
                    ReturnType = new IntegerType(),
                    Body = new Scope
                    (
                        new ReturnStatement()
                    )
                }
            }
        );

        // Act
        var exception = Assert.Throws<TypeMismatchException>(() => _sut.Analyze(program));

        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Analyze_AnonymousFunctionReturnsCorrectType_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "foo"),
                Value = new AnonymousFunctionDeclarationExpression
                {
                    ReturnType = new IntegerType(),
                    Body = new Scope
                    (
                        new ReturnStatement
                        {
                            Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
                        }
                    )
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }
    
    [Fact]
    public void Analyze_AnonymousFunctionWithVoidReturnType_DoesNotThrowException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "foo"),
                Value = new AnonymousFunctionDeclarationExpression
                {
                    ReturnType = new VoidType(),
                    Body = new Scope
                    (
                        new ReturnStatement()
                    )
                }
            }
        );

        // Act
        _sut.Analyze(program);
    }
}
