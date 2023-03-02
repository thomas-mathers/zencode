using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types.Strategies;

public class ArrayTypeParsingStrategy : IInfixTypeParsingStrategy
{
    public ArrayTypeParsingStrategy(int precedence)
    {
        Precedence = precedence;
    }

    public int Precedence { get; }

    public Type Parse(ITokenStream tokenStream, Type type)
    {
        tokenStream.Consume(TokenType.LeftBracket);
        tokenStream.Consume(TokenType.RightBracket);

        return new ArrayType(type);
    }
}