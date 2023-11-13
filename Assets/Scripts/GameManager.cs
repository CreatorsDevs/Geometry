using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(10)]
public class GameManager : Singleton<GameManager>
{
    public GameObject gameMenuPanel;
    public GameObject boostButton;
    public GameObject swipePanel;
    public GameObject gameOverPanel;
    public GameObject pauseMenuPanel;
    public GameObject pauseButton;
    public GameObject boostSlider;

    public Image BoostFillImage { get { return m_BoostFillImage; } }

    private PlayerController playerController;
    private GameObject m_GameMenuPanel, m_BoostButton, m_SwipePanel, m_GameOverPanel, m_PauseMenuPanel, m_PauseButton, m_BoostSlider;
    private float m_SwipeUIShowTime = 0.5f;

    private PlatformManager m_PlatformManager;
    private int m_BuildIndex = 0;
    private Image m_BoostFillImage;

    public bool GameStarted {  get; private set; }
    public bool GamePaused { get; private set; }
    public bool GameEnded { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }

    private void Start()
    {
        m_GameMenuPanel = Instantiate(gameMenuPanel) as GameObject;
        //m_GameMenuPanel.SetActive(true);
        m_BoostButton = Instantiate(boostButton) as GameObject;
        m_SwipePanel = Instantiate(swipePanel) as GameObject;
        m_GameOverPanel = Instantiate(gameOverPanel) as GameObject;
        m_PauseMenuPanel = Instantiate(pauseMenuPanel) as GameObject;
        m_PauseButton = Instantiate(pauseButton) as GameObject;
        m_BoostSlider = Instantiate(boostSlider) as GameObject;
        m_BoostFillImage = m_BoostSlider.transform.GetChild(0).GetChild(0).GetComponent<Image>();

        ShowGameMenu();
    }

    private void Update()
    {
        if (GameStarted)
        {
            m_GameMenuPanel.SetActive(false);
        }
    }

    public void ShowGameMenu()
    {
        m_GameMenuPanel.SetActive(true);
        m_BoostButton.SetActive(false);
        m_SwipePanel.SetActive(false);
        m_GameOverPanel.SetActive(false);
        m_PauseMenuPanel.SetActive(false);
        m_PauseButton.SetActive(false);
        m_BoostSlider.SetActive(false);
    }

    public void StartGame()
    {
        m_GameMenuPanel.SetActive(false);
        m_PauseButton.SetActive(true);
        GameStarted = true;
        playerController = PlayerService.Instance.SpawnSelectedPlayer();
        SwipeUI();

        ServiceLocator.Get<ScoreManager>().StartScoring(playerController); // Score Counting
    }

    public void PauseGame()
    {
        GamePaused = true;
        Time.timeScale = 0;
        m_PauseMenuPanel.SetActive(true);
        m_PauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        GamePaused = false;
        Time.timeScale = 1;
        m_PauseMenuPanel.SetActive(false);
        m_PauseButton.SetActive(true);
    }

    public void EndGame()
    {
        //Destroy(playerController.PlayerView);
        GameEnded = true;
        ServiceLocator.Get<ScoreManager>().StopScoring();
        playerController.PlayerView.gameObject.SetActive(false);
        m_PauseButton.SetActive(false);
        StartCoroutine(ShowGameOverPanel());
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        ServiceLocator.Get<ScoreManager>().ResetScore();
        m_BuildIndex = SceneManager.GetActiveScene().buildIndex;
        GameStarted = false;
        SceneManager.LoadScene(m_BuildIndex);
    }

    async void SwipeUI()
    {
        await Task.Delay((int)m_SwipeUIShowTime * 1000);
        ServiceLocator.Get<ObserverSystem>().NotifyGameStart();
        await Task.Yield();
    }

    public void SetPlayerModelMoveSpeed(float moveSpeed)
    {
        if (playerController == null) return;
        playerController.PlayerModel.MoveSpeed = moveSpeed;
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(0.5f);

        m_GameOverPanel.SetActive(true);
        m_GameOverPanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = ServiceLocator.Get<ScoreManager>().GetCurrentScore().ToString();
        StopCoroutine(ShowGameOverPanel());
    }

    public void ActivateSwipeUI()
    {
        m_SwipePanel.SetActive(true);
    }

    public void DeactivateSwipeUI()
    {
        m_SwipePanel.SetActive(false);
    }

    public void ActivateBoostButtonUI()
    {
        m_BoostButton.SetActive(true);
    }

    public void DeactivateBoostButtonUI()
    {
        m_BoostButton.SetActive(false);
    }

    public void ActivateBoostSlider()
    {
        m_BoostSlider.SetActive(true);
    }

    public void DeactivateBoostSlider()
    {
        m_BoostSlider.SetActive(false);
    }
}
