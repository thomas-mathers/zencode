using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Types;

public class TypeParserFactory : ITypeParserFactory
{
    public ITypeParser Create()
    {
        var typeParser = new TypeParser();

        typeParser.SetPrefixTypeParsingStrategy(TokenType.Void, new VoidTypeParsingStrategy());
        typeParser.SetPrefixTypeParsingStrategy(TokenType.Boolean, new BooleanTypeParsingStrategy());
        typeParser.SetPrefixTypeParsingStrategy(TokenType.Integer, new IntegerTypeParsingStrategy());
        typeParser.SetPrefixTypeParsingStrategy(TokenType.Float, new FloatTypeParsingStrategy());
        typeParser.SetPrefixTypeParsingStrategy(TokenType.String, new StringTypeParsingStrategy());

        return typeParser;
    }
}