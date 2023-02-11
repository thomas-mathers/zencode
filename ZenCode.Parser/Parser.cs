using ZenCode.Grammar;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser;

public class Parser : IParser
{
    private readonly IStatementParsingContext _statementParsingContext;
    private readonly ITokenizer _tokenizer;

    protected Parser(ITokenizer tokenizer, IStatementParsingContext statementParsingContext)
    {
        _tokenizer = tokenizer;
        _statementParsingContext = statementParsingContext;
    }

    public Program Parse(string input)
    {
        var tokenStream = _tokenizer.Tokenize(input);

        var statements = new List<Statement>();

        while (tokenStream.Peek(0) != null)
            statements.Add(_statementParsingContext.Parse(tokenStream));

        return new Program(statements);
    }
}