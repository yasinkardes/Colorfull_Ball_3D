using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private InterstitialAd intersitital;
    private RewardedAd rewardedAd;

    public UIManager uiManager;

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.intersitital = new InterstitialAd(adUnitId);

        // Create an empty ad request
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request
        this.intersitital.LoadAd(request);
    }

    public void RequestRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);


        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        //Create an empty ad request
        AdRequest request = new AdRequest.Builder().Build();

        //Load the rewarded ad with the request
        this.rewardedAd.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("NoAds") == 0)
        {
            if (this.intersitital.IsLoaded())
            {
                this.intersitital.Show();
            }
        }
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    // Called when an ad is shown.
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    // Called when the ad is closed.
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    // Called when the user should be rewarded for interacting with the ad.
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        CoinCalculator(400);
        uiManager.StartCoroutine("AfterRewardButton");
    }

    public void CoinCalculator(int money)
    {
        if (PlayerPrefs.HasKey("moneyy"))
        {
            int oldScore = PlayerPrefs.GetInt("moneyy");
            PlayerPrefs.SetInt("moneyy", oldScore + money);
        }
        else
        {
            PlayerPrefs.SetInt("moneyy", 0);
        }
    }

    public void OnDestroy()
    {
        if (intersitital != null)
        {
            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                intersitital.Destroy();
            }
        }
    }
}

