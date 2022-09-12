using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public UIManager uiManager;

    public GameObject particle_1;
    public GameObject particle_2;
    public GameObject particle_3;
    public GameObject particle_4;

    public Sprite green_Line;
    public Sprite yellow_Line;

    public GameObject shopItem_1;
    public GameObject shopItem_2;
    public GameObject shopItem_3;
    public GameObject shopItem_4;

    public GameObject lock_1;
    public GameObject lock_2;
    public GameObject lock_3;

    public void Item_1()
    {
        //Efektler açýk kapalý
        particle_1.SetActive(true);
        particle_2.SetActive(false);
        particle_3.SetActive(false);
        particle_4.SetActive(false);

        //Çerçeveler açýk kapalý
        shopItem_1.GetComponent<Image>().sprite = green_Line;
        shopItem_2.GetComponent<Image>().sprite = yellow_Line;
        shopItem_3.GetComponent<Image>().sprite = yellow_Line;
        shopItem_4.GetComponent<Image>().sprite = yellow_Line;

        //PlayerPrefs
        PlayerPrefs.SetInt("itemSelect", 0);
    }

    public void Item_2()
    {
        particle_1.SetActive(false);
        particle_2.SetActive(true);
        particle_3.SetActive(false);
        particle_4.SetActive(false);

        shopItem_1.GetComponent<Image>().sprite = yellow_Line;
        shopItem_2.GetComponent<Image>().sprite = green_Line;
        shopItem_3.GetComponent<Image>().sprite = yellow_Line;
        shopItem_4.GetComponent<Image>().sprite = yellow_Line;

        //PlayerPrefs
        PlayerPrefs.SetInt("itemSelect", 1);
    }

    public void Item_3()
    {
        particle_1.SetActive(false);
        particle_2.SetActive(false);
        particle_3.SetActive(true);
        particle_4.SetActive(false);

        shopItem_1.GetComponent<Image>().sprite = yellow_Line;
        shopItem_2.GetComponent<Image>().sprite = yellow_Line;
        shopItem_3.GetComponent<Image>().sprite = green_Line;
        shopItem_4.GetComponent<Image>().sprite = yellow_Line;

        //PlayerPrefs
        PlayerPrefs.SetInt("itemSelect", 2);
    }

    public void Item_4()
    {
        particle_1.SetActive(false);
        particle_2.SetActive(false);
        particle_3.SetActive(false);
        particle_4.SetActive(true);

        shopItem_1.GetComponent<Image>().sprite = yellow_Line;
        shopItem_2.GetComponent<Image>().sprite = yellow_Line;
        shopItem_3.GetComponent<Image>().sprite = yellow_Line;
        shopItem_4.GetComponent<Image>().sprite = green_Line;
    }

    public void Awake()
    {
        if (PlayerPrefs.HasKey("itemSelect") == false)
            PlayerPrefs.SetInt("itemSelect", 0);

        //-----------------Item Select ------------------//
        if (PlayerPrefs.GetInt("itemSelect") == 0)
            Item_1();

        else if (PlayerPrefs.GetInt("itemSelect") == 1)
            Item_2();

        else if (PlayerPrefs.GetInt("itemSelect") == 2)
            Item_3();


        //--------------------------- LOCKS --------------------------------------//
        if (PlayerPrefs.HasKey("lock1Control") == false)
            PlayerPrefs.SetInt("lock1Control", 0);

        if (PlayerPrefs.HasKey("lock2Control") == false)
            PlayerPrefs.SetInt("lock2Control", 0);

        if (PlayerPrefs.HasKey("lock3Control") == false)
            PlayerPrefs.SetInt("lock3Control", 0);

        // Butonlar

        if (PlayerPrefs.GetInt("lock1Control") == 1)
            lock_1.SetActive(false);

        if (PlayerPrefs.GetInt("lock2Control") == 1)
            lock_2.SetActive(false);

        if (PlayerPrefs.GetInt("lock3Control") == 1)
            lock_3.SetActive(false);

    }

    public void Lock_1()
    {
        int money = PlayerPrefs.GetInt("moneyy");
        int lock1Control = PlayerPrefs.GetInt("lock1Control");

        if (money >= 500 && lock1Control == 0)
        {
            lock_1.SetActive(false);
            PlayerPrefs.SetInt("moneyy", money - 500);
            PlayerPrefs.SetInt("lock1Control", 1);
            Item_2();
            uiManager.coinTextUpdate();
        }
    }

    public void Lock_2()
    {
        int money = PlayerPrefs.GetInt("moneyy");
        int lock2Control = PlayerPrefs.GetInt("lock2Control");

        if (money >= 1000 && lock2Control == 0)
        {
            lock_2.SetActive(false);
            PlayerPrefs.SetInt("moneyy", money - 500);
            PlayerPrefs.SetInt("lock2Control", 1);
            Item_3();
            uiManager.coinTextUpdate();
        }
    }

    public void Lock_3()
    {
        int money = PlayerPrefs.GetInt("moneyy");
        int lock3Control = PlayerPrefs.GetInt("lock3Control");

        if (money >= 10000 && lock3Control == 0)
        {
            lock_3.SetActive(false);
            PlayerPrefs.SetInt("moneyy", money - 500);
            PlayerPrefs.SetInt("lock3Control", 1);
            Item_4();
            uiManager.coinTextUpdate();
        }
    }
}
