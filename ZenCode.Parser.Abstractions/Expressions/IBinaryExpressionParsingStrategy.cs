using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies
{
    public interface IBinaryExpressionParsingStrategy
    {
        BinaryExpression Parse(IParser parser, ITokenStream tokenStream, Expression lOperand,
            TokenType operatorTokenType, int precedence, bool isRightAssociative);
    }
}