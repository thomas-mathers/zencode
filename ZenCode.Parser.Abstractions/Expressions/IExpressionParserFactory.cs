namespace ZenCode.Parser.Abstractions.Expressions;

public interface IExpressionParserFactory
{
    IExpressionParser Create();
}