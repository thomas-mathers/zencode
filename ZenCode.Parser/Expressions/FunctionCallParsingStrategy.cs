using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class FunctionCallParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly int _precedence;

    public FunctionCallParsingStrategy(int precedence)
    {
        _precedence = precedence;
    }

    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator)
    {
        if (lOperand is not VariableReferenceExpression variableReferenceExpression)
        {
            throw new SyntaxError();
        }
        
        var parameters = new List<Expression>();

        if (!tokenStream.Match(TokenType.RightParenthesis))
        {
            do
            {
                parameters.Add(parser.Parse(tokenStream));
            } while (tokenStream.Match(TokenType.Comma));

            tokenStream.Consume(TokenType.RightParenthesis);   
        }

        return new FunctionCall(variableReferenceExpression, parameters);
    }

    public int GetPrecedence()
    {
        return _precedence;
    }
}