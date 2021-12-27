using UnityEngine;


public delegate void GameOverHandler(); 

public class GameManager : MonoBehaviour
{
        [SerializeField]
        private EnemyManager enemyManager;

        [SerializeField]
        private GameObject playerPrefab;

        private Player player;

        private GenericFSM stateMachine;

        private GameplayState gameplayState;
        private EndgameState endgameState;

        public Player Player => player;
        public event GameOverHandler OnGameOver;

        private void Awake()
        {
                var playerStart = FindObjectOfType<PlayerStart>();
                var playerStartPosition = Vector3.zero;
                var playerStartRotation = Quaternion.identity;
                if (playerStart != null)
                {
                        var playerStartTransform = playerStart.transform;
                        playerStartPosition = playerStartTransform.position;
                        playerStartRotation = playerStartTransform.rotation;
                }

                player = FindObjectOfType<Player>();
                if (player == null)
                {
                        player = Instantiate(playerPrefab).GetComponent<Player>();
                }
                player.transform.SetPositionAndRotation(playerStartPosition, playerStartRotation);

                stateMachine = new GenericFSM();
                gameplayState = new GameplayState(enemyManager, player);
                endgameState = new EndgameState();
        }

        private void Start()
        {
                gameplayState.Init();
                endgameState.Init();

                player.PlayerState.OnHealthChanged += OnPlayerHealthChanged;
                stateMachine.EnterNewState(gameplayState);
        }

        private void OnPlayerHealthChanged(int oldValue, int newValue)
        {
                if (newValue > 0) return;

                // Oh no, we are dead
                OnGameOver?.Invoke();
                stateMachine.EnterNewState(endgameState);
        }
}