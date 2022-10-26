using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

// TODO: 設定は反映しない
public class RepeatStASTNode : RepeatStASTNodeBase
{
    static int loopCount = 0;
    int N;
    StatementASTNodeBase statement;
    public RepeatStASTNode(int numberOfExecutions, StatementASTNodeBase statement)
    {
        this.N = numberOfExecutions;
        this.statement = statement;
        if (numberOfExecutions < 0) { throw new NotSupportedException("ループ回数を負の値にすることはできません"); }
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        // ユーザー定義変数名に @ を登録することはできないことを利用して, 任意のユーザー定義変数名と重複しないループカウンタ変数を定義
        var counterName = $"@__repeat_loop_counter_{loopCount}__";

        // var i;
        var counterAddr = vtable.Count;
        vtable.Add(counterName, counterAddr);

        // i = 0;
        var assignment = new AssignStASTNode(counterName, new PrimaryExpASTNode(0))  // @__loop_counter__ = 0
                .Compile(vtable);

        var loopBegin = new List<EnemyVM.Instruction>()
            // :loop_begin
            // if (not(i<N)) break;  -> if(not(i<N)) goto :end_loop;
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, counterAddr))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.LOAD, 0))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, N))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.LT, 0))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.JNE, 0)).ToList();

        var body = statement.Compile(vtable);

        var loopTail = new List<EnemyVM.Instruction>()
            //i++
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, counterAddr))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.LOAD, 0))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, counterAddr))
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.STORE, 0))
            //goto :loop_begin
            .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.JMP, 0))
            .ToList();
        // :end_loop

        loopBegin = loopBegin.Select(e =>
        {
            if (e.mnemonic == EnemyVM.Mnemonic.JNE)
            {
                return new EnemyVM.Instruction(EnemyVM.Mnemonic.JNE, body.Count + loopTail.Count);
            }
            return e;
        }).ToList();
        loopTail = loopTail.Select(e =>
        {
            if (e.mnemonic == EnemyVM.Mnemonic.JMP)
            {
                return new EnemyVM.Instruction(EnemyVM.Mnemonic.JMP, -loopTail.Count - body.Count - loopBegin.Count);
            }
            return e;
        }).ToList();

        // 子 statement 内の break 文から生成された BREAK 命令をループ末尾へのジャンプに置換
        // 内側ループの BREAK は置換済みであることを想定
        for (int i = 0; i < body.Count; i++)
        {
            if (body[i].mnemonic != EnemyVM.Mnemonic.BREAK) continue;
            body[i] = new EnemyVM.Instruction(EnemyVM.Mnemonic.JMP, body.Count - i + loopTail.Count - 1);
        }

        return assignment
            .Concat(loopBegin)
            .Concat(body)
            .Concat(loopTail).ToList();
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + "repeat(" + N + ")" + statement.Print(tab);
    }
}
