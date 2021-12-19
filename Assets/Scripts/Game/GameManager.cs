using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        private static GameManager instance;

        public static GameManager Instance => instance;

        [SerializeField]
        private GameObject playerPrefab;

        private Player player;
        
        private void Awake()
        {
                if (instance != null && instance != this)
                {
                        Destroy(gameObject);
                }
                else 
                {
                        instance = this;
                }

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
        }
}