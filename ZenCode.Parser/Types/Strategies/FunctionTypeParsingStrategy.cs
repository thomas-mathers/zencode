using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies;

public class FunctionTypeParsingStrategy : IFunctionTypeParsingStrategy
{
    public FunctionType Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);

        var typeList = tokenStream.Match(TokenType.RightParenthesis)
            ? new TypeList()
            : parser.ParseTypeList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);
        tokenStream.Consume(TokenType.RightArrow);

        var returnType = parser.ParseType(tokenStream);

        return new FunctionType(returnType, typeList);
    }
}