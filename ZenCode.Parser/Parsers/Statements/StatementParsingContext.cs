using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Statements;
using ZenCode.Parser.Parsers.Expressions;

namespace ZenCode.Parser.Parsers.Statements;

public class StatementParsingContext : IStatementParsingContext
{
    private readonly IReadOnlyDictionary<TokenType, IStatementParsingStrategy> _statementParsingStrategies;

    public StatementParsingContext(IExpressionParser expressionParser)
    {
        _statementParsingStrategies = new Dictionary<TokenType, IStatementParsingStrategy>()
        {
            [TokenType.Identifier] = new AssignmentStatementParsingStrategy(expressionParser)
        };
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        throw new NotImplementedException();
    }
}