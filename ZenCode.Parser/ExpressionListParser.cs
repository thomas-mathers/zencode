using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser;

public class ExpressionListParser : IExpressionListParser
{
    public ExpressionList ParseExpressionList(IParser parser, ITokenStream tokenStream)
    {
        var expressions = new List<Expression>();

        while (true)
        {
            expressions.Add(parser.ParseExpression(tokenStream));

            if (!tokenStream.Match(TokenType.Comma))
            {
                break;
            }

            tokenStream.Consume(TokenType.Comma);
        }

        return new ExpressionList { Expressions = expressions };
    }
}