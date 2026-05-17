using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool isGameOver;
    private int score;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        CoreHealth.OnCoreDestroyed += HandleLoss;
        EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        CoreHealth.OnCoreDestroyed -= HandleLoss;
        EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleLoss()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("GAME OVER! Press 'R' to Restart.");
    }

    private void HandleEnemyDeath()
    {
        if (isGameOver) return;
        score += 10;
        Debug.Log("Score Updated: " + score);
    }

    public void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("VICTORY! Press 'R' to Play Again.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}