using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis;

public class SemanticAnalyzer : ISemanticAnalyzer
{
    private readonly IAssignmentStatementAnalyzer _assignmentStatementAnalyzer;
    private readonly IBreakStatementAnalyzer _breakStatementAnalyzer;
    private readonly IContinueStatementAnalyzer _continueStatementAnalyzer;
    private readonly IForStatementAnalyzer _forStatementAnalyzer;
    private readonly IFunctionDeclarationStatementAnalyzer _functionDeclarationStatementAnalyzer;
    private readonly IIfStatementAnalyzer _ifStatementAnalyzer;
    private readonly IPrintStatementAnalyzer _printStatementAnalyzer;
    private readonly IReadStatementAnalyzer _readStatementAnalyzer;
    private readonly IReturnStatementAnalyzer _returnStatementAnalyzer;
    private readonly IVariableDeclarationStatementAnalyzer _variableDeclarationStatementAnalyzer;
    private readonly IWhileStatementAnalyzer _whileStatementAnalyzer;
    private readonly IScopeAnalyzer _scopeAnalyzer;
    private readonly IAnonymousFunctionDeclarationExpressionAnalyzer _anonymousFunctionDeclarationExpressionAnalyzer;
    private readonly IBinaryExpressionAnalyzer _binaryExpressionAnalyzer;
    private readonly IFunctionCallExpressionAnalyzer _functionCallExpressionAnalyzer;
    private readonly ILiteralExpressionAnalyzer _literalExpressionAnalyzer;
    private readonly INewArrayExpressionAnalyzer _newArrayExpressionAnalyzer;
    private readonly IUnaryExpressionAnalyzer _unaryExpressionAnalyzer;
    private readonly IVariableReferenceExpressionAnalyzer _variableReferenceExpressionAnalyzer;
    private readonly IConditionScopeAnalyzer _conditionScopeAnalyzer;
    private readonly IParameterListAnalyzer _parameterListAnalyzer;
    private readonly IParameterAnalyzer _parameterAnalyzer;

    public SemanticAnalyzer
    (
        IAssignmentStatementAnalyzer assignmentStatementAnalyzer,
        IBreakStatementAnalyzer breakStatementAnalyzer,
        IContinueStatementAnalyzer continueStatementAnalyzer,
        IForStatementAnalyzer forStatementAnalyzer,
        IFunctionDeclarationStatementAnalyzer functionDeclarationStatementAnalyzer,
        IIfStatementAnalyzer ifStatementAnalyzer,
        IPrintStatementAnalyzer printStatementAnalyzer,
        IReadStatementAnalyzer readStatementAnalyzer,
        IReturnStatementAnalyzer returnStatementAnalyzer,
        IVariableDeclarationStatementAnalyzer variableDeclarationStatementAnalyzer,
        IWhileStatementAnalyzer whileStatementAnalyzer,
        IScopeAnalyzer scopeAnalyzer,
        IAnonymousFunctionDeclarationExpressionAnalyzer anonymousFunctionDeclarationExpressionAnalyzer,
        IBinaryExpressionAnalyzer binaryExpressionAnalyzer,
        IFunctionCallExpressionAnalyzer functionCallExpressionAnalyzer,
        ILiteralExpressionAnalyzer literalExpressionAnalyzer,
        INewArrayExpressionAnalyzer newArrayExpressionAnalyzer,
        IUnaryExpressionAnalyzer unaryExpressionAnalyzer,
        IVariableReferenceExpressionAnalyzer variableReferenceExpressionAnalyzer,
        IConditionScopeAnalyzer conditionScopeAnalyzer,
        IParameterListAnalyzer parameterListAnalyzer,
        IParameterAnalyzer parameterAnalyzer
    )
    {
        _assignmentStatementAnalyzer = assignmentStatementAnalyzer;
        _breakStatementAnalyzer = breakStatementAnalyzer;
        _continueStatementAnalyzer = continueStatementAnalyzer;
        _forStatementAnalyzer = forStatementAnalyzer;
        _functionDeclarationStatementAnalyzer = functionDeclarationStatementAnalyzer;
        _ifStatementAnalyzer = ifStatementAnalyzer;
        _printStatementAnalyzer = printStatementAnalyzer;
        _readStatementAnalyzer = readStatementAnalyzer;
        _returnStatementAnalyzer = returnStatementAnalyzer;
        _variableDeclarationStatementAnalyzer = variableDeclarationStatementAnalyzer;
        _whileStatementAnalyzer = whileStatementAnalyzer;
        _scopeAnalyzer = scopeAnalyzer;
        _anonymousFunctionDeclarationExpressionAnalyzer = anonymousFunctionDeclarationExpressionAnalyzer;
        _binaryExpressionAnalyzer = binaryExpressionAnalyzer;
        _functionCallExpressionAnalyzer = functionCallExpressionAnalyzer;
        _literalExpressionAnalyzer = literalExpressionAnalyzer;
        _newArrayExpressionAnalyzer = newArrayExpressionAnalyzer;
        _unaryExpressionAnalyzer = unaryExpressionAnalyzer;
        _variableReferenceExpressionAnalyzer = variableReferenceExpressionAnalyzer;
        _conditionScopeAnalyzer = conditionScopeAnalyzer;
        _parameterListAnalyzer = parameterListAnalyzer;
        _parameterAnalyzer = parameterAnalyzer;
    }

    public Type Analyze(ISemanticAnalyzerContext context, AstNode statement)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(statement);

        context.PushAstNode(statement);

        Type? type;

        switch (statement)
        {
            case AssignmentStatement assignmentStatement:
                type = _assignmentStatementAnalyzer.Analyze(this, context, assignmentStatement);

                break;
            case BreakStatement:
                type = _breakStatementAnalyzer.Analyze(context);

                break;
            case ContinueStatement:
                type = _continueStatementAnalyzer.Analyze(context);

                break;
            case ForStatement forStatement:
                type = _forStatementAnalyzer.Analyze(this, context, forStatement);

                break;
            case FunctionDeclarationStatement functionDeclarationStatement:
                type = _functionDeclarationStatementAnalyzer.Analyze(this, context, functionDeclarationStatement);

                break;
            case IfStatement ifStatement:
                type = _ifStatementAnalyzer.Analyze(this, context, ifStatement);

                break;
            case PrintStatement:
                type = _printStatementAnalyzer.Analyze();

                break;
            case ReadStatement:
                type = _readStatementAnalyzer.Analyze();

                break;
            case ReturnStatement returnStatement:
                type = _returnStatementAnalyzer.Analyze(this, context, returnStatement);

                break;
            case VariableDeclarationStatement variableDeclarationStatement:
                type = _variableDeclarationStatementAnalyzer.Analyze(this, context, variableDeclarationStatement);

                break;
            case WhileStatement whileStatement:
                type = _whileStatementAnalyzer.Analyze(this, context, whileStatement);

                break;
            case Scope scope:
                type = _scopeAnalyzer.Analyze(this, context, scope);

                break;
            case AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression:
                type = _anonymousFunctionDeclarationExpressionAnalyzer.Analyze
                    (this, context, anonymousFunctionDeclarationExpression);

                break;
            case BinaryExpression binaryExpression:
                type = _binaryExpressionAnalyzer.Analyze(this, context, binaryExpression);

                break;
            case FunctionCallExpression functionCallExpression:
                type = _functionCallExpressionAnalyzer.Analyze(this, context, functionCallExpression);

                break;
            case LiteralExpression literalExpression:
                type = _literalExpressionAnalyzer.Analyze(context, literalExpression);

                break;
            case NewArrayExpression newArrayExpression:
                type = _newArrayExpressionAnalyzer.Analyze(context, newArrayExpression);

                break;
            case UnaryExpression unaryExpression:
                type = _unaryExpressionAnalyzer.Analyze(this, context, unaryExpression);

                break;
            case VariableReferenceExpression variableReferenceExpression:
                type = _variableReferenceExpressionAnalyzer.Analyze(context, variableReferenceExpression);

                break;
            case ConditionScope conditionScope:
                type = _conditionScopeAnalyzer.Analyze(this, context, conditionScope);

                break;
            case ParameterList parameterList:
                type = _parameterListAnalyzer.Analyze(this, context, parameterList);

                break;
            case Parameter parameter:
                type = _parameterAnalyzer.Analyze(context, parameter);

                break;
            case Program program:
                type = _scopeAnalyzer.Analyze(this, context, program.Scope);

                break;
            default:
                throw new InvalidOperationException();
        }

        context.PopAstNode();

        return type;
    }
}
