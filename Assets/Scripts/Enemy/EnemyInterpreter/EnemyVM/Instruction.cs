using System;
public partial class EnemyVM
{
    public struct Instruction
    {
        public Mnemonic mnemonic;
        public PrimitiveValue argument;
        public Instruction(Mnemonic mnemonic, PrimitiveValue argument)
        {
            this.mnemonic = mnemonic;
            this.argument = argument;
        }

        public override string ToString() => $"{mnemonic} {argument}";
    }
}
