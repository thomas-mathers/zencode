using ZenCode.Lexer;
using ZenCode.Parser.Grammar;
using ZenCode.Parser.Grammar.Statements;
using ZenCode.Parser.Parsers.Statements;

namespace ZenCode.Parser.Parsers;

public class Parser : IParser
{
    private readonly ITokenizer _tokenizer;
    private readonly IStatementParsingContext _statementParsingContext;
    
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
        {
            statements.Add(_statementParsingContext.Parse(tokenStream));   
        }

        return new Program(statements);
    }
}