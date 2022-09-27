#nullable enable

using System.Collections.Generic;

public partial class EnemyParser
{
    public ParseResult<BreakStASTNodeBase> ParseBreakSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (stream.maybe.Expect("break").IsSatisfied) {
            return ParseResult<BreakStASTNodeBase>.Failed(
                "expected break",
                "BreakStASTNodeBase",
                stream.CurrentPointer);
        }
        return new ParseResult<BreakStASTNodeBase>(
            new BreakStASTNode(),
            stream.CurrentPointer);
    }

    public ParseResult<StatementASTNodeBase> ParseStatement(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        var result = stream.match<StatementASTNodeBase>()
            .Try(ParseBlockSt)
            .Try(ParseCallFuncStASTNode)
            .Try(ParseRepeatSt)
            .Try(ParseBreakSt)
            .Try(ParseIfSt)
            .Result;
        if (result == null)
        {
            throw new ParseException("Undefined Exception");
        }
        else {
            return new (
                result,
                stream.CurrentPointer);
        }
    }

    public ParseResult<BlockStASTNodeBase> ParseBlockSt(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        if (!TestBlockStASTNodeBase(pointer)) {
            return ParseResult<BlockStASTNodeBase>.Failed(
                "expected {",
                "BlockStASTNodeBase",
                stream.CurrentPointer);
        }
        pointer.StartStream().should
            .Expect("{")
            .ExpectMultiComsumer(ParseStatement,
                out List<StatementASTNodeBase> statements)
            .Expect("}");
        return new ParseResult<BlockStASTNodeBase>(
            new BlockStASTNode(statements),
            stream.CurrentPointer);
    }

    public ParseResult<IfStASTNodeBase> ParseIfSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!TestBlockStASTNodeBase(pointer))
        {
            return ParseResult<IfStASTNodeBase>.Failed(
                "expected {",
                "BlockStASTNodeBase",
                stream.CurrentPointer);
        }
        pointer.StartStream().should
            .Expect("if")
            .Expect("(")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase cond)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase statement);
        if (TestElseStASTNodeBase(pointer))
        {
            pointer.StartStream().should
                .Expect("else")
                .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase elseStatement);
            return new ParseResult<IfStASTNodeBase>(
                new IfStASTNode(cond, statement, elseStatement),
                stream.CurrentPointer);
        }
        else {
            return new ParseResult<IfStASTNodeBase>(
                new IfStASTNode(cond, statement),
                stream.CurrentPointer);
        }
    }

    public ParseResult<AssignStASTNodeBase> ParseAssignSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!TestAssignStASTNodeBase(pointer))
        {
            return ParseResult<AssignStASTNodeBase>.Failed(
                "expected <ID>",
                "AssignStASTNodeBase",
                stream.CurrentPointer);
        }
        stream.should.ExpectSymbolID(out string id)
            .Expect("=")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase exp);
        return new (
            new AssignStASTNode(id, exp),
            stream.CurrentPointer);
    }

    public ParseResult<RepeatStASTNodeBase> ParseRepeatSt(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        if (!TestRepeatStASTNodeBase(pointer)) {
            return ParseResult<RepeatStASTNodeBase>.Failed(
                "expected repeat",
                "RepeatStASTNodeBase",
                stream.CurrentPointer);
        }
        pointer.StartStream().should
            .Expect("repeat")
            .Expect("(")
            .ExpectVariable(out ScriptToken token)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase statement);
        return new (
            new RepeatStASTNode(
                token.int_val,
                statement),
            stream.CurrentPointer);
    }

    public bool TestIfStASTNodeBase(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .Expect("if")
            .Expect("(")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase cond)
            .Expect(")")
            .ExpectMultiComsumer(ParseStatement, out List<StatementASTNodeBase> statements)
            .Expect("}")
            .IsSatisfied;

    }

    public bool TestElseStASTNodeBase(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .Expect("else")
            .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase statement)
            .IsSatisfied;
    }

    public bool TestBlockStASTNodeBase(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .Expect("{")
            .ExpectMultiComsumer(ParseStatement, out List<StatementASTNodeBase> statements)
            .Expect("}")
            .IsSatisfied;

    }

    public bool TestRepeatStASTNodeBase(TokenStreamPointer pointer) {
        return pointer.StartStream().maybe
            .Expect("repeat")
            .Expect("(")
            .ExpectVariable(out ScriptToken token)
            .Expect(")")
            .IsSatisfied;

    }

    public bool TestAssignStASTNodeBase(TokenStreamPointer pointer) {
        return pointer.StartStream().maybe.ExpectSymbolID(out string id)
            .Expect("=")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase exp)
            .IsSatisfied;
    }
}
