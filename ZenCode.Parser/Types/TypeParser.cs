using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Types
{
    public class TypeParser : ITypeParser
    {
        private readonly IBooleanTypeParsingStrategy _booleanTypeParsingStrategy;
        private readonly IFloatTypeParsingStrategy _floatTypeParsingStrategy;
        private readonly IIntegerTypeParsingStrategy _integerTypeParsingStrategy;
        private readonly IStringTypeParsingStrategy _stringTypeParsingStrategy;
        private readonly IVoidTypeParsingStrategy _voidTypeParsingStrategy;
    
        public TypeParser(
            IBooleanTypeParsingStrategy booleanTypeParsingStrategy,
            IFloatTypeParsingStrategy floatTypeParsingStrategy,
            IIntegerTypeParsingStrategy integerTypeParsingStrategy,
            IStringTypeParsingStrategy stringTypeParsingStrategy,
            IVoidTypeParsingStrategy voidTypeParsingStrategy)
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
}