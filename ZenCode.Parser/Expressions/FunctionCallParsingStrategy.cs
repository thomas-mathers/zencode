using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class FunctionCallParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly IExpressionListParser _expressionListParser;

    public FunctionCallParsingStrategy(IExpressionListParser expressionListParser, int precedence)
    {
        _expressionListParser = expressionListParser;
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

        if (tokenStream.Match(TokenType.RightParenthesis))
        {
            return new FunctionCall(variableReferenceExpression);
        }

        var parameterExpressions = _expressionListParser.Parse(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCall(variableReferenceExpression) { Parameters = parameterExpressions };
    }
}