using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(10)]
public class GameManager : Singleton<GameManager>
{
    public GameObject gameMenuPanel;
    public GameObject boostButton;
    private PlayerController playerController;
    public bool GameStarted {  get; private set; }
    
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
            boostButton.SetActive(true);
        }
    }

    public void ShowGameMenu()
    {
        gameMenuPanel.SetActive(true);
        boostButton.SetActive(false);
    }

    public void StartGame()
    {
        gameMenuPanel.SetActive(false);
        GameStarted = true;
        playerController = PlayerService.Instance.SpawnSelectedPlayer();
    }

    public void SetPlayerModelMoveSpeed(float moveSpeed)
    {
        if (playerController == null) return;
        playerController.PlayerModel.MoveSpeed = moveSpeed;
    }
}
