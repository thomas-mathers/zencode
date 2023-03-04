using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser;

public class ParserFactory : IParserFactory
{
    private readonly ITokenizer _tokenizer;
    private readonly IStatementParser _statementParser;

    public ParserFactory(ITokenizerFactory tokenizerFactory, IStatementParserFactory statementParserFactory)
    {
        _tokenizer = tokenizerFactory.Create();
        _statementParser = statementParserFactory.Create();
    }
    
    public IParser Create()
    {
        return new Parser(_tokenizer, _statementParser);
    }
}