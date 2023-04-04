using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Types;

public class TypeParser : ITypeParser
{
    private readonly IArrayTypeParsingStrategy _arrayTypeParsingStrategy;
    private readonly IBooleanTypeParsingStrategy _booleanTypeParsingStrategy;
    private readonly IFloatTypeParsingStrategy _floatTypeParsingStrategy;
    private readonly IFunctionTypeParsingStrategy _functionTypeParsingStrategy;
    private readonly IIntegerTypeParsingStrategy _integerTypeParsingStrategy;
    private readonly IStringTypeParsingStrategy _stringTypeParsingStrategy;
    private readonly IVoidTypeParsingStrategy _voidTypeParsingStrategy;

    public TypeParser(IBooleanTypeParsingStrategy booleanTypeParsingStrategy,
        IFloatTypeParsingStrategy floatTypeParsingStrategy, IIntegerTypeParsingStrategy integerTypeParsingStrategy,
        IStringTypeParsingStrategy stringTypeParsingStrategy, IVoidTypeParsingStrategy voidTypeParsingStrategy,
        IArrayTypeParsingStrategy arrayTypeParsingStrategy, IFunctionTypeParsingStrategy functionTypeParsingStrategy)
    {
        _booleanTypeParsingStrategy = booleanTypeParsingStrategy;
        _floatTypeParsingStrategy = floatTypeParsingStrategy;
        _integerTypeParsingStrategy = integerTypeParsingStrategy;
        _stringTypeParsingStrategy = stringTypeParsingStrategy;
        _voidTypeParsingStrategy = voidTypeParsingStrategy;
        _arrayTypeParsingStrategy = arrayTypeParsingStrategy;
        _functionTypeParsingStrategy = functionTypeParsingStrategy;
    }

    public Type ParseType(IParser parser, ITokenStream tokenStream)
    {
        var type = ParsePrefixType(parser, tokenStream);

        while (tokenStream.Peek(0)?.Type == TokenType.LeftBracket &&
               tokenStream.Peek(1)?.Type == TokenType.RightBracket)
        {
            type = _arrayTypeParsingStrategy.Parse(tokenStream, type);
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

    private FunctionType ParseFunctionType(IParser parser, ITokenStream tokenStream)
    {
        return _functionTypeParsingStrategy.Parse(parser, tokenStream);
    }

    private Type ParsePrefixType(IParser parser, ITokenStream tokenStream)
    {
        return tokenStream.Current.Type switch
        {
            TokenType.Boolean => ParseBooleanType(tokenStream),
            TokenType.Float => ParseFloatType(tokenStream),
            TokenType.Integer => ParseIntegerType(tokenStream),
            TokenType.String => ParseStringType(tokenStream),
            TokenType.Void => ParseVoidType(tokenStream),
            TokenType.LeftParenthesis => ParseFunctionType(parser, tokenStream),
            _ => throw new UnexpectedTokenException(tokenStream.Current.Type)
        };
    }
}