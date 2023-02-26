using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class VariableReferenceParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IArgumentListParser _argumentListParser;

    public VariableReferenceParsingStrategy(IArgumentListParser argumentListParser)
    {
        _argumentListParser = argumentListParser;
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

        var indexExpressions = _argumentListParser.Parse(tokenStream);

        tokenStream.Consume(TokenType.RightBracket);

        return new VariableReferenceExpression(identifierToken) { Indices = indexExpressions };
    }
}