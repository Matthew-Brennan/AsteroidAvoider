using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdManager Instance;

    [SerializeField] string _androidGameId = "4881981";
    [SerializeField] string _iOSGameId = "4881980";
    [SerializeField] bool _testMode = true;

    private string _gameId;



    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }
        InitializeAds();
    string _adUnitId = null;
#if UNITY_IOS
        _adUnitId=_iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId=_androidGameId;
#endif

    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}