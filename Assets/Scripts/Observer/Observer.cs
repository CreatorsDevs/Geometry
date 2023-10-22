using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private GameObject swipeUI;
    [SerializeField] private GameObject boostUI;

    private void Start()
    {
        ServiceLocator.Get<ObserverSystem>().OnGameStart += HandleGameStart;
        ServiceLocator.Get<ObserverSystem>().OnBoostAvailable += HandleBoostAvailable;
        ServiceLocator.Get<ObserverSystem>().OnBoostActivated += HandleBoostActivated;
        ServiceLocator.Get<ObserverSystem>().OnSwipe += HandleSwipe;
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<ObserverSystem>().OnGameStart -= HandleGameStart;
        ServiceLocator.Get<ObserverSystem>().OnBoostAvailable -= HandleBoostAvailable;
        ServiceLocator.Get<ObserverSystem>().OnBoostActivated -= HandleBoostActivated;
        ServiceLocator.Get<ObserverSystem>().OnSwipe -= HandleSwipe;
    }

    private void HandleGameStart()
    {
        //swipeUI.SetActive(true);
        GameManager.Instance.ActivateSwipeUI();
    }

    private void HandleBoostAvailable()
    {
        //boostUI.SetActive(true);
        GameManager.Instance.ActivateBoostButtonUI();
    }

    private void HandleBoostActivated()
    {
        //boostUI.SetActive(false);
        GameManager.Instance.DeactivateBoostButtonUI();
    }

    private void HandleSwipe()
    {
        //swipeUI.SetActive(false);
        GameManager.Instance.DeactivateSwipeUI();
    }
}
