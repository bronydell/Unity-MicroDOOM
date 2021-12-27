using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void Show()
    {
        gameObject.SetActive(true);
        int score = Singleton<GameManager>.Instance.Player.PlayerState.Score;
        scoreText.text = $"{score}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}