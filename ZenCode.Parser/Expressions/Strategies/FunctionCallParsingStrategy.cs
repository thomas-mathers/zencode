using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class FunctionCallParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly IArgumentListParser _argumentListParser;

    public FunctionCallParsingStrategy(IArgumentListParser argumentListParser, int precedence)
    {
        _argumentListParser = argumentListParser;
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
            return new FunctionCallExpression(variableReferenceExpression);
        }

        var arguments = _argumentListParser.Parse(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCallExpression(variableReferenceExpression) { Arguments = arguments };
    }
}