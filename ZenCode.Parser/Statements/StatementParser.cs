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
        var scopeParser = new ScopeParser(this);
        var conditionScopeParser = new ConditionScopeParser(expressionParser, scopeParser);
        
        _statementParsingStrategies = new Dictionary<TokenType, IStatementParsingStrategy>
        {
            [TokenType.Identifier] = new AssignmentStatementParsingStrategy(expressionParser),
            [TokenType.If] = new IfStatementParsingStrategy(conditionScopeParser, scopeParser),
            [TokenType.While] = new WhileStatementParsingStrategy(conditionScopeParser),
            [TokenType.Var] = new VariableDeclarationStatementParsingStrategy(expressionParser),
            [TokenType.Print] = new PrintStatementParsingStrategy(expressionParser)
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