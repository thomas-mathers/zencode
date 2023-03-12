using System.Collections;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer;

public class TokenStream : ITokenStream
{
    private readonly LinkedList<Token> _peekedTokens = new();
    private readonly IEnumerator<Token> _tokenEnumerator;

    public TokenStream(IEnumerable<Token> tokenEnumerator)
    {
        _tokenEnumerator = tokenEnumerator.GetEnumerator();
    }

    public Token Current => Peek(0)!;

    public Token Consume(params TokenType[] tokenTypes)
    {
        var token = TryConsumeToken();

        if (token == null)
        {
            throw new UnexpectedTokenException();
        }

        if (!tokenTypes.Contains(token.Type))
        {
            throw new UnexpectedTokenException();
        }

        return token;
    }

    public Token Consume()
    {
        var token = TryConsumeToken();

        if (token == null)
        {
            throw new InvalidOperationException();
        }

        return _tokenEnumerator.Current;
    }

    public Token? Peek(byte numTokens)
    {
        if (numTokens < _peekedTokens.Count)
        {
            return GetPeekedToken(numTokens);
        }

        var numRemainingTokensToConsume = numTokens - _peekedTokens.Count;

        for (var i = 0; i <= numRemainingTokensToConsume; i++)
        {
            var token = _tokenEnumerator.MoveNext() ? _tokenEnumerator.Current : null;

            if (token == null)
            {
                return null;
            }

            _peekedTokens.AddLast(token);
        }

        return _peekedTokens.Last();
    }

    public bool Match(TokenType tokenType) => Peek(0)?.Type == tokenType;

    public IEnumerator<Token> GetEnumerator()
    {
        return _tokenEnumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private Token? TryConsumeToken()
    {
        if (_peekedTokens.Any())
        {
            return PopPeakedToken();
        }

        return !_tokenEnumerator.MoveNext() ? null : _tokenEnumerator.Current;
    }

    private Token? GetPeekedToken(int index)
    {
        var peekedTokenNode = _peekedTokens.First;

        while (peekedTokenNode != null && index > 0)
        {
            peekedTokenNode = peekedTokenNode.Next;
            index--;
        }

        return peekedTokenNode?.Value;
    }

    private Token PopPeakedToken()
    {
        var token = _peekedTokens.First();
        _peekedTokens.RemoveFirst();
        return token;
    }
}