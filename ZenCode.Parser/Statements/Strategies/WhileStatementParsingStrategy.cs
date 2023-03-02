using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class WhileStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public WhileStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.While);

        var conditionScope = _parser.ParseConditionScope(tokenStream);

        return new WhileStatement(conditionScope);
    }
}