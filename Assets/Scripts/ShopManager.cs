using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    int coins = 5000;

    public TextMeshProUGUI coinsText;
    int[] cost = { 0, 1000, 2000, 3000, 4000, 5000, 0, 1000, 2000, 3000, 4000, 5000 };
    int[] items;
    public TextMeshProUGUI[] texts;
    public int currentSkin = 0;
    public int currentBackground = 0;

    public TextMeshProUGUI prompt;
    void Awake()
    {
        coinsText.text = coins.ToString();
        currentSkin = PlayerPrefs.GetInt("current_skin", 0);
        currentBackground = PlayerPrefs.GetInt("current_background", 0);
        items = new int[12];
        for (int i = 0; i < 12; i++)
        {
            items[i] = 0;
            if (items[i] == 1)
            {
                texts[i].text = "Equip";
            }
            //TODO:Assign value fetched from DB;
        }
        //TODO:Update coins from database
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Buy(int i)
    {
        if (items[i] == 1)
        {
            if (i < 6)
            {
                currentSkin = i;
                prompt.text = "Skin " + i + " Equipped";
                PlayerPrefs.SetInt("current_skin", i);
            }
            else
            {
                currentBackground = i;
                prompt.text = "Background " + i + " Equipped";
                PlayerPrefs.SetInt("current_background", i);
            }

        }
        else if (coins >= cost[i])
        {
            coins -= cost[i];
            //TODO:Update player owns
            items[i] = 1;
            coinsText.text = coins.ToString();
            texts[i].text = "Equip";
            if (i < 6)
            {
                prompt.text = "Skin " + i + "  Bought and Equipped";
                PlayerPrefs.SetInt("current_skin", i);    
            }
            else
            {
                prompt.text = "Baackground " + i + "  Bought and Equipped";
                PlayerPrefs.SetInt("current_background", i);
            }  
        }
        else
        {
            prompt.text = "Not Enough Coins";
        }
    }
}
