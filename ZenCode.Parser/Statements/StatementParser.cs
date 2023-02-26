using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements;

public class StatementParser : IStatementParser
{
    public IReadOnlyDictionary<TokenType, IStatementParsingStrategy> Strategies { get; set; } =
        new Dictionary<TokenType, IStatementParsingStrategy>();

    public Statement Parse(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!Strategies.TryGetValue(token.Type, out var statementParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return statementParsingStrategy.Parse(tokenStream);
    }
}