using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser;

public class ParameterListParser : IParameterListParser
{
    public ParameterList ParseParameterList(IParser parser, ITokenStream tokenStream)
    {
        var parameters = new List<Parameter>();

        while (true)
        {
            var identifier = tokenStream.Consume(TokenType.Identifier);

            tokenStream.Consume(TokenType.Colon);

            var type = parser.ParseType(tokenStream);

            parameters.Add(new Parameter(identifier, type));

            if (!tokenStream.Match(TokenType.Comma)) break;

            tokenStream.Consume(TokenType.Comma);
        }

        return new ParameterList { Parameters = parameters };
    }
}