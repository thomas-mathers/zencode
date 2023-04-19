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
        ArgumentNullException.ThrowIfNull(program);

        foreach (var statement in program.Scope.Statements)
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
            case Scope scope:
                Analyze(scope, null);
                break;
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
            default:
                throw new InvalidOperationException();
        }
    }

    private void Analyze(AssignmentStatement assignmentStatement)
    {
        var symbol = _symbolTable.ResolveSymbol(assignmentStatement.VariableReference.Identifier.Text);

        if (symbol == null)
        {
            throw new UndeclaredIdentifierException(assignmentStatement.VariableReference.Identifier);
        }

        var expressionType = DetermineType(assignmentStatement.Value);

        if (symbol.Type != expressionType)
        {
            throw new TypeMismatchException(symbol.Type, expressionType);
        }
    }

    private void Analyze(BreakStatement breakStatement)
    {
        var environment = _symbolTable.Environments.FirstOrDefault(e => e.Statement is WhileStatement or ForStatement);

        if (environment == null)
        {
            throw new InvalidBreakException();
        }
    }

    private void Analyze(ContinueStatement continueStatement)
    {
        var environment = _symbolTable.Environments.FirstOrDefault(e => e.Statement is WhileStatement or ForStatement);

        if (environment == null)
        {
            throw new InvalidContinueException();
        }
    }

    private void Analyze(ForStatement forStatement)
    {
        Analyze(forStatement.Initializer);

        var conditionType = DetermineType(forStatement.Condition);

        if (conditionType is not BooleanType)
        {
            throw new TypeMismatchException(new BooleanType(), conditionType);
        }

        Analyze(forStatement.Iterator);
        Analyze(forStatement.Body, forStatement);
    }

    private void Analyze(FunctionDeclarationStatement functionDeclarationStatement)
    {
        var type = new FunctionType
        (
            functionDeclarationStatement.ReturnType,
            new TypeList
            (
                functionDeclarationStatement.Parameters.Parameters.Select(parameter => parameter.Type).ToArray()
            )
        );

        var symbol = new Symbol(functionDeclarationStatement.Name, type);

        _symbolTable.DefineSymbol(symbol);
        
        Analyze(functionDeclarationStatement.Body, functionDeclarationStatement);
    }

    private void Analyze(IfStatement ifStatement)
    {
        Analyze(ifStatement.ThenScope, ifStatement);

        foreach (var conditionScope in ifStatement.ElseIfScopes)
        {
            Analyze(conditionScope, ifStatement);
        }

        if (ifStatement.ElseScope != null)
        {
            Analyze(ifStatement.ElseScope, ifStatement);
        }
    }

    private void Analyze(PrintStatement printStatement)
    {
    }

    private void Analyze(ReadStatement readStatement)
    {
    }

    private void Analyze(ReturnStatement returnStatement)
    {
        var environment = _symbolTable.Environments.FirstOrDefault
            (e => e.Statement is FunctionDeclarationStatement or AnonymousFunctionDeclarationExpression);

        if (environment == null)
        {
            throw new InvalidReturnException();
        }
        
        var returnType = returnStatement.Value == null
            ? new VoidType()
            : DetermineType(returnStatement.Value);

        var functionReturnType = environment.Statement switch
        {
            FunctionDeclarationStatement functionDeclarationStatement => functionDeclarationStatement.ReturnType,
            AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression => anonymousFunctionDeclarationExpression.ReturnType,
            _ => throw new InvalidOperationException()
        };
        
        if (returnType != functionReturnType)
        {
            throw new TypeMismatchException(functionReturnType, returnType);
        }
    }

    private void Analyze(VariableDeclarationStatement variableDeclarationStatement)
    {
        Analyze(variableDeclarationStatement.Value);
        
        var type = DetermineType(variableDeclarationStatement.Value);

        var symbol = new Symbol(variableDeclarationStatement.VariableName, type);

        _symbolTable.DefineSymbol(symbol);
    }

    private void Analyze(WhileStatement whileStatement)
    {
        Analyze(whileStatement.ConditionScope, whileStatement);
    }

    private void Analyze(ConditionScope conditionScope, Statement? statement)
    {
        var conditionType = DetermineType(conditionScope.Condition);

        if (conditionType is not BooleanType)
        {
            throw new TypeMismatchException(new BooleanType(), conditionType);
        }

        Analyze(conditionScope.Scope, statement);
    }

    private void Analyze(Scope scope, Statement? statement)
    {
        _symbolTable.PushEnvironment(statement);

        foreach (var s in scope.Statements)
        {
            Analyze(s);
        }

        _symbolTable.PopEnvironment();
    }
    
    private void Analyze(AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression)
    {
        Analyze(anonymousFunctionDeclarationExpression.Body, anonymousFunctionDeclarationExpression);
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

    private Type DetermineType(Expression expression)
    {
        switch (expression)
        {
            case AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression:
                return DetermineType(anonymousFunctionDeclarationExpression);
            case BinaryExpression binaryExpression:
                return DetermineType(binaryExpression);
            case FunctionCallExpression functionCallExpression:
                return DetermineType(functionCallExpression);
            case LiteralExpression literalExpression:
                return DetermineType(literalExpression);
            case NewArrayExpression newArrayExpression:
                return DetermineType(newArrayExpression);
            case UnaryExpression unaryExpression:
                return DetermineType(unaryExpression);
            case VariableReferenceExpression variableReferenceExpression:
                return DetermineType(variableReferenceExpression);
            default:
                throw new InvalidOperationException();
        }
    }

    private Type DetermineType(AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression)
    {
        return new FunctionType
        (
            anonymousFunctionDeclarationExpression.ReturnType,
            new TypeList(anonymousFunctionDeclarationExpression.Parameters.Parameters.Select(t => t.Type).ToArray())
        );
    }

    private Type DetermineType(BinaryExpression binaryExpression)
    {
        var lType = DetermineType(binaryExpression.Left);
        var rType = DetermineType(binaryExpression.Right);

        if (lType != rType)
        {
            throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
        }

        switch (binaryExpression.Operator.Type)
        {
            case TokenType.Plus:
                switch (lType)
                {
                    case IntegerType:
                        return new IntegerType();
                    case FloatType:
                        return new FloatType();
                    case StringType:
                        return new StringType();
                    default:
                        throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }
            case TokenType.Minus:
            case TokenType.Multiplication:
            case TokenType.Division:
            case TokenType.Modulus:
            case TokenType.Exponentiation:
                switch (lType)
                {
                    case IntegerType:
                        return new IntegerType();
                    case FloatType:
                        return new FloatType();
                    default:
                        throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }
            case TokenType.LessThan:
            case TokenType.LessThanOrEqual:
            case TokenType.GreaterThan:
            case TokenType.GreaterThanOrEqual:
                switch (lType)
                {
                    case IntegerType:
                        return new BooleanType();
                    case FloatType:
                        return new BooleanType();
                    case StringType:
                        return new BooleanType();
                    default:
                        throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }
            case TokenType.Equals:
            case TokenType.NotEquals:
                return new BooleanType();
            case TokenType.And:
            case TokenType.Or:
                switch (lType)
                {
                    case BooleanType:
                        return new IntegerType();
                    default:
                        throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private Type DetermineType(FunctionCallExpression functionCallExpression)
    {
        var expressionType = DetermineType(functionCallExpression.FunctionReference);

        if (expressionType is not FunctionType functionType)
        {
            throw new InvokingNonFunctionTypeException();
        }

        if (functionType.ParameterTypes.Types.Count != functionCallExpression.Arguments.Expressions.Count)
        {
            throw new IncorrectNumberOfParametersException
                (functionType.ParameterTypes.Types.Count, functionCallExpression.Arguments.Expressions.Count);
        }

        for (var i = 0; i < functionType.ParameterTypes.Types.Count; i++)
        {
            var parameterType = functionType.ParameterTypes.Types[i];
            var argumentType = DetermineType(functionCallExpression.Arguments.Expressions[i]);

            if (parameterType != argumentType)
            {
                throw new TypeMismatchException(parameterType, argumentType);
            }
        }

        return functionType.ReturnType;
    }

    private Type DetermineType(LiteralExpression literalExpression)
    {
        switch (literalExpression.Token.Type)
        {
            case TokenType.BooleanLiteral:
                return new BooleanType();
            case TokenType.IntegerLiteral:
                return new IntegerType();
            case TokenType.FloatLiteral:
                return new FloatType();
            case TokenType.StringLiteral:
                return new StringType();
            default:
                throw new InvalidOperationException();
        }
    }

    private Type DetermineType(NewArrayExpression newArrayExpression)
    {
        return new ArrayType(newArrayExpression.Type);
    }

    private Type DetermineType(UnaryExpression unaryExpression)
    {
        return DetermineType(unaryExpression.Expression);
    }

    private Type DetermineType(VariableReferenceExpression variableReferenceExpression)
    {
        var symbol = _symbolTable.ResolveSymbol(variableReferenceExpression.Identifier.Text);

        if (symbol == null)
        {
            throw new UndeclaredIdentifierException(variableReferenceExpression.Identifier);
        }

        return symbol.Type;
    }
}
