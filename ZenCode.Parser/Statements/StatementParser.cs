using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class StatementParser : IStatementParser
{
    private readonly IReadOnlyDictionary<TokenType, IStatementParsingStrategy> _statementParsingStrategies;

    public StatementParser(IExpressionParser expressionParser)
    {
        _statementParsingStrategies = new Dictionary<TokenType, IStatementParsingStrategy>
        {
            [TokenType.Identifier] = new AssignmentStatementParsingStrategy(expressionParser),
            [TokenType.If] = new IfStatementParsingStrategy(this, expressionParser),
            [TokenType.While] = new WhileStatementParsingStrategy(this, expressionParser),
            [TokenType.Var] = new VariableDeclarationStatementParsingStrategy(expressionParser)
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