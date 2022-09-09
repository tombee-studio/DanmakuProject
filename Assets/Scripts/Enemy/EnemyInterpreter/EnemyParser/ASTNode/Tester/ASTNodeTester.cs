using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester: Tester
{
    protected override Tester cloneThisObject()
    {
        return new EnemyASTNodeTester();
    }
    private string GetTabs(int tabCount)
    {
        string tab = "\t";
        string tabs = "";
        for (int i = 0; i < tabCount; i++)
        {
            tabs += tab;
        }
        return tabs;
    }

    protected void checkGeneratedInstructionIsSame(string[] testCodes, ASTNode astNode){
        Debug.Log("checkGeneratedInstructionIsSame called");
        foreach (var instruction in astNode.Compile(null)
            .Zip(testCodes, (i1, i2) => (i1, i2)))
        {
            var (i1, i2) = instruction;
            Assert.AreEqual($"{i1}", i2);
        }
    }
}

