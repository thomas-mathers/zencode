using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class BinaryExpressionParsingStrategy : IBinaryExpressionParsingStrategy
{
    public BinaryExpression Parse(IParser parser, ITokenStream tokenStream, Expression lOperand,
        TokenType operatorTokenType, int precedence, bool isRightAssociative)
    {
        var operatorToken = tokenStream.Consume(operatorTokenType);

        var rOperand = parser.ParseExpression(tokenStream, isRightAssociative ? precedence - 1 : precedence);

        return new BinaryExpression(lOperand, operatorToken, rOperand);
    }
}