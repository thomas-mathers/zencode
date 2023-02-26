using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Model;

namespace ZenCode.Parser.Expressions.Helpers;

public class ParameterListParser : IParameterListParser
{
    private readonly ITypeParser _typeParser;

    public ParameterListParser(ITypeParser typeParser)
    {
        _typeParser = typeParser;
    }
    
    public IReadOnlyList<Parameter> Parse(ITokenStream tokenStream)
    {
        var parameters = new List<Parameter>();
        
        do
        {
            var identifier = tokenStream.Consume(TokenType.Identifier);
            
            tokenStream.Consume(TokenType.Colon);

            var type = _typeParser.Parse(tokenStream);
            
            parameters.Add(new Parameter(identifier, type));
        } while (tokenStream.Match(TokenType.Comma));

        return parameters;
    }
}