using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser;

public class ParserFactory
{
    public IParser Create()
    {
        var expressionParser = new ExpressionParser();
        
        expressionParser.SetPrefixStrategy(TokenType.BooleanLiteral, 
            new ConstantParsingStrategy());
        expressionParser.SetPrefixStrategy(TokenType.IntegerLiteral, 
            new ConstantParsingStrategy());
        expressionParser.SetPrefixStrategy(TokenType.FloatLiteral, 
            new ConstantParsingStrategy());
        expressionParser.SetPrefixStrategy(TokenType.StringLiteral, 
            new ConstantParsingStrategy());
        expressionParser.SetPrefixStrategy(TokenType.Identifier, 
            new VariableReferenceParsingStrategy(expressionParser));
        expressionParser.SetPrefixStrategy(TokenType.Subtraction, 
            new UnaryExpressionParsingStrategy(expressionParser));
        expressionParser.SetPrefixStrategy(TokenType.Not, 
            new UnaryExpressionParsingStrategy(expressionParser));
        expressionParser.SetPrefixStrategy(TokenType.LeftParenthesis, 
            new ParenthesisParsingStrategy(expressionParser));
        
        expressionParser.SetInfixStrategy(TokenType.Addition, 
            new BinaryExpressionParsingStrategy(expressionParser, 4));
        expressionParser.SetInfixStrategy(TokenType.Subtraction, 
            new BinaryExpressionParsingStrategy(expressionParser, 4));
        expressionParser.SetInfixStrategy(TokenType.Multiplication, 
            new BinaryExpressionParsingStrategy(expressionParser, 5));
        expressionParser.SetInfixStrategy(TokenType.Division, 
            new BinaryExpressionParsingStrategy(expressionParser, 5));
        expressionParser.SetInfixStrategy(TokenType.Modulus, 
            new BinaryExpressionParsingStrategy(expressionParser, 5));
        expressionParser.SetInfixStrategy(TokenType.Exponentiation, 
            new BinaryExpressionParsingStrategy(expressionParser, 6, true));
        expressionParser.SetInfixStrategy(TokenType.LessThan, 
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixStrategy(TokenType.LessThanOrEqual, 
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixStrategy(TokenType.Equals, 
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixStrategy(TokenType.NotEquals, 
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixStrategy(TokenType.GreaterThan, 
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixStrategy(TokenType.GreaterThanOrEqual, 
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixStrategy(TokenType.And, 
            new BinaryExpressionParsingStrategy(expressionParser, 2));
        expressionParser.SetInfixStrategy(TokenType.Or, 
            new BinaryExpressionParsingStrategy(expressionParser, 1));
        expressionParser.SetInfixStrategy(TokenType.LeftParenthesis, 
            new FunctionCallParsingStrategy(expressionParser, 7));
        
        var statementParser = new StatementParser();
        
        statementParser.SetStrategy(TokenType.Identifier, 
            new AssignmentStatementParsingStrategy(expressionParser));
        statementParser.SetStrategy(TokenType.If, 
            new IfStatementParsingStrategy(expressionParser, statementParser));
        statementParser.SetStrategy(TokenType.Print, 
            new PrintStatementParsingStrategy(expressionParser));
        statementParser.SetStrategy(TokenType.Var, 
            new VariableDeclarationStatementParsingStrategy(expressionParser));
        statementParser.SetStrategy(TokenType.While, 
            new WhileStatementParsingStrategy(expressionParser, statementParser));

        var tokenizer = new Tokenizer();
        
        var parser = new Parser(tokenizer, statementParser);

        return parser;
    }
}