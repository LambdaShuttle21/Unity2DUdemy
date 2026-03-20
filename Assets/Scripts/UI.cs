using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField] private GameObject gameOverUI;
    [Space]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;
    private bool gameOverAlreadyShown;

    private int killCount;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;// every time the scene restarts, the time is set
        // to his normal value (100 percent)
    }
    private void Update()
    {
        timerText.text = Time.time.ToString("F2") + "s";// first 2 decimals and "s" of seconds
    }
    public void EnableGameOverUI()
    {
        if (gameOverAlreadyShown == true)
        {
            return;
        }

        gameOverAlreadyShown = true;

        DatabaseManager.Instance.GuardarPartida(Time.time, killCount, "Game Over");

        Time.timeScale = 0.5f; // we slowdown the time by 50 percent
        gameOverUI.SetActive(true);
    } // activates the GameObject 
    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    public void AddKillCount()
    {
        killCount++;// indepentent counter, we use Die() function from Entity to execute AddKillCount() each time a Enemy dies
        killCountText.text = killCount.ToString(); // We assign our counter to our killCounterText.text (TextMeshPro game object)
    }
}
