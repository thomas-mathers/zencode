using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer;

public class TokenizerFactory : ITokenizerFactory
{
    public ITokenizer Create()
    {
        return new Tokenizer
        (
            new ITokenMatcher[]
            {
                new TokenMatcher(TokenType.Plus, "+"),
                new TokenMatcher(TokenType.And, "and"),
                new TokenMatcher(TokenType.Assignment, ":="),
                new TokenMatcher(TokenType.Boolean, "bool"),
                new TokenMatcher(TokenType.BooleanLiteral, "false"),
                new TokenMatcher(TokenType.BooleanLiteral, "true"),
                new TokenMatcher(TokenType.Break, "break"),
                new TokenMatcher(TokenType.Colon, ":"),
                new TokenMatcher(TokenType.Comma, ","),
                new TokenMatcher(TokenType.Continue, "continue"),
                new TokenMatcher(TokenType.Division, "/"),
                new TokenMatcher(TokenType.ElseIf, "else if"),
                new TokenMatcher(TokenType.Else, "else"),
                new TokenMatcher(TokenType.RightArrow, "=>"),
                new TokenMatcher(TokenType.Equals, "="),
                new TokenMatcher(TokenType.Exponentiation, "^"),
                new TokenMatcher(TokenType.Float, "float"),
                new TokenMatcher(TokenType.For, "for"),
                new TokenMatcher(TokenType.Function, "function"),
                new TokenMatcher(TokenType.GreaterThanOrEqual, ">="),
                new TokenMatcher(TokenType.GreaterThan, ">"),
                new TokenMatcher(TokenType.If, "if"),
                new TokenMatcher(TokenType.Integer, "int"),
                new TokenMatcher(TokenType.LeftBrace, "{"),
                new TokenMatcher(TokenType.LeftBracket, "["),
                new TokenMatcher(TokenType.LeftParenthesis, "("),
                new TokenMatcher(TokenType.LessThanOrEqual, "<="),
                new TokenMatcher(TokenType.LessThan, "<"),
                new TokenMatcher(TokenType.Modulus, "mod"),
                new TokenMatcher(TokenType.Multiplication, "*"),
                new TokenMatcher(TokenType.New, "new"),
                new TokenMatcher(TokenType.Not, "not"),
                new TokenMatcher(TokenType.NotEquals, "!="),
                new TokenMatcher(TokenType.Or, "or"),
                new TokenMatcher(TokenType.Print, "print"),
                new TokenMatcher(TokenType.Read, "read"),
                new TokenMatcher(TokenType.Return, "return"),
                new TokenMatcher(TokenType.RightBrace, "}"),
                new TokenMatcher(TokenType.RightBracket, "]"),
                new TokenMatcher(TokenType.RightParenthesis, ")"),
                new TokenMatcher(TokenType.Semicolon, ";"),
                new TokenMatcher(TokenType.String, "string"),
                new TokenMatcher(TokenType.Minus, "-"),
                new TokenMatcher(TokenType.Var, "var"),
                new TokenMatcher(TokenType.While, "while"),
                new RegexTokenMatcher(TokenType.FloatLiteral, "[0-9]*\\.[0-9]+([eE][-+]?[0-9]+)?"),
                new RegexTokenMatcher(TokenType.IntegerLiteral, "[0-9]+"),
                new RegexTokenMatcher(TokenType.Identifier, "[a-zA-Z][a-zA-Z0-9]*"),
                new RegexTokenMatcher(TokenType.StringLiteral, "'.*'")
            }
        );
    }
}
