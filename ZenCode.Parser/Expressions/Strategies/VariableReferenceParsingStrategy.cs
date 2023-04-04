using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class VariableReferenceParsingStrategy : IVariableReferenceParsingStrategy
{
    public VariableReferenceExpression Parse(IParser parser, ITokenStream tokenStream)
    {
        var identifierToken = tokenStream.Consume(TokenType.Identifier);

        var indices = tokenStream.Match(TokenType.LeftBracket)
            ? parser.ParseArrayIndexExpressionList(tokenStream)
            : new ArrayIndexExpressionList();

        var variableReferenceExpression = new VariableReferenceExpression(identifierToken)
        {
            Indices = indices
        };

        return variableReferenceExpression;
    }
}