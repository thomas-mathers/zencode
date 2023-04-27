using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Model.Mappers;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.SemanticAnalysis.Tests.Integration.TestData;
using Program = ZenCode.Parser.Model.Grammar.Program;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Tests.Integration;

public class SemanticAnalyzerTests
{
    private readonly ISemanticAnalyzer _sut = new SemanticAnalyzerFactory().Create();

    [Fact]
    public void Analyze_NullProgram_ThrowsArgumentNullException()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _sut.Analyze(new SemanticAnalyzerContext(), null!));

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [ClassData(typeof(BinaryOperatorUnsupportedTypes))]
    public void Analyze_BinaryOperatorOperatorUnsupportedTypes_AddsBinaryOperatorUnsupportedTypesException
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
                    Operator = TokenTypeToBinaryOperatorTypeMapper.Map(op),
                    Right = new LiteralExpression(new Token(rightType))
                }
            }
        );
        
        var semanticAnalyzerContext = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(semanticAnalyzerContext, program);

        // Assert
        Assert.Contains(semanticAnalyzerContext.Errors, e => e is BinaryOperatorUnsupportedTypesException);
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
                    Operator = TokenTypeToBinaryOperatorTypeMapper.Map(op),
                    Right = new LiteralExpression(new Token(rightType))
                }
            }
        );

        // Act
        _sut.Analyze(new SemanticAnalyzerContext(), program);

        // TODO: Assert that the type of the variable is correct
    }

    [Fact]
    public void Analyze_AssignIncorrectType_AddsTypeMismatchException()
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
        
        var semanticAnalyzerContext = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(semanticAnalyzerContext, program);

        // Assert
        Assert.Contains(semanticAnalyzerContext.Errors, e => e is TypeMismatchException);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }

    [Fact]
    public void Analyze_DuplicateVariableDeclarationInDifferentScope_AddsDuplicateIdentifierException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }
    
    [Fact]
    public void Analyze_DuplicateFunctionDeclarationInDifferentScope_AddsDuplicateIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                ReturnType = new VoidType(),
            },
            new Scope
            (
                new FunctionDeclarationStatement
                {
                    Name = new Token(TokenType.Identifier, "x"),
                    ReturnType = new VoidType(),
                }
            )
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }
    
    [Fact]
    public void Analyze_DuplicateFunctionDeclarationInSameScope_AddsDuplicateIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                ReturnType = new VoidType(),
            },
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "x"),
                ReturnType = new VoidType(),
            }
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }
    
    [Fact]
    public void Analyze_DuplicateVariableDeclarationInSameScope_AddsDuplicateIdentifierException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }
    
    [Fact]
    public void Analyze_VariableHasSameNameAsParameter_AddsDuplicateIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new FunctionDeclarationStatement
            {
                Name = new Token(TokenType.Identifier, "f"),
                ReturnType = new VoidType(),
                Parameters = new ParameterList
                (
                    new Parameter
                    (
                        new Token(TokenType.Identifier, "x"),
                        new IntegerType()
                    )
                ),
                Body = new Scope
                (
                    new VariableDeclarationStatement
                    {
                        VariableName = new Token(TokenType.Identifier, "x"),
                        Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
                    }
                )
            }
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }
    
    [Fact]
    public void Analyze_UndeclaredVariableReference_AddsUndeclaredIdentifierException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is UndeclaredIdentifierException);
    }

    [Fact]
    public void Analyze_InvokingNonFunction_AddsInvokingNonFunctionTypeException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is InvokingNonFunctionTypeException);
    }

    [Fact]
    public void
        Analyze_AnonymousFunctionCallWithIncorrectNumberOfParameters_AddsIncorrectNumberOfParametersException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is IncorrectNumberOfParametersException);
    }

    [Fact]
    public void Analyze_FunctionCallWithIncorrectNumberOfParameters_AddsIncorrectNumberOfParametersException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is IncorrectNumberOfParametersException);
    }

    [Fact]
    public void Analyze_FunctionCallWithIncorrectParameterType_AddsTypeMismatchException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
    }

    [Fact]
    public void Analyze_AnonymousFunctionCallWithIncorrectParameterType_AddsTypeMismatchException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }

    [Fact]
    public void Analyze_IfStatementWithNonBooleanConditionType_AddsTypeMismatchException()
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

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }

    [Fact]
    public void Analyze_ElseIfWithNonBooleanConditionType_AddsTypeMismatchException()
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
        
        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }

    [Fact]
    public void Analyze_WhileStatementWithNonBooleanConditionType_AddsTypeMismatchException()
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
        
        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }

    [Fact]
    public void Analyze_ForStatementNonBooleanCondition_AddsTypeMismatchException()
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
                        Operator = BinaryOperatorType.Addition,
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
            }
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
            }
        );

        // Act
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }
    
    [Fact]
    public void Analyze_ForStatementReferenceIteratorInsideBody_DoesNotThrowException()
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
                Body = new Scope
                (
                    new VariableDeclarationStatement
                    {
                        VariableName = new Token(TokenType.Identifier, "y"),
                        Value = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    }
                )
            }
        );

        // Act
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }
    
    [Fact]
    public void Analyze_ReferenceForLoopIteratorInParentScope_AddsUndeclaredIdentifierException()
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                }
            },
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "y"),
                Value = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
            }
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is UndeclaredIdentifierException);
    }
    
    [Fact]
    public void Analyze_ForLoopIteratorHasSameNameAsVariableDefinedBeforeLoop_AddsDuplicateIdentifierException()
    {
        // Arrange
        var program = new Program
        (
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            },
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
            }
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }
    
    [Fact]
    public void Analyze_ForLoopIteratorHasSameNameAsVariableDefinedAfterLoop_AddsDuplicateIdentifierException()
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
            },
            new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier, "x"),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            }
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }

    [Fact]
    public void Analyze_DefineVariableWithSameNameAsForLoopIterator_AddsDuplicateIdentifierException()
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
                        Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    },
                },
                Body = new Scope
                (
                    new VariableDeclarationStatement
                    {
                        VariableName = new Token(TokenType.Identifier, "x"),
                        Value = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    }
                )
            }
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is DuplicateIdentifierException);
    }

    [Fact]
    public void Analyze_BreakStatementNotInsideLoop_AddsInvalidBreakException()
    {
        // Arrange
        var program = new Program
        (
            new BreakStatement()
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);
        
        // Assert
        Assert.Contains(context.Errors, e => e is InvalidBreakException);
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }
    
    [Fact]
    public void Analyze_ContinueStatementNotInsideLoop_AddsInvalidContinueException()
    {
        // Arrange
        var program = new Program
        (
            new ContinueStatement()
        );

        var context = new SemanticAnalyzerContext();

        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is InvalidContinueException);
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
                    Operator = BinaryOperatorType.LessThan,
                    Right = new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                },
                Iterator = new AssignmentStatement
                {
                    VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                    Value = new BinaryExpression
                    {
                        Left = new VariableReferenceExpression(new Token(TokenType.Identifier, "x")),
                        Operator = BinaryOperatorType.Addition,
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }
    
    [Fact]
    public void Analyze_ReturnStatementNotInsideFunction_AddsInvalidReturnException()
    {
        // Arrange
        var program = new Program
        (
            new ReturnStatement()
        );

        var context = new SemanticAnalyzerContext();
        
        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is InvalidReturnException);
    }
    
    [Fact]
    public void Analyze_ReturnsIncorrectType_AddsTypeMismatchException()
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

        var context = new SemanticAnalyzerContext();
        
        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
    }
    
    [Fact]
    public void Analyze_MissingReturnExpression_AddsTypeMismatchException()
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

        var context = new SemanticAnalyzerContext();
        
        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }
    
    [Fact]
    public void Analyze_AnonymousFunctionReturnsIncorrectType_AddsTypeMismatchException()
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

        var context = new SemanticAnalyzerContext();
        
        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
    }
    
    [Fact]
    public void Analyze_AnonymousFunctionMissingReturnExpression_AddsTypeMismatchException()
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

        var context = new SemanticAnalyzerContext();
        
        // Act
        _sut.Analyze(context, program);

        // Assert
        Assert.Contains(context.Errors, e => e is TypeMismatchException);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
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
        _sut.Analyze(new SemanticAnalyzerContext(), program);
    }
}
