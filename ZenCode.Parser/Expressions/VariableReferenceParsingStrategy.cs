using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class VariableReferenceParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        if (token.Type != TokenType.Identifier)
        {
            throw new SyntaxError();
        }
        
        var indices = new List<Expression>();

        if (tokenStream.Match(TokenType.LeftBracket))
        {
            do
            {
                indices.Add(parser.Parse(tokenStream));
            } while (tokenStream.Match(TokenType.Comma));

            tokenStream.Consume(TokenType.RightBracket);   
        }

        return new VariableReferenceExpression(token, indices);
    }
}