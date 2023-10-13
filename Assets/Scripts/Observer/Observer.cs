using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private GameObject swipeUI;
    [SerializeField] private GameObject boostUI;
    // Start is called before the first frame update
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
        swipeUI.SetActive(true);
    }

    private void HandleBoostAvailable()
    {
        boostUI.SetActive(true);
    }

    private void HandleBoostActivated()
    {
        boostUI.SetActive(false);
    }

    private void HandleSwipe()
    {
        swipeUI.SetActive(false);
    }
}
