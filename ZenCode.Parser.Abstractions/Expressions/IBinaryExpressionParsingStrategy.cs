using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IBinaryExpressionParsingStrategy
{
    BinaryExpression Parse(IParser parser, ITokenStream tokenStream, Expression lOperand,
        TokenType operatorTokenType, int precedence, bool isRightAssociative);
}