using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceManager : Singleton<ServiceManager>
{
    [SerializeField] private List<GameObject> services = new();

    private List<GameObject> m_LoadedServices = new();
    private int m_BuildIndex;
    private static bool s_FirstLoad = true;

    protected override void Awake()
    {
        if (!s_FirstLoad)
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        if (!s_FirstLoad) return;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void LoadServices()
    {
        m_LoadedServices.Clear();

        for (int i = 0; i < services.Count; i++)
            m_LoadedServices.Add(Instantiate(services[i]));
    }

    private void UnloadServices()
    {
        int loadedServicesCount = m_LoadedServices.Count;
        for (int i = loadedServicesCount - 1; i >= 0; i--)
            Destroy(m_LoadedServices[i]);
        m_LoadedServices.Clear();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(s_FirstLoad) 
        {
            m_BuildIndex = scene.buildIndex;
            s_FirstLoad = false;
        }
        
        // Load services
        LoadServices();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.buildIndex != m_BuildIndex) return;

        // Unloading the same scene,
        // Destroy all the managers.
        UnloadServices();
    }
}
