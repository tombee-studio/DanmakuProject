using System;
using System.Collections.Generic;
using System.Linq;

public class BulletASTNode : ASTNode
{
    private List<BulletSectionASTNode> bulletSections;
    public BulletASTNode(List<BulletSectionASTNode> bulletSections)
    {
        this.bulletSections = bulletSections;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return GetInstructionsForAll(bulletSections, vtable);
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + "bullet >> \n"
            + GetMergedString(bulletSections, tab);
    }
}
