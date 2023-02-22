using ZenCode.Grammar;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser;

public class Parser : IParser
{
    private readonly IStatementParser _statementParser;
    private readonly ITokenizer _tokenizer;

    public Parser(ITokenizer tokenizer, IStatementParser statementParser)
    {
        _tokenizer = tokenizer;
        _statementParser = statementParser;
    }

    public Program Parse(string input)
    {
        var tokenStream = _tokenizer.Tokenize(input);

        var statements = new List<Statement>();

        while (tokenStream.Peek(0) != null)
        {
            statements.Add(_statementParser.Parse(tokenStream));
        }

        return new Program(statements);
    }
}