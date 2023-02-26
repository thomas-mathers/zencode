using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types;

public class TypeParser : ITypeParser
{
    public Type Parse(ITokenStream tokenStream)
    {
        var token = tokenStream.Consume(TokenType.Boolean, TokenType.Integer, TokenType.Float, TokenType.String);

        Type type = token.Type switch
        {
            TokenType.Boolean => new BooleanType(),
            TokenType.Integer => new IntegerType(),
            TokenType.Float => new FloatType(),
            _ => new StringType()
        };

        while (tokenStream.Match(TokenType.LeftBracket))
        {
            tokenStream.Consume(TokenType.RightBracket);
            type = new ArrayType(type);
        }
        
        return type;
    }
}