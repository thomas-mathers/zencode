using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Prefix;

public class VariableReferenceParser : IPrefixExpressionParser
{
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        if (!tokenStream.Match(TokenType.LeftBracket))
        {
            return new VariableReferenceExpression(token, Array.Empty<Expression>());
        }

        var indices = new List<Expression>();
        
        do
        {
            indices.Add(parser.Parse(tokenStream));
        } 
        while (tokenStream.Match(TokenType.Comma));

        tokenStream.Consume(TokenType.RightBracket);
        
        return new VariableReferenceExpression(token, indices);
    }
}