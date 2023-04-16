using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer;

public class Tokenizer : ITokenizer
{
    private readonly IEnumerable<ITokenMatcher> _tokenMatchers;

    private int _currentColumn;
    private int _currentIndex;
    private int _currentLine;
    private string _text = string.Empty;

    public Tokenizer(IEnumerable<ITokenMatcher> tokenMatchers)
    {
        _tokenMatchers = tokenMatchers;
    }

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
        foreach (var matcher in _tokenMatchers)
        {
            var match = matcher.Match(_text, _currentIndex);

            if (match == null)
            {
                continue;
            }

            var token = new Token(matcher.TokenType)
            {
                Line = _currentLine,
                StartingColumn = _currentColumn,
                Text = match
            };

            _currentIndex += match.Length;
            _currentColumn += match.Length;

            return token;
        }

        throw new TokenParseException(_currentLine, _currentColumn);
    }
}
