using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    private List<ExpASTNode> ChangeArgsToList(PrimitiveValue[] args)
    {
        return new List<ExpASTNode>(
            args.Select(e => (ExpASTNode)new PrimaryExpASTNode(e))
        );
    }
    private List<ExpASTNode> GetArgsList(params PrimitiveValue[] args)
    {
        return ChangeArgsToList(args);
    }
    private List<T> getList<T>(params T[] elements)
    {
        return elements.ToList();
    }
    private void TestFunctionByConstantArgument(int id, string functionName, params PrimitiveValue[] args)
    {
        int funcID = EnemyFunctionFactory.GetInstance().Find(functionName);
        var testCodesList = new List<string>(
            args.Select(e => $"PUSH {e}")
                .Append($"PUSH {id}")
                .Append($"CALL {funcID}")
        );
        var node = new CallFuncASTNode(id, functionName, GetArgsList(args));
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
        TestFunctionByConstantArgument(0, "set_bullets_in_circular_pattern", 7.5f);
    }
    public void test_ScatterBulletsInCircularPatternCallFuncASTNode()
    {
        TestFunctionByConstantArgument(0, "scatter_bullets_in_circular_pattern", 0.1f, 7.5f);
    }

    /* サンプルプログラム */
    /*
        bullet >>
        ID: 0;
        generate_bullets(24);
        set_bullets_position_at_enemy(id);
        scatter_bullets_in_circular_pattern(0.1f, 0f);
        delay_bullets(60);
        scatter_bullets_in_circular_pattern(0.1f, 180f);
        delay_bullets(120);

        ID: 1;
        generate_bullets(24);
        delay_bullets(60);
        set_bullets_position_at_enemy(id);
        scatter_bullets_in_circular_pattern(0.1f, 0f);
        delay_bullets(60);
        move_bullets_parallel(0.1f, 30f);
        delay_bullets(120);

        action >>
        activate_bullets(0);
        activate_bullets(1);
    */
    /* 上のサンプルに対応する AST の動作を確認する */
    public void test_CallFuncASTNode()
    {
        int id;
        id = 0;
        var nodes = getList(
            new CallFuncASTNode(
                id,
                "generate_bullets",
                GetArgsList(24)
            ),
            new CallFuncASTNode(
                id,
                "set_bullets_position_at_enemy",
                GetArgsList()
            ),
            new CallFuncASTNode(
                id,
                "scatter_bullets_in_circular_pattern",
                GetArgsList(0.1f, 0f)
            ),
            new CallFuncASTNode(
                id,
                "delay_bullets",
                GetArgsList(60)
            ),
            new CallFuncASTNode(
                id,
                "scatter_bullets_in_circular_pattern",
                GetArgsList(0.1f, 180f)
            ),
            new CallFuncASTNode(
                id,
                "delay_bullets",
                GetArgsList(120)
            ),
            new CallFuncASTNode(
                id,
                "activate_bullets",
                GetArgsList()
            )
        );

        id = 1;
        nodes.AddRange(
            getList(
                new CallFuncASTNode(
                    id,
                    "generate_bullets",
                    GetArgsList(24)
                ),
                new CallFuncASTNode(
                    id,
                    "delay_bullets",
                    GetArgsList(60)
                ),
                new CallFuncASTNode(
                    id,
                    "set_bullets_position_at_enemy",
                    GetArgsList()
                ),
                new CallFuncASTNode(
                    id,
                    "scatter_bullets_in_circular_pattern",
                    GetArgsList(0.1f, 0f)
                ),
                new CallFuncASTNode(
                    id,
                    "delay_bullets",
                    GetArgsList(60)
                ),
                new CallFuncASTNode(
                    id,
                    "move_bullets_parallel",
                    GetArgsList(0.1f, 30f)
                ),
                new CallFuncASTNode(
                    id,
                    "delay_bullets",
                    GetArgsList(120)
                ),
                new CallFuncASTNode(
                    id,
                    "activate_bullets",
                    GetArgsList()
                )
            )
        );

        var enemy = GameObject.FindObjectOfType<EnemyComponent>();
        var vm = new EnemyVM(enemy);
        var vtable = new Dictionary<string, int>();
        var instructions = nodes.Select(node => node.Compile(vtable))
            .SelectMany(instructions => instructions).ToList();
        instructions.ForEach(instruction => vm.appendInstruction(instruction));
        while (!vm.IsExit)
        {
            vm.run();
        }
    }
}

