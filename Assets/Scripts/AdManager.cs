using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = true;
    public static AdManager Instance;

    private GameOverHandler gameOverHandler;

#if UNITY_ANDROID
    private string gameID = "4881981";
#elif UNITY_IOS
    private string gameID = "4881981";
#endif

  // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this);
            Advertisement.Initialize(gameID,testMode);
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        this.gameOverHandler = gameOverHandler;

        Advertisement.Show("Rewarded_Video");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Finished:
                gameOverHandler.ContinueGame();
                break;

            case ShowResult.Skipped:
                Debug.LogWarning("Ad Skipped");
                break;

            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"Unity Ads Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log($"Unity Ads Ready");
    }
}
