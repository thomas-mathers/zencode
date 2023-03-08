using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class PrintStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public PrintStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Print);

        var expression = _parser.ParseExpression(tokenStream);

        return new PrintStatement(expression);
    }
}