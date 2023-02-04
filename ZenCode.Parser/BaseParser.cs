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
    private IEnumerator<Token> _tokenEnumerator = Enumerable.Empty<Token>().GetEnumerator();
    private bool _isDone = false;

    protected BaseParser(ITokenizer tokenizer,
        IReadOnlyDictionary<TokenType, IPrefixExpressionParser> prefixExpressionParsers,
        IReadOnlyDictionary<TokenType, IInfixExpressionParser> infixExpressionParsers)
    {
        _tokenizer = tokenizer;
        _prefixExpressionParsers = prefixExpressionParsers;
        _infixExpressionParsers = infixExpressionParsers;
    }

    public Token? Consume()
    {
        var hasMore = _tokenEnumerator.MoveNext();

        if (!hasMore)
        {
            _isDone = true;
            return null;
        }

        return _tokenEnumerator.Current;
    }

    public Token Expect(TokenType tokenType)
    {
        var token = Consume();

        if (token?.Type != tokenType)
            throw new ParseException();

        return token;
    }

    public Program Parse(string input)
    {
        _tokenEnumerator = _tokenizer.Tokenize(input).GetEnumerator();

        var statements = new List<Statement>();

        while (!_isDone)
        {
            var statement = ParseStatement();
            statements.Add(statement);
        }

        return new Program(statements);
    }

    public Statement ParseStatement()
    {
        return ParseExpression();
    }

    public Expression ParseExpression()
    {
        var token = Consume();

        if (token == null)
            throw new ParseException();

        if (!_prefixExpressionParsers.TryGetValue(token.Type, out var prefixExpressionParser))
            throw new ParseException();

        var lOperand = prefixExpressionParser.Parse(this, token);

        var op = Consume();

        if (op == null)
            return lOperand;

        if (!_infixExpressionParsers.TryGetValue(op.Type, out var infixExpressionParser))
            throw new ParseException();

        return infixExpressionParser.Parse(this, lOperand, op);
    }
}