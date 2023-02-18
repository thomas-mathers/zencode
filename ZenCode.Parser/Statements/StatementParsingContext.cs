using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class StatementParsingContext : IStatementParsingContext
{
    private readonly IReadOnlyDictionary<TokenType, IStatementParsingStrategy> _statementParsingStrategies;

    public StatementParsingContext(IExpressionParser expressionParser)
    {
        _statementParsingStrategies = new Dictionary<TokenType, IStatementParsingStrategy>
        {
            [TokenType.Identifier] = new AssignmentStatementParsingStrategy(expressionParser)
        };
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_statementParsingStrategies.TryGetValue(token.Type, out var statementParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return statementParsingStrategy.Parse(tokenStream);
    }
}