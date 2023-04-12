using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser;

public class ArrayIndexExpressionListParser : IArrayIndexExpressionListParser
{
    public ArrayIndexExpressionList Parse(IParser parser, ITokenStream tokenStream)
    {
        var indexExpressions = new List<Expression>();

        while (tokenStream.Match(TokenType.LeftBracket))
        {
            tokenStream.Consume(TokenType.LeftBracket);

            var indexExpression = parser.ParseExpression(tokenStream);

            indexExpressions.Add(indexExpression);

            tokenStream.Consume(TokenType.RightBracket);
        }

        return new ArrayIndexExpressionList { Expressions = indexExpressions };
    }
}
