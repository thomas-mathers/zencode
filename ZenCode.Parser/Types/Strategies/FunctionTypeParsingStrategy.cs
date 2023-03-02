using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types.Strategies;

public class FunctionTypeParsingStrategy : IPrefixTypeParsingStrategy
{
    private readonly IParser _parser;

    public FunctionTypeParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Type Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameters = new List<Type>();

        while (!tokenStream.Match(TokenType.RightParenthesis))
        {
            parameters.Add(_parser.ParseType(tokenStream));
        }

        tokenStream.Consume(TokenType.RightArrow);

        var returnType = _parser.ParseType(tokenStream);

        return new FunctionType(returnType, parameters);
    }
}