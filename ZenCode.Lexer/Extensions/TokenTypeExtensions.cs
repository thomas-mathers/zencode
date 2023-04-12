using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Extensions;

public static class TokenTypeExtensions
{
    private static readonly IReadOnlyDictionary<TokenType, string> Keywords = new Dictionary<TokenType, string>
    {
        [TokenType.Plus] = "+",
        [TokenType.And] = "and",
        [TokenType.Assignment] = ":=",
        [TokenType.Boolean] = "bool",
        [TokenType.Break] = "break",
        [TokenType.Colon] = ":",
        [TokenType.Comma] = ",",
        [TokenType.Continue] = "continue",
        [TokenType.Division] = "/",
        [TokenType.ElseIf] = "else if",
        [TokenType.Else] = "else",
        [TokenType.RightArrow] = "=>",
        [TokenType.Equals] = "=",
        [TokenType.Exponentiation] = "^",
        [TokenType.Float] = "float",
        [TokenType.For] = "for",
        [TokenType.Function] = "function",
        [TokenType.GreaterThanOrEqual] = ">=",
        [TokenType.GreaterThan] = ">",
        [TokenType.If] = "if",
        [TokenType.Integer] = "int",
        [TokenType.LeftBrace] = "{",
        [TokenType.LeftBracket] = "[",
        [TokenType.LeftParenthesis] = "(",
        [TokenType.LessThanOrEqual] = "<=",
        [TokenType.LessThan] = "<",
        [TokenType.Modulus] = "mod",
        [TokenType.Multiplication] = "*",
        [TokenType.New] = "new",
        [TokenType.Not] = "not",
        [TokenType.NotEquals] = "!=",
        [TokenType.Or] = "or",
        [TokenType.Print] = "print",
        [TokenType.Read] = "read",
        [TokenType.Return] = "return",
        [TokenType.RightBrace] = "}",
        [TokenType.RightBracket] = "]",
        [TokenType.RightParenthesis] = ")",
        [TokenType.Semicolon] = ";",
        [TokenType.String] = "string",
        [TokenType.Minus] = "-",
        [TokenType.Var] = "var",
        [TokenType.While] = "while",
    };
    
    public static string GetText(this TokenType type)
    {
        return Keywords.TryGetValue(type, out var text) ? text : type.ToString();
    }
}
