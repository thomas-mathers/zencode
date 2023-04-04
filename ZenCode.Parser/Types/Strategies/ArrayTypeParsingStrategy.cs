using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Types.Strategies;

public class ArrayTypeParsingStrategy : IArrayTypeParsingStrategy
{
    public ArrayType Parse(ITokenStream tokenStream, Type baseType)
    {
        tokenStream.Consume(TokenType.LeftBracket);
        tokenStream.Consume(TokenType.RightBracket);

        return new ArrayType(baseType);
    }
}