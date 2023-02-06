using ZenCode.Lexer;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Infix;

public class FunctionCallParser : IInfixExpressionParser
{
    public Expression Parse(IParser parser, Expression lOperand, Token @operator)
    {
        if (lOperand is not IdentifierExpression identifier)
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
        return 1;
    }
}