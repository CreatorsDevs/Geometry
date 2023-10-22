using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[DefaultExecutionOrder(10)]
public class GameManager : Singleton<GameManager>
{
    public GameObject gameMenuPanel;
    public GameObject boostButton;
    public GameObject swipePanel;
    private PlayerController playerController;
    public bool GameStarted {  get; private set; }

    private float m_SwipeUIShowTime = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }

    private void Start()
    {
        ShowGameMenu();
    }

    private void Update()
    {
        if (GameStarted)
        {
            gameMenuPanel.SetActive(false);
        }
    }

    public void ShowGameMenu()
    {
        gameMenuPanel.SetActive(true);
        boostButton.SetActive(false);
        swipePanel.SetActive(false);
    }

    public void StartGame()
    {
        gameMenuPanel.SetActive(false);
        GameStarted = true;
        playerController = PlayerService.Instance.SpawnSelectedPlayer();
        SwipeUI();
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
}
