using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class FunctionCallParsingStrategy : IFunctionCallParsingStrategy
{
    public FunctionCallExpression Parse(IParser parser, ITokenStream tokenStream, Expression functionReference)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        ArgumentNullException.ThrowIfNull(functionReference);
        
        tokenStream.Consume(TokenType.LeftParenthesis);

        var arguments = tokenStream.Match(TokenType.RightParenthesis)
            ? new ExpressionList()
            : parser.ParseExpressionList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCallExpression
        {
            FunctionReference = functionReference,
            Arguments = arguments
        };
    }
}
