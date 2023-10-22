using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(10)]
public class ScoreManager : Singleton<ScoreManager>
{
    public int scorePerSecond = 10;
    private TextMeshProUGUI score; // Reference to UI Text component to display score
    public GameObject scorePanelUI;

    private float currentScore;
    private bool isScoring = false; // To track if we should score or not
    private GameObject m_ScorePanelUI;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }
    void Start()
    {
        currentScore = 0;
        m_ScorePanelUI = Instantiate(scorePanelUI) as GameObject;
        m_ScorePanelUI.SetActive(false);
        score = m_ScorePanelUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    void Update()
    {
        if (isScoring)
        {
            IncreaseScore();
        }
    }

    public void StartScoring(PlayerController playerController)
    {
        Debug.Log("Started Scoring");
        isScoring = true;
        m_ScorePanelUI.SetActive(true);
        if (playerController.PlayerModel.PlayerType == PlayerType.Slow)
            scorePerSecond = 10;
        if (playerController.PlayerModel.PlayerType == PlayerType.Fast)
            scorePerSecond = 15;
    }

    public void StopScoring()
    {
        Debug.Log("Stopped Scoring");
        isScoring = false;
        m_ScorePanelUI.SetActive(false);
    }

    void IncreaseScore()
    {
        Debug.Log("Increasing Score");

        if(!PlayerStateMachine.Instance.activateBoost)
            currentScore += scorePerSecond * Time.deltaTime;
        else if(PlayerStateMachine.Instance.activateBoost)
            currentScore += (scorePerSecond * 2) * Time.deltaTime;

        Debug.Log("Score: " + Mathf.FloorToInt(currentScore) + " ScorePerSecond: " + scorePerSecond);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (score != null)
        {
            //score.text = currentScore.ToString();
            score.text = Mathf.FloorToInt(currentScore).ToString();
        }
    }

    public int GetCurrentScore()
    {
        return Mathf.FloorToInt(currentScore);
    }

    // Call this method to reset the score, e.g., when starting a new game
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }
}
