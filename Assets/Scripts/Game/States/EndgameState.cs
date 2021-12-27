using UnityEngine.SceneManagement;

public class EndgameState : GenericState
{
    private DeathScreenManager deathScreenManager;

    public EndgameState()
    {
        deathScreenManager = Singleton<UIManager>.Instance.DeathScreenManager;
    }

    public override void Init()
    {
        base.Init();
        deathScreenManager.Hide();
    }

    public override void Enter()
    {
        base.Enter();
        deathScreenManager.Show();
    }

    public override void Exit()
    {
        base.Exit();
        deathScreenManager.Hide();
        // I think most systems are ready for recycling, but I'm tired :(
        SceneManager.LoadScene(0);
    }
}