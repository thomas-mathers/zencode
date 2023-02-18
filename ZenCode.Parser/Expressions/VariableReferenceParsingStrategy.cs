using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class VariableReferenceParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _parser;

    public VariableReferenceParsingStrategy(IExpressionParser parser)
    {
        _parser = parser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        var identifierToken = tokenStream.Consume();

        if (identifierToken.Type != TokenType.Identifier)
        {
            throw new UnexpectedTokenException();
        }

        var indexExpressions = new List<Expression>();

        if (!tokenStream.Match(TokenType.LeftBracket))
        {
            return new VariableReferenceExpression(identifierToken, indexExpressions);
        }

        if (tokenStream.Match(TokenType.RightBracket))
        {
            throw new MissingIndexExpressionException();
        }

        do
        {
            indexExpressions.Add(_parser.Parse(tokenStream));
        } while (tokenStream.Match(TokenType.Comma));

        tokenStream.Consume(TokenType.RightBracket);

        return new VariableReferenceExpression(identifierToken, indexExpressions);
    }
}