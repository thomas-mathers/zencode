namespace ZenCode.Parser.Abstractions.Statements;

public interface IStatementParserFactory
{
    IStatementParser Create();
}