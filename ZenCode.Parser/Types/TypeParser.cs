using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types.Strategies;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types;

public class TypeParser : ITypeParser
{
    private readonly BooleanTypeParsingStrategy _booleanTypeParsingStrategy;
    private readonly FloatTypeParsingStrategy _floatTypeParsingStrategy;
    private readonly IntegerTypeParsingStrategy _integerTypeParsingStrategy;
    private readonly StringTypeParsingStrategy _stringTypeParsingStrategy;
    private readonly VoidTypeParsingStrategy _voidTypeParsingStrategy;
    
    public TypeParser(
        BooleanTypeParsingStrategy booleanTypeParsingStrategy,
        FloatTypeParsingStrategy floatTypeParsingStrategy,
        IntegerTypeParsingStrategy integerTypeParsingStrategy,
        StringTypeParsingStrategy stringTypeParsingStrategy,
        VoidTypeParsingStrategy voidTypeParsingStrategy)
    {
        _booleanTypeParsingStrategy = booleanTypeParsingStrategy;
        _floatTypeParsingStrategy = floatTypeParsingStrategy;
        _integerTypeParsingStrategy = integerTypeParsingStrategy;
        _stringTypeParsingStrategy = stringTypeParsingStrategy;
        _voidTypeParsingStrategy = voidTypeParsingStrategy;
    }
    
    public Type ParseType(ITokenStream tokenStream, int precedence = 0)
    {
        var type = ParsePrefixType(tokenStream);

        while (tokenStream.Peek(0)?.Type == TokenType.LeftBracket && tokenStream.Peek(1)?.Type == TokenType.RightBracket)
        {
            tokenStream.Consume(TokenType.LeftBracket);
            tokenStream.Consume(TokenType.RightBracket);
            
            type = new ArrayType(type);
        }

        return type;
    }

    private BooleanType ParseBooleanType(ITokenStream tokenStream)
    {
        return _booleanTypeParsingStrategy.Parse(tokenStream);
    }

    private FloatType ParseFloatType(ITokenStream tokenStream)
    {
        return _floatTypeParsingStrategy.Parse(tokenStream);
    }

    private IntegerType ParseIntegerType(ITokenStream tokenStream)
    {
        return _integerTypeParsingStrategy.Parse(tokenStream);
    }

    private StringType ParseStringType(ITokenStream tokenStream)
    {
        return _stringTypeParsingStrategy.Parse(tokenStream);
    }

    private VoidType ParseVoidType(ITokenStream tokenStream)
    {
        return _voidTypeParsingStrategy.Parse(tokenStream);
    }

    private Type ParsePrefixType(ITokenStream tokenStream) =>
        tokenStream.Current.Type switch
        {
            TokenType.Boolean => ParseBooleanType(tokenStream),
            TokenType.Float => ParseFloatType(tokenStream),
            TokenType.Integer => ParseIntegerType(tokenStream),
            TokenType.String => ParseStringType(tokenStream),
            TokenType.Void => ParseVoidType(tokenStream),
            _ => throw new UnexpectedTokenException()
        };    
}