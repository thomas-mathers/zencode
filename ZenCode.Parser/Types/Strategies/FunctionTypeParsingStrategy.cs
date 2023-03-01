using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types.Strategies;

public class FunctionTypeParsingStrategy : IPrefixTypeParsingStrategy
{
    private readonly ITypeParser _typeParser;

    public FunctionTypeParsingStrategy(ITypeParser typeParser)
    {
        _typeParser = typeParser;
    }
    
    public Type Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameters = new List<Type>();
        
        while (!tokenStream.Match(TokenType.RightParenthesis))
        {
            parameters.Add(_typeParser.Parse(tokenStream));
        }

        tokenStream.Consume(TokenType.RightArrow);

        var returnType = _typeParser.Parse(tokenStream);

        return new FunctionType(returnType, parameters);
    }
}