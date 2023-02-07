namespace ZenCode.Lexer;

public class Tokenizer : BaseTokenizer
{
    private static readonly IEnumerable<ITokenMatcher> TokenMatchers = new ITokenMatcher[]
    {
        new TokenMatcher(TokenType.Assignment, ":="),
        new TokenMatcher(TokenType.Multiplication, "*"),
        new TokenMatcher(TokenType.Division, "/"),
        new TokenMatcher(TokenType.Modulus, "mod"),
        new TokenMatcher(TokenType.Exponentiation, "^"),
        new TokenMatcher(TokenType.LessThanOrEqual, "<="),
        new TokenMatcher(TokenType.LessThan, "<"),
        new TokenMatcher(TokenType.Equals, "="),
        new TokenMatcher(TokenType.NotEquals, "!="),
        new TokenMatcher(TokenType.GreaterThanOrEqual, ">="),
        new TokenMatcher(TokenType.GreaterThan, ">"),
        new TokenMatcher(TokenType.And, "and"),
        new TokenMatcher(TokenType.Or, "or"),
        new TokenMatcher(TokenType.Not, "not"),
        new TokenMatcher(TokenType.Boolean, "true"),
        new TokenMatcher(TokenType.Boolean, "false"),
        new TokenMatcher(TokenType.LeftParenthesis, "("),
        new TokenMatcher(TokenType.RightParenthesis, ")"),
        new RegexTokenMatcher(TokenType.Float, "[-+]?[0-9]*\\.[0-9]+([eE][-+]?[0-9]+)?"),
        new RegexTokenMatcher(TokenType.Integer, "[-+]?[0-9]+"),
        new TokenMatcher(TokenType.Addition, "+"),
        new TokenMatcher(TokenType.Subtraction, "-"),
        new RegexTokenMatcher(TokenType.Identifier, "[a-zA-Z][a-zA-Z0-9]*"),
        new TokenMatcher(TokenType.Comma, ","),
        new TokenMatcher(TokenType.LeftBracket, "["),
        new TokenMatcher(TokenType.RightBracket, "]"),
    };

    public Tokenizer() : base(TokenMatchers) 
    { 
    }
}
