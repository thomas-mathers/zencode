using ZenCode.Lexer;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Types;

namespace ZenCode.Parser.Tests.Integration;

public class ParserTests
{
    private readonly IParser _parser;

    public ParserTests()
    {
        var tokenizerFactory = new TokenizerFactory();

        var typeParserFactory = new TypeParserFactory();

        var expressionParserFactory = new ExpressionParserFactory(typeParserFactory);

        var statementParserFactory = new StatementParserFactory(expressionParserFactory, typeParserFactory);
        
        _parser = new ParserFactory(tokenizerFactory, statementParserFactory).Create();
    }
}