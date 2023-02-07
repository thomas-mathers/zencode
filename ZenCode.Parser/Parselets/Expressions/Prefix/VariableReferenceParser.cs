using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Prefix;

public class VariableReferenceParser : IPrefixExpressionParser
{
    public Expression Parse(IParser parser, Token identifier)
    {
        if (!parser.TokenStream.Match(TokenType.LeftBracket))
        {
            return new VariableReferenceExpression(identifier, Array.Empty<Expression>());
        }

        var indices = new List<Expression>();
        
        do
        {
            indices.Add(parser.ParseExpression());
        } 
        while (parser.TokenStream.Match(TokenType.Comma));

        parser.TokenStream.Consume(TokenType.RightBracket);
        
        return new VariableReferenceExpression(identifier, indices);
    }
}