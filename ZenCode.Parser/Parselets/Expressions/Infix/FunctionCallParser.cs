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
        
        while (true)
        {
            var parameter = parser.ParseExpression();
            
            parameters.Add(parameter);

            var token = parser.Consume();

            if (token == null)
            {
                throw new ParseException();
            }
            
            if (token.Type == TokenType.Comma)
            {
                continue;
            }

            if (token.Type == TokenType.RightParenthesis)
            {
                break;
            }

            throw new ParseException();
        }

        return new FunctionCall(identifier.Identifier, parameters);
    }
}