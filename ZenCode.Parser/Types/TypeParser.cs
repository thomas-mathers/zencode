using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Types;

public class TypeParser : BaseTypeParser
{
    public TypeParser()
    {
        PrefixStrategies = new Dictionary<TokenType, IPrefixTypeParsingStrategy>
        {
            [TokenType.Void] = new PrimitivePrefixTypeParsingStrategy<VoidType>(TokenType.Void),
            [TokenType.Boolean] = new PrimitivePrefixTypeParsingStrategy<BooleanType>(TokenType.Boolean),
            [TokenType.Integer] = new PrimitivePrefixTypeParsingStrategy<IntegerType>(TokenType.Integer),
            [TokenType.Float] = new PrimitivePrefixTypeParsingStrategy<FloatType>(TokenType.Float),
            [TokenType.String] = new PrimitivePrefixTypeParsingStrategy<StringType>(TokenType.String),
            [TokenType.LeftParenthesis] = new FunctionPrefixTypeParsingStrategy(this)
        };

        InfixStrategies = new Dictionary<TokenType, IInfixTypeParsingStrategy>
        {
            [TokenType.LeftBracket] = new ArrayInfixTypeParsingStrategy(1)
        };
    }
}