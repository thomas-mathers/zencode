using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class VariableReferenceParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionListParser _expressionListParser;

    public VariableReferenceParsingStrategy(IExpressionListParser expressionListParser)
    {
        _expressionListParser = expressionListParser;
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

        var indexExpressions = _expressionListParser.Parse(tokenStream);

        tokenStream.Consume(TokenType.RightBracket);

        return new VariableReferenceExpression(identifierToken) { Indices = indexExpressions };
    }
}