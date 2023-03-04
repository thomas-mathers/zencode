using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements;

public class StatementParser : IStatementParser
{
    private readonly IDictionary<TokenType, IStatementParsingStrategy> _statementParsingStrategies =
        new Dictionary<TokenType, IStatementParsingStrategy>();

    public Statement ParseStatement(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_statementParsingStrategies.TryGetValue(token.Type, out var statementParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return statementParsingStrategy.Parse(tokenStream);
    }

    public Scope ParseScope(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftBrace);

        var statements = new List<Statement>();

        while (!tokenStream.Match(TokenType.RightBrace))
        {
            statements.Add(ParseStatement(tokenStream));
        }

        return new Scope { Statements = statements };
    }

    public void SetStatementParsingStrategy(TokenType tokenType, IStatementParsingStrategy parsingStrategy)
    {
        _statementParsingStrategies[tokenType] = parsingStrategy;
    }
}