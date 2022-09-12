using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Banner : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });

        if (PlayerPrefs.HasKey("NoAds") == false)
        {
            PlayerPrefs.SetInt("NoAds", 0);
        }

        if (PlayerPrefs.GetInt("NoAds") == 0)
        {
            this.RequestBanner();
        }
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexcepted_platform";
#endif

        // Create a 320x50 banner at the top of the screen
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request
        this.bannerView.LoadAd(request);
    }
}
