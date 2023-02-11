using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class VariableReferenceParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        if (!tokenStream.Match(TokenType.LeftBracket))
            return new VariableReferenceExpression(token, Array.Empty<Expression>());

        var indices = new List<Expression>();

        do
        {
            indices.Add(parser.Parse(tokenStream));
        } while (tokenStream.Match(TokenType.Comma));

        tokenStream.Consume(TokenType.RightBracket);

        return new VariableReferenceExpression(token, indices);
    }
}