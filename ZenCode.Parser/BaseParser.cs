using ZenCode.Lexer;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Grammar;
using ZenCode.Parser.Grammar.Expressions;
using ZenCode.Parser.Grammar.Statements;
using ZenCode.Parser.Parselets.Expressions.Infix;
using ZenCode.Parser.Parselets.Expressions.Prefix;

namespace ZenCode.Parser;

public abstract class BaseParser : IParser
{
    private readonly ITokenizer _tokenizer;
    private readonly IReadOnlyDictionary<TokenType, IPrefixExpressionParser> _prefixExpressionParsers;
    private readonly IReadOnlyDictionary<TokenType, IInfixExpressionParser> _infixExpressionParsers;

    public ITokenStream TokenStream { get; private set; } = new TokenStream(Enumerable.Empty<Token>());
    
    protected BaseParser(ITokenizer tokenizer,
        IReadOnlyDictionary<TokenType, IPrefixExpressionParser> prefixExpressionParsers,
        IReadOnlyDictionary<TokenType, IInfixExpressionParser> infixExpressionParsers)
    {
        _tokenizer = tokenizer;
        _prefixExpressionParsers = prefixExpressionParsers;
        _infixExpressionParsers = infixExpressionParsers;
    }

    public Program Parse(string input)
    {
        TokenStream = _tokenizer.Tokenize(input);

        var statements = new List<Statement>();

        while (TokenStream.Peek(0) != null)
        {
            statements.Add(ParseStatement());   
        }

        return new Program(statements);
    }

    public Expression ParseExpression(int precedence = 0)
    {
        var token = TokenStream.Consume();

        if (!_prefixExpressionParsers.TryGetValue(token.Type, out var prefixExpressionParser))
        {
            throw new ParseException();   
        }

        var lExpression = prefixExpressionParser.Parse(this, token);

        while (precedence < GetPrecedence())
        {
            var op = TokenStream.Consume();

            if (!_infixExpressionParsers.TryGetValue(op.Type, out var infixExpressionParser))
            {
                throw new ParseException();
            }

            lExpression = infixExpressionParser.Parse(this, lExpression, op);
        }
        
        return lExpression;
    }
    
    private Statement ParseStatement()
    {
        return ParseExpression();
    }

    private int GetPrecedence()
    {
        var currentToken = TokenStream?.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !_infixExpressionParsers.TryGetValue(currentToken.Type, out var parselet) 
            ? 0 
            : parselet.GetPrecedence();
    }
}