using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool _testMode = true;

    [SerializeField] private string _gameId;

    private string _video = "Interstitial_Android";
    private string _rewardedVideo = "Rewarded_Android";
    private const string Extra_Life = "Extra_Life";
    private const string Add_Coins = "Add_Coins";

    public delegate void RewardAd();
    public static event RewardAd ExtraLife;
    public static event RewardAd AddCoins;

	void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, _testMode);
    }
    public static void ShowAdsVideo(string placementId)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementId);
        }
        else
        {
            Debug.Log("Advertisement not ready!");
        }
    }
	public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            switch (placementId)
            {
                case Extra_Life:
                    ExtraLife?.Invoke();
                    break;
                case Add_Coins:
                    AddCoins?.Invoke();
                    break;
            }
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
    public void OnDestroy()
    {
        AddCoins = null;
        ExtraLife = null;
        Advertisement.RemoveListener(this);
    }
}
