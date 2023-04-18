using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.SemanticAnalysis.Exceptions;

namespace ZenCode.SemanticAnalysis;

public class SemanticAnalyzer
{
    private readonly SymbolTable _symbolTable;
    private readonly TypeChecker _typeChecker;

    public SemanticAnalyzer(SymbolTable symbolTable, TypeChecker typeChecker)
    {
        _symbolTable = symbolTable;
        _typeChecker = typeChecker;
    }
    
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
        var symbol = _symbolTable.ResolveSymbol(assignmentStatement.VariableReferenceExpression.Identifier.Text);
        
        if (symbol == null)
        {
            throw new UndeclaredIdentifierException(assignmentStatement.VariableReferenceExpression.Identifier);
        }
        
        var expressionType = _typeChecker.DetermineType(assignmentStatement.Expression);
        
        if (symbol.Type != expressionType)
        {
            throw new TypeMismatchException(symbol.Type, expressionType);
        }
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
    }

    private void Analyze(ReadStatement readStatement)
    {
    }

    private void Analyze(ReturnStatement returnStatement)
    {
    }

    private void Analyze(VariableDeclarationStatement variableDeclarationStatement)
    {
        var type = _typeChecker.DetermineType(variableDeclarationStatement.Expression);

        var symbol = new Symbol(variableDeclarationStatement.Identifier, type);
        
        _symbolTable.DefineSymbol(symbol);
    }

    private void Analyze(WhileStatement whileStatement)
    {
    }
    
    private void Analyze(ConditionScope conditionScope)
    {
        Analyze(conditionScope.Condition);
        Analyze(conditionScope.Scope);
    }

    private void Analyze(Scope scope)
    {
        _symbolTable.PushEnvironment();
        
        foreach (var statement in scope.Statements)
        {
            Analyze(statement);
        }
        
        _symbolTable.PopEnvironment();
    }
}
