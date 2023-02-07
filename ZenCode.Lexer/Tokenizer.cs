using ZenCode.Lexer.Exceptions;

namespace ZenCode.Lexer;

public class Tokenizer : ITokenizer
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
    
    private string _text = string.Empty;
    private int _currentLine;
    private int _currentColumn;
    private int _currentIndex;

    public ITokenStream Tokenize(string text)
    {
        return new TokenStream(TokenizeHelper(text));
    }
    
    private IEnumerable<Token> TokenizeHelper(string text)
    {
        _text = text;
        _currentLine = 0;
        _currentColumn = 0;
        _currentIndex = 0;

        while (_currentIndex < _text.Length)
        {
            var currCharacter = _text[_currentIndex];
            var nextCharacter = _currentIndex + 1 < _text.Length ? _text[_currentIndex + 1] : '\0';

            switch (currCharacter)
            {
                case '\r' when nextCharacter == '\n':
                    _currentIndex += 2;
                    _currentLine++;
                    _currentColumn = 0;
                    break;
                case '\n':
                case '\r':
                    _currentIndex++;
                    _currentLine++;
                    _currentColumn = 0;
                    break;
                default:
                {
                    if (char.IsWhiteSpace(currCharacter))
                    {
                        _currentIndex++;
                        _currentColumn++;
                    }
                    else
                    {
                        yield return ConsumeToken();
                    }

                    break;
                }
            }
        }
    }

    private Token ConsumeToken()
    {
        foreach (var matcher in TokenMatchers)
        {
            var match = matcher.Match(_text, _currentIndex);

            if (match == null)
                continue;
            
            var token = new Token
            {
                Type = matcher.TokenType,
                Line = _currentLine,
                StartingColumn = _currentColumn,
                Text = match
            };

            _currentIndex += match.Length;
            _currentColumn += match.Length;

            return token;
        }

        throw new TokenParseException();
    }
}
