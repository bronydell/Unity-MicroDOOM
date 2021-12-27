public class GameplayState : GenericState
{
    private HUDManager gameplayHUDManager;
    private EnemyManager enemyManager;
    private Player player;

    public GameplayState(EnemyManager enemyManager, Player player)
    {
        gameplayHUDManager = Singleton<UIManager>.Instance.HUDManager;
        this.enemyManager = enemyManager;
        this.player = player;
    }

    public override void Init()
    {
        base.Init();
        StopGameplay();
    }

    public override void Enter()
    {
        base.Enter();
        gameplayHUDManager.Show();
        enemyManager.StartSpawning();
        player.Activate();
    }

    public override void Exit()
    {
        base.Exit();
        StopGameplay();
    }

    private void StopGameplay()
    {
        gameplayHUDManager.Hide();
        enemyManager.StopSpawning();
        enemyManager.StopAllEnemies();
        player.Deactivate();
    }
}