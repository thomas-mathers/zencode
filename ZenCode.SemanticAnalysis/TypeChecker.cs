using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis;

public class TypeChecker
{
    private readonly ISymbolTable _symbolTable;

    public TypeChecker(ISymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }

    public Type DetermineType(Expression expression)
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
        var lType = DetermineType(binaryExpression.LeftOperand);
        var rType = DetermineType(binaryExpression.RightOperand);

        if (lType != rType)
        {
            throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
        }

        var isComparisonOperator = binaryExpression.Operator.Type is TokenType.LessThan
            or TokenType.LessThanOrEqual
            or TokenType.GreaterThan
            or TokenType.GreaterThanOrEqual
            or TokenType.Equals
            or TokenType.NotEquals;

        return isComparisonOperator ? new BooleanType() : lType;
    }

    private Type DetermineType(FunctionCallExpression functionCallExpression)
    {
        var expressionType = DetermineType(functionCallExpression.Expression);

        if (expressionType is not FunctionType functionType)
        {
            throw new InvokingNonFunctionTypeException();
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
