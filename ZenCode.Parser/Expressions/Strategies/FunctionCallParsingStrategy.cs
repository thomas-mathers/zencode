using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class FunctionCallParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly IParser _parser;

    public FunctionCallParsingStrategy(IParser parser, int precedence)
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

        if (tokenStream.Match(TokenType.RightParenthesis))
        {
            return new FunctionCallExpression(variableReferenceExpression);
        }

        var arguments = _parser.ParseExpressionList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCallExpression(variableReferenceExpression) { Arguments = arguments };
    }
}