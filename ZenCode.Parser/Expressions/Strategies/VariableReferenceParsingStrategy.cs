using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class VariableReferenceParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public VariableReferenceParsingStrategy(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        var identifierToken = tokenStream.Consume();

        if (identifierToken.Type != TokenType.Identifier)
        {
            throw new UnexpectedTokenException();
        }

        if (!tokenStream.Match(TokenType.LeftBracket))
        {
            return new VariableReferenceExpression(identifierToken);
        }

        if (tokenStream.Match(TokenType.RightBracket))
        {
            throw new MissingIndexExpressionException();
        }

        var indexExpressions = _expressionParser.ParseList(tokenStream);

        tokenStream.Consume(TokenType.RightBracket);

        return new VariableReferenceExpression(identifierToken) { Indices = indexExpressions };
    }
}