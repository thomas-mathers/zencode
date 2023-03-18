using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser;

public class ParserFactory
{
    public IParser Create()
    {
        var prefixExpressionParser = new PrefixExpressionParser(
            new AnonymousFunctionDeclarationParsingStrategy(),
            new LiteralParsingStrategy(),
            new NewExpressionParsingStrategy(),
            new ParenthesisParsingStrategy(),
            new UnaryExpressionParsingStrategy(),
            new VariableReferenceParsingStrategy()
        );
        
        var infixExpressionParser = new InfixExpressionParser(
            new BinaryExpressionParsingStrategy(), new FunctionCallParsingStrategy());
        
        var expressionParser = new ExpressionParser(prefixExpressionParser, infixExpressionParser);
        
        var statementParser = new StatementParser(
            new AssignmentStatementParsingStrategy(), 
            new BreakStatementParsingStrategy(),
            new ContinueStatementParsingStrategy(),
            new ForStatementParsingStrategy(), 
            new FunctionDeclarationStatementParsingStrategy(), 
            new IfStatementParsingStrategy(), 
            new PrintStatementParsingStrategy(), 
            new ReadStatementParsingStrategy(),
            new ReturnStatementParsingStrategy(), 
            new VariableDeclarationStatementParsingStrategy(), 
            new WhileStatementParsingStrategy()
        );
        
        var typeParser = new TypeParser(
            new BooleanTypeParsingStrategy(), 
            new FloatTypeParsingStrategy(), 
            new IntegerTypeParsingStrategy(), 
            new StringTypeParsingStrategy(), 
            new VoidTypeParsingStrategy(),
            new ArrayTypeParsingStrategy(),
            new FunctionTypeParsingStrategy()
        );
        
        var parser = new Parser(
            new ExpressionListParser(),
            expressionParser,
            new ParameterListParser(),
            new ScopeParser(),
            statementParser,
            typeParser,
            new TypeListParser()
        );

        return parser;
    }
}