public partial class EnemyVM
{
    public struct Instruction
    {
        public Mnemonic mnemonic;
        public int argument;
        public Instruction(Mnemonic mnemonic, int argument)
        {
            this.mnemonic = mnemonic;
            this.argument = argument;
        }
    }
}
