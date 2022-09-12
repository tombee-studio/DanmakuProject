using System;
using System.Collections.Generic;
using System.Linq;

public abstract class ASTNode
{
    public abstract string Print(int tab);
    public abstract List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable);
    protected string GetTabs(int tabCount)
    {
        string tab = "\t";
        string tabs = "";
        for (int i = 0; i < tabCount; i++)
        {
            tabs += tab;
        }
        return tabs;
    }
    protected string GetMergedString<T>(IEnumerable<T> astNodes, int tab, string separator="") where T: ASTNode
    {
        return string.Join(separator, astNodes.Select(e => e.Print(tab)));
    }
    protected List<EnemyVM.Instruction> GetInstructionsForAll<T>(IEnumerable<T> astNodes, Dictionary<string, int> vtable) where T: ASTNode
    {
        var instructions = astNodes.SelectMany(e=>e.Compile(vtable)).ToList();
        return instructions;
    }
}
