#nullable enable

using System.Collections.Generic;
using UnityEngine;

public partial class EnemyParser
{
    public ParseResult<BreakStASTNodeBase> ParseBreakSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        stream.should.Expect("break");
        return new ParseResult<BreakStASTNodeBase>(
            new BreakStASTNode(),
            stream.CurrentPointer);
    }

    public ParseResult<StatementASTNodeBase> ParseStatement(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        var result = observer.matchConsume<StatementASTNodeBase>()
            .Try(ParseBreakSt)
            .Try(ParseBlockSt)
            .Try(ParseDeclarationSt)
            .Try(ParseIfSt)
            .Try(ParseAssignSt)
            .Try(ParseRepeatSt)
            .Try(ParseCallFuncStASTNode)
            .Try(ParseExpSt)
            .Result;
        if (result == null)
        {
            throw new ParseException("Undefined Exception");
        }
        else
        {
            return new(result, observer.CurrentPointer);
        }
    }

    public ParseResult<BlockStASTNodeBase> ParseBlockSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        stream.should
            .Expect("{")
            .ExpectMultiComsumer(ParseStatement, out var statements)
            .Expect("}");
        return new ParseResult<BlockStASTNodeBase>(
            new BlockStASTNode(statements),
            stream.CurrentPointer);
    }

    public ParseResult<DeclarationStASTNodeBase> ParseDeclarationSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (false) { }
        else if (stream.maybe.Expect("int").IsSatisfied)
        {
            stream.should.ExpectSymbolID(out var id);
            return new ParseResult<DeclarationStASTNodeBase>(
                new DeclarationStASTNode(PrimitiveValue.Type.INT, id),
                stream.CurrentPointer);
        }
        else if (stream.maybe.Expect("float").IsSatisfied)
        {
            stream.should.ExpectSymbolID(out var id);
            return new ParseResult<DeclarationStASTNodeBase>(
                new DeclarationStASTNode(PrimitiveValue.Type.FLOAT, id),
                stream.CurrentPointer);
        }
        else
        {
            return ParseResult<DeclarationStASTNodeBase>.Failed(
                "expected int, float",
                "DeclarationStASTNode",
                stream.CurrentPointer);
        }
    }

    public ParseResult<ExpStASTNodeBase> ParseExpSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        stream.should.ExpectConsumedBy(ParseExpASTNode, out var exp);
        return new ParseResult<ExpStASTNodeBase>(
            new ExpStASTNode(exp),
            stream.CurrentPointer);
    }

    public ParseResult<IfStASTNodeBase> ParseIfSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        //if (!
        stream.should
            .Expect("if")
            .Expect("(")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase cond)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase statement);
        if (!stream.maybe.Expect("else").IsSatisfied)
        {
            return new ParseResult<IfStASTNodeBase>(
                new IfStASTNode(cond, statement),
                stream.CurrentPointer);
        }
        stream.should.ExpectConsumedBy(ParseStatement, out StatementASTNodeBase elseStatement);
        return new ParseResult<IfStASTNodeBase>(
            new IfStASTNode(cond, statement, elseStatement),
            stream.CurrentPointer);
    }

    public ParseResult<AssignStASTNodeBase> ParseAssignSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        stream.should.ExpectSymbolID(out string id)
            .Expect("=")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase exp);
        return new(
            new AssignStASTNode(id, exp),
            stream.CurrentPointer);
    }

    public ParseResult<RepeatStASTNodeBase> ParseRepeatSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        stream.should
            .Expect("repeat")
            .Expect("(")
            .ExpectVariable(out ScriptToken token)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase statement);
        return new(
            new RepeatStASTNode(
                token.int_val,
                statement),
            stream.CurrentPointer);
    }
}
