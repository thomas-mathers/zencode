using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class FunctionCallParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly IExpressionParser _parser;

    public FunctionCallParsingStrategy(IExpressionParser parser, int precedence)
    {
        _parser = parser;
        Precedence = precedence;
    }

    public int Precedence { get; }

    public Expression Parse(ITokenStream tokenStream, Expression lOperand)
    {
        if (lOperand is not VariableReferenceExpression variableReferenceExpression)
        {
            throw new UnexpectedTokenException();
        }

        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameterExpressions = new List<Expression>();

        if (tokenStream.Match(TokenType.RightParenthesis))
        {
            return new FunctionCall(variableReferenceExpression, parameterExpressions);
        }

        do
        {
            parameterExpressions.Add(_parser.Parse(tokenStream));
        } while (tokenStream.Match(TokenType.Comma));

        tokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCall(variableReferenceExpression, parameterExpressions);
    }
}