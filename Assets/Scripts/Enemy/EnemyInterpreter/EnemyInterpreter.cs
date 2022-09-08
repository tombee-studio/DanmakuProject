public class EnemyInterpreter
{
    #nullable enable
    EnemyCompiler? _compiler=null;
    public EnemyCompiler compiler {get => _compiler ??= new EnemyCompiler();}

    EnemyVM? _vm;
    EnemyComponent enemyComponent;
    public EnemyVM vm {get => _vm ??= new EnemyVM(enemyComponent); }
    public bool IsContinue { get =>  vm.IsContinute; }
    public bool IsExit {  get => vm.IsExit; }
    public int ReturnValue { get => vm.ReturnValue; }

    public EnemyInterpreter(EnemyComponent enemyComponent)
    {
        this.enemyComponent = enemyComponent;
    }

    public void run(){
        vm.run();
    }

    public void test_run() {
        var tester1 = new EnemyVMTester();
        tester1.runTests();

        var tester2 = new EnemyFunctionsFatoryTester();
        tester2.runTests();

        var tester3 = new EnemyLexerTester();
        tester3.runTests();

        var tester4 = new EnemyASTNodeTester();
        tester4.runTests();
    }
}
