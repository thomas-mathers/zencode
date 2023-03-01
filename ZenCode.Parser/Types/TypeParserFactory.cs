using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Types;

public class TypeParserFactory
{
    public ITypeParser Create()
    {
        var typeParser = new TypeParser();

        typeParser.SetPrefixParsingStrategy(TokenType.Void,
            new VoidTypeParsingStrategy());
        typeParser.SetPrefixParsingStrategy(TokenType.Boolean,
            new BooleanTypeParsingStrategy());
        typeParser.SetPrefixParsingStrategy(TokenType.Integer,
            new IntegerTypeParsingStrategy());
        typeParser.SetPrefixParsingStrategy(TokenType.Float,
            new FloatTypeParsingStrategy());
        typeParser.SetPrefixParsingStrategy(TokenType.String,
            new StringTypeParsingStrategy());
        typeParser.SetPrefixParsingStrategy(TokenType.LeftParenthesis,
            new FunctionTypeParsingStrategy(typeParser));

        typeParser.SetInfixParsingStrategy(TokenType.LeftBracket, new ArrayTypeParsingStrategy(1));

        return typeParser;
    }
}