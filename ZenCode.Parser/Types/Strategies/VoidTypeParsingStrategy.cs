using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies;

public class VoidTypeParsingStrategy : IVoidTypeParsingStrategy
{
    public VoidType Parse(ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        tokenStream.Consume(TokenType.Void);

        return new VoidType();
    }
}
