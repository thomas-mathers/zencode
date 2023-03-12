using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser;

public class Parser : IParser
{
    private readonly IExpressionListParser _expressionListParser;
    private readonly IExpressionParser _expressionParser;
    private readonly IParameterListParser _parameterListParser;
    private readonly IScopeParser _scopeParser;
    private readonly IStatementParser _statementParser;
    private readonly ITypeParser _typeParser;

    public Parser(IExpressionListParser expressionListParser, IExpressionParser expressionParser,
        IParameterListParser parameterListParser, IScopeParser scopeParser, IStatementParser statementParser,
        ITypeParser typeParser)
    {
        _expressionListParser = expressionListParser;
        _expressionParser = expressionParser;
        _parameterListParser = parameterListParser;
        _scopeParser = scopeParser;
        _statementParser = statementParser;
        _typeParser = typeParser;
    }

    public Program ParseProgram(ITokenStream tokenStream)
    {
        var statements = new List<Statement>();

        while (tokenStream.Peek(0) != null)
        {
            statements.Add(ParseStatement(tokenStream));
        }

        return new Program(statements);
    }

    public Expression ParseExpression(ITokenStream tokenStream, int precedence = 0)
    {
        return _expressionParser.ParseExpression(this, tokenStream, precedence);
    }

    public AssignmentStatement ParseAssignmentStatement(ITokenStream tokenStream)
    {
        return _statementParser.ParseAssignmentStatement(this, tokenStream);
    }

    public VariableDeclarationStatement ParseVariableDeclarationStatement(ITokenStream tokenStream)
    {
        return _statementParser.ParseVariableDeclarationStatement(this, tokenStream);
    }

    public Statement ParseStatement(ITokenStream tokenStream)
    {
        return _statementParser.ParseStatement(this, tokenStream);
    }

    public Type ParseType(ITokenStream tokenStream, int precedence = 0)
    {
        return _typeParser.ParseType(tokenStream);
    }

    public ExpressionList ParseExpressionList(ITokenStream tokenStream)
    {
        return _expressionListParser.ParseExpressionList(this, tokenStream);
    }

    public Scope ParseScope(ITokenStream tokenStream)
    {
        return _scopeParser.ParseScope(this, tokenStream);
    }

    public ConditionScope ParseConditionScope(ITokenStream tokenStream)
    {
        var condition = ParseExpression(tokenStream);
        var scope = ParseScope(tokenStream);

        return new ConditionScope(condition, scope);
    }

    public ParameterList ParseParameterList(ITokenStream tokenStream)
    {
        return _parameterListParser.ParseParameterList(this, tokenStream);
    }
}