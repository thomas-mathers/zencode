using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types.Strategies;

public class ArrayInfixTypeParsingStrategy : IInfixTypeParsingStrategy
{
    public int Precedence { get; }

    public ArrayInfixTypeParsingStrategy(int precedence)
    {
        Precedence = precedence;
    }
    
    public Type Parse(ITokenStream tokenStream, Type type)
    {
        tokenStream.Consume(TokenType.LeftBracket);
        tokenStream.Consume(TokenType.RightBracket);

        return new ArrayType(type);
    }
}