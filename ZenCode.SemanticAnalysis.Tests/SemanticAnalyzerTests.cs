using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests;

public class SemanticAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly Mock<IAssignmentStatementAnalyzer> _assignmentStatementAnalyzerMock = new();
    private readonly Mock<IBreakStatementAnalyzer> _breakStatementAnalyzerMock = new();
    private readonly Mock<IContinueStatementAnalyzer> _continueStatementAnalyzerMock = new();
    private readonly Mock<IForStatementAnalyzer> _forStatementAnalyzerMock = new();
    private readonly Mock<IFunctionDeclarationStatementAnalyzer> _functionDeclarationStatementAnalyzerMock = new();
    private readonly Mock<IIfStatementAnalyzer> _ifStatementAnalyzerMock = new();
    private readonly Mock<IPrintStatementAnalyzer> _printStatementAnalyzerMock = new();
    private readonly Mock<IReadStatementAnalyzer> _readStatementAnalyzerMock = new();
    private readonly Mock<IReturnStatementAnalyzer> _returnStatementAnalyzerMock = new();
    private readonly Mock<IVariableDeclarationStatementAnalyzer> _variableDeclarationStatementAnalyzerMock = new();
    private readonly Mock<IWhileStatementAnalyzer> _whileStatementAnalyzerMock = new();
    private readonly Mock<IScopeAnalyzer> _scopeAnalyzerMock = new();
    private readonly Mock<IAnonymousFunctionDeclarationExpressionAnalyzer>
        _anonymousFunctionDeclarationExpressionAnalyzerMock = new();
    private readonly Mock<IBinaryExpressionAnalyzer> _binaryExpressionAnalyzerMock = new();
    private readonly Mock<IFunctionCallExpressionAnalyzer> _functionCallExpressionAnalyzerMock = new();
    private readonly Mock<ILiteralExpressionAnalyzer> _literalExpressionAnalyzerMock = new();
    private readonly Mock<INewArrayExpressionAnalyzer> _newArrayExpressionAnalyzerMock = new();
    private readonly Mock<IUnaryExpressionAnalyzer> _unaryExpressionAnalyzerMock = new();
    private readonly Mock<IVariableReferenceExpressionAnalyzer> _variableReferenceExpressionAnalyzerMock = new();
    private readonly Mock<IConditionScopeAnalyzer> _conditionScopeAnalyzerMock = new();
    private readonly Mock<IParameterListAnalyzer> _parameterListAnalyzerMock = new();
    private readonly Mock<IParameterAnalyzer> _parameterAnalyzerMock = new();

    private readonly SemanticAnalyzer _sut;

    public SemanticAnalyzerTests()
    {
        _sut = new SemanticAnalyzer
        (
            _assignmentStatementAnalyzerMock.Object,
            _breakStatementAnalyzerMock.Object,
            _continueStatementAnalyzerMock.Object,
            _forStatementAnalyzerMock.Object,
            _functionDeclarationStatementAnalyzerMock.Object,
            _ifStatementAnalyzerMock.Object,
            _printStatementAnalyzerMock.Object,
            _readStatementAnalyzerMock.Object,
            _returnStatementAnalyzerMock.Object,
            _variableDeclarationStatementAnalyzerMock.Object,
            _whileStatementAnalyzerMock.Object,
            _scopeAnalyzerMock.Object,
            _anonymousFunctionDeclarationExpressionAnalyzerMock.Object,
            _binaryExpressionAnalyzerMock.Object,
            _functionCallExpressionAnalyzerMock.Object,
            _literalExpressionAnalyzerMock.Object,
            _newArrayExpressionAnalyzerMock.Object,
            _unaryExpressionAnalyzerMock.Object,
            _variableReferenceExpressionAnalyzerMock.Object,
            _conditionScopeAnalyzerMock.Object,
            _parameterListAnalyzerMock.Object,
            _parameterAnalyzerMock.Object
        );
    }

    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Analyze(null!, new Program(new Scope())));
    }
    
    [Fact]
    public void Analyze_NullAstNode_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Analyze(_semanticAnalyzerContextMock.Object, null!));
    }
    
    [Fact]
    public void Analyze_AssignmentStatement_CallsAssignmentStatementAnalyzer()
    {
        // Arrange
        var assignmentStatement = new AssignmentStatement
        {
            VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "a")), 
            Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, assignmentStatement);
        
        // Assert
        _assignmentStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, assignmentStatement),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_BreakStatement_CallsBreakStatementAnalyzer()
    {
        // Arrange
        var breakStatement = new BreakStatement();
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, breakStatement);
        
        // Assert
        _breakStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_semanticAnalyzerContextMock.Object),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_ContinueStatement_CallsContinueStatementAnalyzer()
    {
        // Arrange
        var continueStatement = new ContinueStatement();
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, continueStatement);
        
        // Assert
        _continueStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_semanticAnalyzerContextMock.Object),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_ForStatement_CallsForStatementAnalyzer()
    {
        // Arrange
        var forStatement = new ForStatement
        {
            Initializer = new VariableDeclarationStatement
            {
                VariableName = new Token(TokenType.Identifier),
                Value = new ExpressionMock()
            },
            Condition = new ExpressionMock(),
            Iterator = new AssignmentStatement
            {
                VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new ExpressionMock()
            },
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, forStatement);
        
        // Assert
        _forStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, forStatement),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_FunctionDeclarationStatement_CallsFunctionDeclarationStatementAnalyzer()
    {
        // Arrange
        var functionDeclarationStatement = new FunctionDeclarationStatement
        {
            ReturnType = new VoidType(),
            Name = new Token(TokenType.Identifier),
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, functionDeclarationStatement);
        
        // Assert
        _functionDeclarationStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, functionDeclarationStatement),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_IfStatement_CallsIfStatementAnalyzer()
    {
        // Arrange
        var ifStatement = new IfStatement
        {
            ThenScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, ifStatement);
        
        // Assert
        _ifStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, ifStatement),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_PrintStatement_CallsPrintStatementAnalyzer()
    {
        // Arrange
        var printStatement = new PrintStatement
        {
            Expression = new ExpressionMock()
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, printStatement);
        
        // Assert
        _printStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_ReadStatement_CallsReadStatementAnalyzer()
    {
        // Arrange
        var readStatement = new ReadStatement
        {
            VariableReference = new VariableReferenceExpression(new Token(TokenType.Identifier))
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, readStatement);
        
        // Assert
        _readStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_ReturnStatement_CallsReturnStatementAnalyzer()
    {
        // Arrange
        var returnStatement = new ReturnStatement();
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, returnStatement);
        
        // Assert
        _returnStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, returnStatement),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_VariableDeclarationStatement_CallsVariableDeclarationStatementAnalyzer()
    {
        // Arrange
        var variableDeclarationStatement = new VariableDeclarationStatement
        {
            VariableName = new Token(TokenType.Identifier),
            Value = new ExpressionMock()
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, variableDeclarationStatement);
        
        // Assert
        _variableDeclarationStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, variableDeclarationStatement),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_WhileStatement_CallsWhileStatementAnalyzer()
    {
        // Arrange
        var whileStatement = new WhileStatement
        {
            ConditionScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, whileStatement);
        
        // Assert
        _whileStatementAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, whileStatement),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_Scope_CallsScopeAnalyzer()
    {
        // Arrange
        var scope = new Scope();
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, scope);
        
        // Assert
        _scopeAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, scope),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_AnonymousFunctionDeclarationExpression_CallsAnonymousFunctionDeclarationExpressionAnalyzer()
    {
        // Arrange
        var anonymousFunctionDeclarationExpression = new AnonymousFunctionDeclarationExpression
        {
            ReturnType = new VoidType()
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, anonymousFunctionDeclarationExpression);
        
        // Assert
        _anonymousFunctionDeclarationExpressionAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, anonymousFunctionDeclarationExpression),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_BinaryExpression_CallsBinaryExpressionAnalyzer()
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        {
            Left = new ExpressionMock(),
            Operator = BinaryOperatorType.Addition,
            Right = new ExpressionMock()
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, binaryExpression);
        
        // Assert
        _binaryExpressionAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, binaryExpression),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_FunctionCallExpression_CallsFunctionCallExpressionAnalyzer()
    {
        // Arrange
        var functionCallExpression = new FunctionCallExpression
        {
            FunctionReference = new VariableReferenceExpression(new Token(TokenType.Identifier))
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, functionCallExpression);
        
        // Assert
        _functionCallExpressionAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, functionCallExpression),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_LiteralExpression_CallsLiteralExpressionAnalyzer()
    {
        // Arrange
        var literalExpression = new LiteralExpression(new Token(TokenType.IntegerLiteral));
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, literalExpression);
        
        // Assert
        _literalExpressionAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_semanticAnalyzerContextMock.Object, literalExpression),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_NewArrayExpression_CallsNewArrayExpressionAnalyzer()
    {
        // Arrange
        var newArrayExpression = new NewArrayExpression
        {
            Type = new BooleanType(),
            Size = new ExpressionMock()
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, newArrayExpression);
        
        // Assert
        _newArrayExpressionAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_semanticAnalyzerContextMock.Object, newArrayExpression),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_UnaryExpression_CallsUnaryExpressionAnalyzer()
    {
        // Arrange
        var unaryExpression = new UnaryExpression
        {
            Operator = UnaryOperatorType.Negate,
            Expression = new ExpressionMock()
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, unaryExpression);
        
        // Assert
        _unaryExpressionAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, unaryExpression),
            Times.Once
        );
    }

    [Fact]
    public void Analyze_VariableReferenceExpression_CallsVariableReferenceExpressionAnalyzer()
    {
        // Arrange
        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier));

        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, variableReferenceExpression);

        // Assert
        _variableReferenceExpressionAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_semanticAnalyzerContextMock.Object, variableReferenceExpression),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_ConditionScope_CallsConditionScopeAnalyzer()
    {
        // Arrange
        var conditionScope = new ConditionScope
        {
            Condition = new ExpressionMock()
        };
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, conditionScope);
        
        // Assert
        _conditionScopeAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, conditionScope),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_ParameterList_CallsParameterListAnalyzer()
    {
        // Arrange
        var parameterList = new ParameterList();
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, parameterList);
        
        // Assert
        _parameterListAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_sut, _semanticAnalyzerContextMock.Object, parameterList),
            Times.Once
        );
    }
    
    [Fact]
    public void Analyze_Parameter_CallsParameterAnalyzer()
    {
        // Arrange
        var parameter = new Parameter(new Token(TokenType.Identifier, "x"), new VoidType());
        
        // Act
        _sut.Analyze(_semanticAnalyzerContextMock.Object, parameter);
        
        // Assert
        _parameterAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_semanticAnalyzerContextMock.Object, parameter),
            Times.Once
        );
    }
}
