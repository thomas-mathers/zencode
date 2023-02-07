using ZenCode.Lexer;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Infix;

public class FunctionCallParser : IInfixExpressionParser
{
    private readonly int _precedence;
    
    public FunctionCallParser(int precedence)
    {
        _precedence = precedence;
    }

    public Expression Parse(IParser parser, Expression lOperand, Token @operator)
    {
        if (lOperand is not VariableReferenceExpression identifier)
        {
            throw new ParseException();   
        }

        var parameters = new List<Expression>();

        if (parser.TokenStream.Match(TokenType.RightParenthesis))
        {
            return new FunctionCall(identifier.Identifier, parameters);   
        }

        do
        {
            parameters.Add(parser.ParseExpression());
        } 
        while (parser.TokenStream.Match(TokenType.Comma));

        parser.TokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCall(identifier.Identifier, parameters);
    }

    public int GetPrecedence()
    {
        return _precedence;
    }
}