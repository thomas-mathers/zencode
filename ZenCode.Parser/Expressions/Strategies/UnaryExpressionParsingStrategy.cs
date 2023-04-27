using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Mappers;

namespace ZenCode.Parser.Expressions.Strategies;

public class UnaryExpressionParsingStrategy : IUnaryExpressionParsingStrategy
{
    public UnaryExpression Parse(IParser parser, ITokenStream tokenStream, TokenType operatorType)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        var operatorToken = tokenStream.Consume(operatorType);

        var expression = parser.ParseExpression(tokenStream);

        return new UnaryExpression
        {
            Operator = TokenTypeToUnaryOperatorTypeMapper.Map(operatorToken.Type),
            Expression = expression
        };
    }
}
