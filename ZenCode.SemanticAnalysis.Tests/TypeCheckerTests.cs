using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;

namespace ZenCode.SemanticAnalysis.Tests;

public class TypeCheckerTests
{
    private readonly Mock<ISymbolTable> _symbolTableMock = new();
    private readonly TypeChecker _sut;

    public TypeCheckerTests()
    {
        _sut = new TypeChecker(_symbolTableMock.Object);
    }

    [Fact]
    public void DetermineType_AnonymousFunctionDeclarationExpression_ReturnsFunctionType()
    {
        // Arrange
        var anonymousFunctionDeclarationExpression = new AnonymousFunctionDeclarationExpression
        (
            new VoidType(),
            new ParameterList(),
            new Scope()
        );

        var expected = new FunctionType(new VoidType(), new TypeList());

        // Act
        var actual = _sut.DetermineType(anonymousFunctionDeclarationExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_AnonymousFunctionDeclarationExpressionWithOneParameter_ReturnsFunctionType()
    {
        // Arrange
        var anonymousFunctionDeclarationExpression = new AnonymousFunctionDeclarationExpression
        (
            new IntegerType(),
            new ParameterList
            (
                new Parameter(new Token(TokenType.Identifier, "x"), new IntegerType())
            ),
            new Scope()
        );

        var expected = new FunctionType(new IntegerType(), new TypeList(new IntegerType()));

        // Act
        var actual = _sut.DetermineType(anonymousFunctionDeclarationExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_AnonymousFunctionDeclarationExpressionWithThreeParameters_ReturnsFunctionType()
    {
        // Arrange
        var anonymousFunctionDeclarationExpression = new AnonymousFunctionDeclarationExpression
        (
            new FloatType(),
            new ParameterList
            (
                new Parameter(new Token(TokenType.Identifier, "x"), new FloatType()),
                new Parameter(new Token(TokenType.Identifier, "y"), new FloatType()),
                new Parameter(new Token(TokenType.Identifier, "y"), new FloatType())
            ),
            new Scope()
        );

        var expected = new FunctionType
        (
            new FloatType(),
            new TypeList
            (
                new FloatType(),
                new FloatType(),
                new FloatType()
            )
        );

        // Act
        var actual = _sut.DetermineType(anonymousFunctionDeclarationExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_BinaryExpressionDifferingTypes_ThrowsBinaryOperatorUnsupportedTypesException()
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            new Token(TokenType.Plus),
            new LiteralExpression(new Token(TokenType.StringLiteral))
        );

        // Act
        var exception = Assert.Throws<BinaryOperatorUnsupportedTypesException>
            (() => _sut.DetermineType(binaryExpression));

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData(TokenType.And)]
    [InlineData(TokenType.Or)]
    public void DetermineType_BooleanOpBoolean_ReturnsBooleanType(TokenType op)
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(TokenType.BooleanLiteral)),
            new Token(op),
            new LiteralExpression(new Token(TokenType.BooleanLiteral))
        );

        // Act
        var type = _sut.DetermineType(binaryExpression);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<BooleanType>(type);
    }

    [Theory]
    [InlineData(TokenType.Plus)]
    [InlineData(TokenType.Minus)]
    [InlineData(TokenType.Multiplication)]
    [InlineData(TokenType.Division)]
    [InlineData(TokenType.Modulus)]
    [InlineData(TokenType.Exponentiation)]
    public void DetermineType_IntegerOpInteger_ReturnsIntegerType(TokenType op)
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            new Token(op),
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );

        // Act
        var type = _sut.DetermineType(binaryExpression);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<IntegerType>(type);
    }

    [Theory]
    [InlineData(TokenType.Plus)]
    [InlineData(TokenType.Minus)]
    [InlineData(TokenType.Multiplication)]
    [InlineData(TokenType.Division)]
    [InlineData(TokenType.Modulus)]
    [InlineData(TokenType.Exponentiation)]
    public void DetermineType_FloatOpFloat_ReturnsFloatType(TokenType op)
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(TokenType.FloatLiteral)),
            new Token(op),
            new LiteralExpression(new Token(TokenType.FloatLiteral))
        );

        // Act
        var type = _sut.DetermineType(binaryExpression);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<FloatType>(type);
    }

    [Theory]
    [InlineData(TokenType.Plus)]
    public void DetermineType_StringOpString_ReturnsStringType(TokenType op)
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(TokenType.StringLiteral)),
            new Token(op),
            new LiteralExpression(new Token(TokenType.StringLiteral))
        );

        // Act
        var type = _sut.DetermineType(binaryExpression);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<StringType>(type);
    }

    [Theory]
    [InlineData(TokenType.IntegerLiteral, TokenType.LessThan)]
    [InlineData(TokenType.IntegerLiteral, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.IntegerLiteral, TokenType.GreaterThan)]
    [InlineData(TokenType.IntegerLiteral, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.FloatLiteral, TokenType.LessThan)]
    [InlineData(TokenType.FloatLiteral, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.FloatLiteral, TokenType.GreaterThan)]
    [InlineData(TokenType.FloatLiteral, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.StringLiteral, TokenType.LessThan)]
    [InlineData(TokenType.StringLiteral, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.StringLiteral, TokenType.GreaterThan)]
    [InlineData(TokenType.StringLiteral, TokenType.GreaterThanOrEqual)]
    public void DetermineType_ComparisonExpression_ReturnsBooleanType(TokenType literalType, TokenType op)
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(literalType)),
            new Token(op),
            new LiteralExpression(new Token(literalType))
        );

        // Act
        var type = _sut.DetermineType(binaryExpression);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<BooleanType>(type);
    }

    [Theory]
    [InlineData(TokenType.BooleanLiteral)]
    [InlineData(TokenType.IntegerLiteral)]
    [InlineData(TokenType.FloatLiteral)]
    [InlineData(TokenType.StringLiteral)]
    public void DetermineType_Equals_ReturnsBooleanType(TokenType literalType)
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(literalType)),
            new Token(TokenType.Equals),
            new LiteralExpression(new Token(literalType))
        );

        // Act
        var type = _sut.DetermineType(binaryExpression);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<BooleanType>(type);
    }

    [Theory]
    [InlineData(TokenType.BooleanLiteral)]
    [InlineData(TokenType.IntegerLiteral)]
    [InlineData(TokenType.FloatLiteral)]
    [InlineData(TokenType.StringLiteral)]
    public void DetermineType_NotEquals_ReturnsBooleanType(TokenType literalType)
    {
        // Arrange
        var binaryExpression = new BinaryExpression
        (
            new LiteralExpression(new Token(literalType)),
            new Token(TokenType.NotEquals),
            new LiteralExpression(new Token(literalType))
        );

        // Act
        var type = _sut.DetermineType(binaryExpression);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<BooleanType>(type);
    }

    [Fact]
    public void DetermineType_BooleanLiteral_ReturnsCorrectType()
    {
        // Arrange
        var expected = new BooleanType();

        // Act
        var actual = _sut.DetermineType(new LiteralExpression(new Token(TokenType.BooleanLiteral)));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_IntegerLiteral_ReturnsCorrectType()
    {
        // Arrange
        var expected = new IntegerType();

        // Act
        var actual = _sut.DetermineType(new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_FloatLiteral_ReturnsCorrectType()
    {
        // Arrange
        var expected = new FloatType();

        // Act
        var actual = _sut.DetermineType(new LiteralExpression(new Token(TokenType.FloatLiteral)));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_StringLiteral_ReturnsCorrectType()
    {
        // Arrange
        var expected = new StringType();

        // Act
        var actual = _sut.DetermineType(new LiteralExpression(new Token(TokenType.StringLiteral)));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_FunctionCallWithNoParameters_ReturnsFunctionReturnType()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns
            (
                new Symbol
                (
                    new Token(TokenType.Identifier, "foo"),
                    new FunctionType(new IntegerType(), new TypeList())
                )
            );

        var functionCall = new FunctionCallExpression
            (new VariableReferenceExpression(new Token(TokenType.Identifier, "foo")))
            {
                Arguments = new ExpressionList()
            };

        // Act
        var type = _sut.DetermineType(functionCall);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<IntegerType>(type);
    }

    [Fact]
    public void DetermineType_FunctionCallWithParameters_ReturnsIntegerType()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns
            (
                new Symbol
                (
                    new Token(TokenType.Identifier, "foo"),
                    new FunctionType
                    (
                        new BooleanType(),
                        new TypeList
                        (
                            new IntegerType(),
                            new FloatType(),
                            new StringType()
                        )
                    )
                )
            );

        var functionCall = new FunctionCallExpression
            (new VariableReferenceExpression(new Token(TokenType.Identifier, "foo")))
            {
                Arguments = new ExpressionList
                (
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    new LiteralExpression(new Token(TokenType.FloatLiteral)),
                    new LiteralExpression(new Token(TokenType.StringLiteral))
                )
            };

        // Act
        var type = _sut.DetermineType(functionCall);

        // Assert
        Assert.NotNull(type);
        Assert.IsType<BooleanType>(type);
    }

    [Fact]
    public void DetermineType_InvokingNonFunction_ThrowsInvokingNonFunctionTypeException()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns
            (
                new Symbol(new Token(TokenType.Identifier, "foo"), new IntegerType())
            );

        var functionCall = new FunctionCallExpression
            (new VariableReferenceExpression(new Token(TokenType.Identifier, "foo")))
            {
                Arguments = new ExpressionList()
            };

        // Act
        var exception = Assert.Throws<InvokingNonFunctionTypeException>(() => _sut.DetermineType(functionCall));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void DetermineType_NewBooleanArrayExpression_ReturnsArrayType()
    {
        // Arrange
        var newArrayExpression = new NewArrayExpression
            (new BooleanType(), new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        var expected = new ArrayType(new BooleanType());

        // Act
        var actual = _sut.DetermineType(newArrayExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_NewIntegerArrayExpression_ReturnsArrayType()
    {
        // Arrange
        var newArrayExpression = new NewArrayExpression
            (new IntegerType(), new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        var expected = new ArrayType(new IntegerType());

        // Act
        var actual = _sut.DetermineType(newArrayExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_NewFloatArrayExpression_ReturnsArrayType()
    {
        // Arrange
        var newArrayExpression = new NewArrayExpression
            (new FloatType(), new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        var expected = new ArrayType(new FloatType());

        // Act
        var actual = _sut.DetermineType(newArrayExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_NewStringArrayExpression_ReturnsArrayType()
    {
        // Arrange
        var newArrayExpression = new NewArrayExpression
            (new StringType(), new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        var expected = new ArrayType(new StringType());

        // Act
        var actual = _sut.DetermineType(newArrayExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_NewArrayOfIntegerArray_ReturnsArrayType()
    {
        // Arrange
        var newArrayExpression = new NewArrayExpression
        (
            new ArrayType(new IntegerType()),
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );

        var expected = new ArrayType(new ArrayType(new IntegerType()));

        // Act
        var actual = _sut.DetermineType(newArrayExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_UnaryExpressionWithBooleanOperand_ReturnsBooleanType()
    {
        // Arrange
        var expected = new BooleanType();

        // Act
        var actual = _sut.DetermineType
            (new UnaryExpression(new Token(TokenType.Not), new LiteralExpression(new Token(TokenType.BooleanLiteral))));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_UnaryExpressionWithIntegerOperand_ReturnsIntegerType()
    {
        // Arrange
        var expected = new IntegerType();

        // Act
        var actual = _sut.DetermineType
        (
            new UnaryExpression(new Token(TokenType.Minus), new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        );

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_UnaryExpressionWithFloatOperand_ReturnsFloatType()
    {
        // Arrange
        var expected = new FloatType();

        // Act
        var actual = _sut.DetermineType
        (
            new UnaryExpression(new Token(TokenType.Minus), new LiteralExpression(new Token(TokenType.FloatLiteral)))
        );

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_BooleanVariableReference_ReturnsBooleanType()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns(new Symbol(new Token(TokenType.Identifier, "foo"), new BooleanType()));

        var expected = new BooleanType();

        // Act
        var actual = _sut.DetermineType(new VariableReferenceExpression(new Token(TokenType.Identifier, "foo")));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_IntegerVariableReference_ReturnsIntegerType()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns(new Symbol(new Token(TokenType.Identifier, "foo"), new IntegerType()));

        var expected = new IntegerType();

        // Act
        var actual = _sut.DetermineType(new VariableReferenceExpression(new Token(TokenType.Identifier, "foo")));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_FloatVariableReference_ReturnsFloatType()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns(new Symbol(new Token(TokenType.Identifier, "foo"), new FloatType()));

        var expected = new FloatType();

        // Act
        var actual = _sut.DetermineType(new VariableReferenceExpression(new Token(TokenType.Identifier, "foo")));

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DetermineType_StringVariableReference_ReturnsStringType()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns(new Symbol(new Token(TokenType.Identifier, "foo"), new StringType()));

        var expected = new StringType();

        // Act
        var actual = _sut.DetermineType(new VariableReferenceExpression(new Token(TokenType.Identifier, "foo")));

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void DetermineType_UndeclaredVariableReference_ThrowsUndeclaredVariableException()
    {
        // Arrange
        _symbolTableMock
            .Setup(x => x.ResolveSymbol("foo"))
            .Returns<Symbol?>(null);

        var variableReference = new VariableReferenceExpression(new Token(TokenType.Identifier, "foo"));

        // Act
        var exception = Assert.Throws<UndeclaredIdentifierException>(() => _sut.DetermineType(variableReference));

        // Assert
        Assert.NotNull(exception);
    }
}
