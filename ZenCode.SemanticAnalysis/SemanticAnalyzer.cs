using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers;
using ZenCode.SemanticAnalysis.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis;

public class SemanticAnalyzer : ISemanticAnalyzer
{
    public Type Analyze(ISemanticAnalyzerContext context, AstNode statement)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(statement);

        context.PushAstNode(statement);

        Type? type;

        switch (statement)
        {
            case AssignmentStatement assignmentStatement:
                type = AssignmentStatementAnalyzer.Analyze(this, context, assignmentStatement);

                break;
            case BreakStatement:
                type = BreakStatementAnalyzer.Analyze(context);

                break;
            case ContinueStatement:
                type = ContinueStatementAnalyzer.Analyze(context);

                break;
            case ForStatement forStatement:
                type = ForStatementAnalyzer.Analyze(this, context, forStatement);

                break;
            case FunctionDeclarationStatement functionDeclarationStatement:
                type = FunctionDeclarationStatementAnalyzer.Analyze(this, context, functionDeclarationStatement);

                break;
            case IfStatement ifStatement:
                type = IfStatementAnalyzer.Analyze(this, context, ifStatement);

                break;
            case PrintStatement:
                type = PrintStatementAnalyzer.Analyze();

                break;
            case ReadStatement:
                type = ReadStatementAnalyzer.Analyze();

                break;
            case ReturnStatement returnStatement:
                type = ReturnStatementAnalyzer.Analyze(this, context, returnStatement);

                break;
            case VariableDeclarationStatement variableDeclarationStatement:
                type = VariableDeclarationStatementAnalyzer.Analyze(this, context, variableDeclarationStatement);

                break;
            case WhileStatement whileStatement:
                type = WhileStatementAnalyzer.Analyze(this, context, whileStatement);

                break;
            case Scope scope:
                type = ScopeAnalyzer.Analyze(this, context, scope);

                break;
            case AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression:
                type = AnonymousFunctionDeclarationExpressionAnalyzer.Analyze
                    (this, context, anonymousFunctionDeclarationExpression);

                break;
            case BinaryExpression binaryExpression:
                type = BinaryExpressionAnalyzer.Analyze(this, context, binaryExpression);

                break;
            case FunctionCallExpression functionCallExpression:
                type = FunctionCallExpressionAnalyzer.Analyze(this, context, functionCallExpression);

                break;
            case LiteralExpression literalExpression:
                type = LiteralExpressionAnalyzer.Analyze(context, literalExpression);

                break;
            case NewArrayExpression newArrayExpression:
                type = NewArrayExpressionAnalyzer.Analyze(context, newArrayExpression);

                break;
            case UnaryExpression unaryExpression:
                type = UnaryExpressionAnalyzer.Analyze(this, context, unaryExpression);

                break;
            case VariableReferenceExpression variableReferenceExpression:
                type = VariableReferenceExpressionAnalyzer.Analyze(context, variableReferenceExpression);

                break;
            case ConditionScope conditionScope:
                type = ConditionScopeAnalyzer.Analyze(this, context, conditionScope);

                break;
            case ParameterList parameterList:
                type = ParameterListAnalyzer.Analyze(this, context, parameterList);

                break;
            case Parameter parameter:
                type = ParameterAnalyzer.Analyze(context, parameter);

                break;
            case Program program:
                type = ScopeAnalyzer.Analyze(this, context, program.Scope);

                break;
            default:
                throw new InvalidOperationException();
        }

        context.PopAstNode();

        return type;
    }
}
