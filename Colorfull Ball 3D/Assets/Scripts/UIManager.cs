using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sound_Manager soundManager;

    public Image whiteEffect;
    public Animator LayoutAnimator;

    private int Counting = 0;
    private bool radialShine;


    [Header("Draw Line")]
    public GameObject Player;
    public GameObject FinishLine;
    public Image fillRate;
    [Header("Settings")]
    public GameObject settingsOpen_Button;
    public GameObject settingsClose_Button;
    public GameObject Layout_Background;
    [Header("Sounds")]
    public GameObject soundOpen_Button;
    public GameObject soundClose_Button;
    [Header("Vibration")]
    public GameObject vibrationOpen_Button;
    public GameObject vibrationClose_Button;
    [Header("Others")]
    public GameObject Restart_Button;
    public TMP_Text Coin_Text;
    public GameObject IAP_Button;
    public GameObject Info_Button;
    [Header("Start Game")]
    public GameObject slideHand;
    public GameObject touchToText;
    public GameObject noAds;
    public GameObject shopping;
    [Header("Finish Screen")]
    public GameObject finishScreen;
    public GameObject finish_Background;
    public GameObject Completed_Image;
    public GameObject radial_shine;
    public GameObject finish_Coin;
    public GameObject finish_Ads;
    public GameObject noThanks;
    [Header("Next Level")]
    public GameObject awardedCoin;
    public GameObject nextLevel;
    public TMP_Text awarded_Text;

    public void Start()
    {
        coinTextUpdate();

        if (PlayerPrefs.HasKey("Sound") == false)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }

        if (PlayerPrefs.HasKey("Vibration") == false)
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }

        if (PlayerPrefs.GetInt("NoAds") == 1)
        {
            NoAdsRemove();
        }
    }

    public void Update()
    {
        if (radialShine == true)
        {
            radial_shine.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 15f * Time.deltaTime));
        }

        fillRate.fillAmount = ((Player.transform.position.z * 100) / (FinishLine.transform.position.z)) / 100;
    }

    public void FirstTouch()
    {
        slideHand.SetActive(false);
        touchToText.SetActive(false);
        noAds.SetActive(false);
        shopping.SetActive(false);
        settingsOpen_Button.SetActive(false);
        settingsClose_Button.SetActive(false);
        soundOpen_Button.SetActive(false);
        soundClose_Button.SetActive(false);
        vibrationOpen_Button.SetActive(false);
        vibrationClose_Button.SetActive(false);
        IAP_Button.SetActive(false);
        Info_Button.SetActive(false);
        Layout_Background.SetActive(false);
    }

    public void NoAdsRemove()
    {
        noAds.SetActive(false);
    }
    
    public void coinTextUpdate()
    {
        Coin_Text.text = PlayerPrefs.GetInt("moneyy").ToString();
    }

    public void FinishScreen()
    {
        StartCoroutine("FinishLaunch");
    }

    public IEnumerator FinishLaunch()
    {
        Time.timeScale = 0.3f;
        radialShine = true;

        finishScreen.SetActive(true);
        finish_Background.SetActive(true);
        yield return new WaitForSecondsRealtime(0.003f);
        Completed_Image.SetActive(true);
        soundManager.CompletedSound();
        yield return new WaitForSecondsRealtime(0.5f);
        soundManager.CompletedSound();
        radial_shine.SetActive(true);
        finish_Coin.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        finish_Ads.SetActive(true);
        soundManager.CompletedSound();
        yield return new WaitForSecondsRealtime(1.5f);
        noThanks.SetActive(true);

    }

    public IEnumerator AfterRewardButton()
    {
        awardedCoin.SetActive(true);
        awarded_Text.gameObject.SetActive(true);
        finish_Ads.SetActive(false);
        noThanks.SetActive(false);

        for (int i = 0; i <= 400; i += 4)
        {
            awarded_Text.text = "+" + i.ToString();
            yield return new WaitForSecondsRealtime(0.0001f);
        }

        yield return new WaitForSecondsRealtime(1f);
        nextLevel.SetActive(true);
    }


    //Restart Area
    public void Restart() 
    {
        Restart_Button.SetActive(true);
    }

    public void RestartScene()
    {
        Variables.firstTouch = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Variables.firstTouch = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Settings Buttons Functions
    public void SettingsOpen()
    {
        settingsOpen_Button.SetActive(false);
        settingsClose_Button.SetActive(true);
        LayoutAnimator.SetTrigger("Slide_In");


        //Sound
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundOpen_Button.SetActive(true);
            soundClose_Button.SetActive(false);
            AudioListener.volume = 1;
        }

        else if (PlayerPrefs.GetInt("Sound") == 2)
        {
            soundOpen_Button.SetActive(false);
            soundClose_Button.SetActive(true);
            AudioListener.volume = 0;
        }

        //Vibration
        if(PlayerPrefs.GetInt("Vibration") == 1)
        {
            vibrationOpen_Button.SetActive(true);
            vibrationClose_Button.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("Vibration") == 2)
        {
            vibrationOpen_Button.SetActive(false);
            vibrationClose_Button.SetActive(true);
        }
    }

    public void SettingsClose()
    {
        settingsOpen_Button.SetActive(true);
        settingsClose_Button.SetActive(false);
        LayoutAnimator.SetTrigger("Slide_Out");
    }

    //Sound Buttons Functions
    public void SoundOpen()
    {
        soundOpen_Button.SetActive(false);
        soundClose_Button.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("Sound", 2);
    }

    public void SoundClose()
    {
        soundOpen_Button.SetActive(true);
        soundClose_Button.SetActive(false);
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("Sound", 1);
    }

    //Vibration Buttons Functions
    public void VibrationOpen()
    {
        vibrationOpen_Button.SetActive(false);
        vibrationClose_Button.SetActive(true);
        PlayerPrefs.SetInt("Vibration", 2);
    }

    public void VibrationClose()
    {
        vibrationOpen_Button.SetActive(true);
        vibrationClose_Button.SetActive(false);
        PlayerPrefs.SetInt("Vibration", 1);
    }

    // Link Functions
    public void Privacy_Policy() 
    {
        Application.OpenURL("http://grapoyn.com");
    }

    public void TermOfUse()
    {
        Application.OpenURL("http://grapoyn.com");
    }

    public IEnumerator WhiteEffect()
    {
        whiteEffect.gameObject.SetActive(true);
        while (Counting == 0)
        {
            yield return new WaitForSeconds(0.01f);
            whiteEffect.color += new Color(0, 0, 0, 0.1f);
            if (whiteEffect.color == new Color(whiteEffect.color.r, whiteEffect.color.g, whiteEffect.color.b, 1))
            {
                Counting = 1;
            }
        }

        while (Counting == 1)
        {
            yield return new WaitForSeconds(0.01f);
            whiteEffect.color -= new Color(0, 0, 0, 0.1f);
            if (whiteEffect.color == new Color(whiteEffect.color.r, whiteEffect.color.g, whiteEffect.color.b, 0))
            {
                Counting = 2;
            }
        }
    }
}
