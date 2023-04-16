using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis;

public class TypeChecker
{
    public static Type CheckType(SymbolTable symbolTable, Expression expression)
    {
        switch (expression)
        {
            case AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression:
                return CheckType(anonymousFunctionDeclarationExpression);
            case BinaryExpression binaryExpression:
                return CheckType(symbolTable, binaryExpression);
            case FunctionCallExpression functionCallExpression:
                return CheckType(symbolTable, functionCallExpression);
            case LiteralExpression literalExpression:
                return CheckType(literalExpression);
            case NewArrayExpression newArrayExpression:
                return CheckType(newArrayExpression);
            case UnaryExpression unaryExpression:
                return CheckType(symbolTable, unaryExpression);
            case VariableReferenceExpression variableReferenceExpression:
                return CheckType(symbolTable, variableReferenceExpression);
            default:
                throw new InvalidOperationException();
        }
    }

    private static Type CheckType(AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression)
    {
        return new FunctionType
        (
            anonymousFunctionDeclarationExpression.ReturnType,
            new TypeList
            {
                Types = anonymousFunctionDeclarationExpression.Parameters.Parameters.Select(t => t.Type).ToArray()
            }
        );
    }

    private static Type CheckType(SymbolTable symbolTable, BinaryExpression binaryExpression)
    {
        var lType = CheckType(symbolTable, binaryExpression.LeftOperand);
        var rType = CheckType(symbolTable, binaryExpression.RightOperand);

        if (lType != rType)
        {
            throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
        }

        switch (binaryExpression.Operator.Type)
        {
            case TokenType.Plus:
                if (lType is not IntegerType and not FloatType and not StringType)
                {
                    throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }
                
                break;
            case TokenType.Minus:
            case TokenType.Multiplication:
            case TokenType.Division:
            case TokenType.Modulus:
            case TokenType.Exponentiation:
                if (lType is not IntegerType and not FloatType)
                {
                    throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }

                break;
            case TokenType.LessThan:
            case TokenType.LessThanOrEqual:
            case TokenType.GreaterThan:
            case TokenType.GreaterThanOrEqual:
                if (lType is not IntegerType and not FloatType and not StringType)
                {
                    throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }

                break;
            case TokenType.Equals:
            case TokenType.NotEquals:
                break;
            case TokenType.And:
            case TokenType.Or:
                if (lType is not BooleanType)
                {
                    throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return lType;
    }
    
    private static Type CheckType(SymbolTable symbolTable, FunctionCallExpression functionCallExpression)
    {
        var expressionType = CheckType(symbolTable, functionCallExpression.Expression);

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
            var argumentType = CheckType(symbolTable, functionCallExpression.Arguments.Expressions[i]);

            if (parameterType != argumentType)
            {
                throw new TypeMismatchException(parameterType, argumentType);
            }
        }

        return functionType.ReturnType;
    }

    private static Type CheckType(LiteralExpression literalExpression)
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

    private static Type CheckType(NewArrayExpression newArrayExpression)
    {
        return new ArrayType(newArrayExpression.Type);
    }

    private static Type CheckType(SymbolTable symbolTable, UnaryExpression unaryExpression)
    {
        return CheckType(symbolTable, unaryExpression.Expression);
    }

    private static Type CheckType(SymbolTable symbolTable, VariableReferenceExpression variableReferenceExpression)
    {
        var symbol = symbolTable.ResolveSymbol(variableReferenceExpression.Identifier.Text);

        if (symbol == null)
        {
            throw new UndeclaredIdentifierException(variableReferenceExpression.Identifier);
        }

        return symbol.Type;
    }
}
