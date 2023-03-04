using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Statements;

public class StatementParserFactory
{
    private readonly IExpressionParser _expressionParser;
    private readonly ITypeParser _typeParser;

    public StatementParserFactory(IExpressionParser expressionParser, ITypeParser typeParser)
    {
        _expressionParser = expressionParser;
        _typeParser = typeParser;
    }

    public IStatementParser Create()
    {
        var statementParser = new StatementParser();

        statementParser.SetStatementParsingStrategy(TokenType.If,
            new IfStatementParsingStrategy(_expressionParser, statementParser));
        statementParser.SetStatementParsingStrategy(TokenType.While,
            new WhileStatementParsingStrategy(_expressionParser, statementParser));
        statementParser.SetStatementParsingStrategy(TokenType.Function,
            new FunctionDeclarationStatementParsingStrategy(_typeParser, statementParser));
        statementParser.SetStatementParsingStrategy(TokenType.Identifier,
            new AssignmentStatementParsingStrategy(_expressionParser));
        statementParser.SetStatementParsingStrategy(TokenType.Print,
            new PrintStatementParsingStrategy(_expressionParser));
        statementParser.SetStatementParsingStrategy(TokenType.Var,
            new VariableDeclarationStatementParsingStrategy(_expressionParser));
        statementParser.SetStatementParsingStrategy(TokenType.Return,
            new ReturnStatementParsingStrategy(_expressionParser));

        return statementParser;
    }
}