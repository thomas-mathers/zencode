using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types.Strategies;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types.Strategies;

public class PrimitivePrefixTypeParsingStrategy<T> : IPrefixTypeParsingStrategy where T : Type, new()
{
    private readonly TokenType _tokenType;

    public PrimitivePrefixTypeParsingStrategy(TokenType tokenType)
    {
        _tokenType = tokenType;
    }

    public Type Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(_tokenType);
        return new T();
    }
}