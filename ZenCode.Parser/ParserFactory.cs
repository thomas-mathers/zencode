using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser
{
    public class ParserFactory
    {
        public IParser Create()
        {
            var parser = new Parser(
                new ExpressionParser(
                    new PrefixExpressionParser(
                        new AnonymousFunctionDeclarationParsingStrategy(),
                        new LiteralParsingStrategy(),
                        new NewExpressionParsingStrategy(),
                        new ParenthesisParsingStrategy(),
                        new UnaryExpressionParsingStrategy(),
                        new VariableReferenceParsingStrategy()
                    ),
                    new InfixExpressionParser(
                        new BinaryExpressionParsingStrategy(), 
                        new FunctionCallParsingStrategy())
                ),
                new StatementParser(
                    new AssignmentStatementParsingStrategy(), 
                    new ForStatementParsingStrategy(), 
                    new FunctionDeclarationStatementParsingStrategy(), 
                    new IfStatementParsingStrategy(), 
                    new PrintStatementParsingStrategy(), 
                    new ReturnStatementParsingStrategy(), 
                    new VariableDeclarationStatementParsingStrategy(), 
                    new WhileStatementParsingStrategy()
                ),
                new TypeParser(
                    new BooleanTypeParsingStrategy(), 
                    new FloatTypeParsingStrategy(), 
                    new IntegerTypeParsingStrategy(), 
                    new StringTypeParsingStrategy(), 
                    new VoidTypeParsingStrategy()
                )
            );

            return parser;
        }
    }
}