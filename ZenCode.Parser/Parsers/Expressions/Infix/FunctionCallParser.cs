using ZenCode.Lexer;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Infix;

public class FunctionCallParser : IInfixExpressionParser
{
    private readonly int _precedence;
    
    public FunctionCallParser(int precedence)
    {
        _precedence = precedence;
    }

    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator)
    {
        if (lOperand is not VariableReferenceExpression identifier)
        {
            throw new ParseException();   
        }

        var parameters = new List<Expression>();

        if (tokenStream.Match(TokenType.RightParenthesis))
        {
            return new FunctionCall(identifier.Identifier, parameters);   
        }

        do
        {
            parameters.Add(parser.Parse(tokenStream));
        } 
        while (tokenStream.Match(TokenType.Comma));

        tokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCall(identifier.Identifier, parameters);
    }

    public int GetPrecedence()
    {
        return _precedence;
    }
}