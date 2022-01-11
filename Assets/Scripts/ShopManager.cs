using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    int coins = 3000;

    public TextMeshProUGUI coinsText;
    int[] cost={0,1000,2000,3000,4000,5000};
    int[] skins={1,1,0,0,0,0};

    public int currentSkin = 0;


    public TextMeshProUGUI prompt;
    void Awake()
    {
        coinsText.text = coins.ToString();
        currentSkin =  PlayerPrefs.GetInt("current_skin",0);
        //TODO:Update coins from database
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuySkin(int i)
    {
        if(skins[i]==1)
        {
            currentSkin = i;
            prompt.text = "Skin "+i+" Equipped";
            PlayerPrefs.SetInt("current_skin",i);
        }
        else if(coins >= cost[i])
        {
            coins-=cost[i];
            //TODO:Update player owns
            skins[i]=1;
            prompt.text = "Skin "+i+"  Bought and Equipped";
            coinsText.text = coins.ToString();
            PlayerPrefs.SetInt("current_skin",i);
        }
        else
        {
            prompt.text = "Not Enough Coins";
        }
    }
}
