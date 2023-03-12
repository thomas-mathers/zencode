using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies
{
    public class IntegerTypeParsingStrategy : IIntegerTypeParsingStrategy
    {
        public IntegerType Parse(ITokenStream tokenStream)
        {
            tokenStream.Consume(TokenType.Integer);
            return new IntegerType();
        }
    }
}