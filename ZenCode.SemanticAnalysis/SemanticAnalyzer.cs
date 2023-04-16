using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis;

public class SemanticAnalyzer
{
    private readonly SymbolTable _symbolTable = new();

    public void Analyze(Program program)
    {
        foreach (var statement in program.Statements)
        {
            Analyze(statement);
        }
    }

    private void Analyze(Statement statement)
    {
        switch (statement)
        {
            case AssignmentStatement assignmentStatement:
                Analyze(assignmentStatement);

                break;
            case BreakStatement breakStatement:
                Analyze(breakStatement);

                break;
            case ContinueStatement continueStatement:
                Analyze(continueStatement);

                break;
            case ForStatement forStatement:
                Analyze(forStatement);

                break;
            case FunctionDeclarationStatement functionDeclarationStatement:
                Analyze(functionDeclarationStatement);

                break;
            case IfStatement ifStatement:
                Analyze(ifStatement);

                break;
            case PrintStatement printStatement:
                Analyze(printStatement);

                break;
            case ReadStatement readStatement:
                Analyze(readStatement);

                break;
            case ReturnStatement returnStatement:
                Analyze(returnStatement);

                break;
            case VariableDeclarationStatement variableDeclarationStatement:
                Analyze(variableDeclarationStatement);

                break;
            case WhileStatement whileStatement:
                Analyze(whileStatement);

                break;
            default:
                throw new InvalidOperationException();
        }
    }

    private void Analyze(AssignmentStatement assignmentStatement)
    {
    }

    private void Analyze(BreakStatement breakStatement)
    {
    }

    private void Analyze(ContinueStatement continueStatement)
    {
    }

    private void Analyze(ForStatement forStatement)
    {
    }

    private void Analyze(FunctionDeclarationStatement functionDeclarationStatement)
    {
    }

    private void Analyze(IfStatement ifStatement)
    {
    }

    private void Analyze(PrintStatement printStatement)
    {
        Analyze(printStatement.Expression);
    }

    private void Analyze(ReadStatement readStatement)
    {
        Analyze(readStatement.VariableReferenceExpression);
    }

    private void Analyze(ReturnStatement returnStatement)
    {
        if (returnStatement.Expression != null)
        {
            Analyze(returnStatement.Expression);
        }
    }

    private void Analyze(VariableDeclarationStatement variableDeclarationStatement)
    {
    }

    private void Analyze(WhileStatement whileStatement)
    {
        Analyze(whileStatement.ConditionScope);
    }

    private void Analyze(Expression expression)
    {
        switch (expression)
        {
            case AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression:
                Analyze(anonymousFunctionDeclarationExpression);

                break;
            case BinaryExpression binaryExpression:
                Analyze(binaryExpression);

                break;
            case FunctionCallExpression functionCallExpression:
                Analyze(functionCallExpression);

                break;
            case LiteralExpression literalExpression:
                Analyze(literalExpression);

                break;
            case NewArrayExpression newArrayExpression:
                Analyze(newArrayExpression);

                break;
            case UnaryExpression unaryExpression:
                Analyze(unaryExpression);

                break;
            case VariableReferenceExpression variableReferenceExpression:
                Analyze(variableReferenceExpression);

                break;
        }
    }

    private void Analyze(AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression)
    {
    }

    private void Analyze(BinaryExpression binaryExpression)
    {
    }

    private void Analyze(FunctionCallExpression functionCallExpression)
    {
    }

    private void Analyze(LiteralExpression literalExpression)
    {
    }

    private void Analyze(NewArrayExpression newArrayExpression)
    {
    }

    private void Analyze(UnaryExpression unaryExpression)
    {
    }

    private void Analyze(VariableReferenceExpression variableReferenceExpression)
    {
    }

    private void Analyze(ConditionScope conditionScope)
    {
    }

    private void Analyze(Scope scope)
    {
    }
}
