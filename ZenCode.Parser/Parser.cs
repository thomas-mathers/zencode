using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;

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

    public Program Parse(string program)
    {
        return ParseProgram(_tokenizer.Tokenize(program));
    }

    private Program ParseProgram(ITokenStream tokenStream)
    {
        var statements = new List<Statement>();

        while (tokenStream.Peek(0) != null)
        {
            statements.Add(_statementParser.ParseStatement(tokenStream));
        }

        return new Program(statements);
    }
}