using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class WhileStatementParsingStrategy : IWhileStatementParsingStrategy
{
    public WhileStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        tokenStream.Consume(TokenType.While);

        var conditionScope = parser.ParseConditionScope(tokenStream);

        return new WhileStatement(conditionScope);
    }
}
