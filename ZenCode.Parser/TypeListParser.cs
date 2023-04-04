using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser;

public class TypeListParser : ITypeListParser
{
    public TypeList ParseTypeList(IParser parser, ITokenStream tokenStream)
    {
        var types = new List<Type>();

        while (true)
        {
            types.Add(parser.ParseType(tokenStream));

            if (!tokenStream.Match(TokenType.Comma)) break;

            tokenStream.Consume(TokenType.Comma);
        }

        return new TypeList { Types = types };
    }
}