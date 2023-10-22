using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ServiceScriptableObject", menuName = "ScriptableObjects/ServiceScriptableObject")]
public class ServicesScriptableObjects : ScriptableObject
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void EndGame()
    {
        GameManager.Instance.EndGame();
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void ActivateBoost()
    {
        PlayerStateMachine.Instance.ActivateBoost();
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void ToggleSound(MuteAudio muteAudio)
    {
        muteAudio?.onButtonPress();    
    }
}
