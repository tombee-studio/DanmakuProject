using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    private List<ExpASTNodeBase> ChangeArgsToList(PrimitiveValue[] args)
    {
        return new List<ExpASTNodeBase>(
            args.Select(e => new PrimaryExpASTNode(e))
        );
    }
    private List<ExpASTNodeBase> GetArgsList(params PrimitiveValue[] args)
    {
        return ChangeArgsToList(args);
    }
    private void TestFunctionByConstantArgument(int id, string functionName, params PrimitiveValue[] args)
    {
        int funcID = EnemyFunctionFactory.GetInstance().Find(functionName);
        var testCodesList = new List<string>(
            args.Select(e => $"PUSH {e}")
                .Append($"PUSH {id}")
                .Append($"CALL {funcID}")
        );
        var node = new CallFuncStASTNode(functionName, GetArgsList(args));
        node.id = id;
        /* テスト本体 */
        checkGeneratedInstructionIsSame(testCodesList.ToArray(), node);
        Assert.AreEqual(node.Print(0), $"{functionName}(" + String.Join(", ", args) + ")\n");
    }
    public void test_GenerateBulletsCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "generate_bullets", 24);
    }
    public void test_ActivateBulletsCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "activate_bullets");
    }
    public void test_DelayBulletsCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "delay_bullets", 120);
    }
    public void test_SetBulletsPositionAtEnemyCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "set_bullets_position_at_enemy");
    }
    public void test_MoveBulletsParallelCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "move_bullets_parallel", 0f, 0f);
    }
    public void test_SetBulletsPositionInCircularPatternCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "set_bullets_position_in_circular_pattern", 7.5f);
    }
    public void test_ScatterBulletsInCircularPatternCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "scatter_bullets_in_circular_pattern", 0.1f, 7.5f);
    }
}

