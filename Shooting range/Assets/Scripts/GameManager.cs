using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Синглтон, чтобы любой скрипт пули или мишени мог легко начислить очки
    public static GameManager Instance;

    [Header("Ссылки на интерфейс")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI endGameText; 


    [Header("Настройки игры")]
    [SerializeField] private int totalTargets = 5; // Укажи здесь точное количество мишеней на сцене!

    private int currentScore = 0;
    private int destroyedTargets = 0;
    private float elapsedTime = 0f;
    public bool isGameActive = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false); // Прячем кнопку на старте
            // Привязываем перезапуск сцены к клику по кнопке
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    private void Update()
    {
        if (isGameActive)
        {
            elapsedTime += Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = "Время: " + elapsedTime.ToString("F1") + " сек";
            }
        }
    }

    // Этот метод будет вызывать мишень, когда в неё попали!
    public void AddScore(int points)
    {
        if (!isGameActive) return;

        currentScore += points;
        destroyedTargets++;
        UpdateUI();

        // Проверяем: если все мишени повержены
        if (destroyedTargets >= totalTargets)
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Очки: " + currentScore;
        }
    }

    void EndGame()
    {
        isGameActive = false;
        Debug.Log("Все мишени повержены! Игра окончена.");
        
        // Включаем финальную табличку и выводим в нее итоговые очки и время!
        if (endGameText != null)
        {
            endGameText.gameObject.SetActive(true);
            endGameText.text = "ИГРА ОКОНЧЕНА!\nНабрано очков: " + currentScore + "\nВремя: " + elapsedTime.ToString("F1") + " сек";
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true); // Показываем кнопку
            
            // Включаем курсор мыши, чтобы кликнуть по кнопке
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void RestartGame()
    {
        // Мгновенно перезагружаем текущую сцену с чистого листа
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
