using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class BinaryExpressionParsingStrategy : IBinaryExpressionParsingStrategy
{
    public BinaryExpression Parse
    (
        IParser parser,
        ITokenStream tokenStream,
        Expression lOperand,
        TokenType operatorTokenType,
        int precedence,
        bool isRightAssociative
    )
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        ArgumentNullException.ThrowIfNull(lOperand);
        
        var operatorToken = tokenStream.Consume(operatorTokenType);

        var rOperand = parser.ParseExpression(tokenStream, isRightAssociative ? precedence - 1 : precedence);

        return new BinaryExpression
        {
            Operator = operatorToken,
            LeftOperand = lOperand,
            RightOperand = rOperand
        };
    }
}
