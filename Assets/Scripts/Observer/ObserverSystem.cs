public class ObserverSystem : Singleton<ObserverSystem>
{
    public delegate void Action();
    public event Action OnGameStart;
    public event Action OnBoostAvailable;
    public event Action OnBoostActivated;
    public event Action OnSwipe;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }

    public void NotifyGameStart()
    {
        OnGameStart?.Invoke();
    }

    public void NotifyBoostAvailable()
    {
        OnBoostAvailable?.Invoke();
    }

    public void NotifyBoostActivated()
    {
        OnBoostActivated?.Invoke();
    }

    public void NotifySwipe()
    {
        OnSwipe?.Invoke();
    }
}
