using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    int coins;
    SqliteDatabase DB;
    public TextMeshProUGUI coinsText;
    int[] cost = { 0, 1000, 2000, 3000, 4000, 5000, 0, 1000, 2000, 3000, 4000, 5000 };
    int[] items;
    public TextMeshProUGUI[] texts;
    public int currentSkin = 0;
    public int currentBackground = 0;
    Converter C = new Converter();
    public TextMeshProUGUI prompt;
    void Awake()
    {
        DB = new SqliteDatabase("GameDB.db");
        currentSkin = PlayerPrefs.GetInt("current_skin", 0);
        currentBackground = PlayerPrefs.GetInt("current_bg", 0);
        coins = PlayerPrefs.GetInt("coins", 0);
        coinsText.text = coins.ToString();
        items = new int[12];
        for (int i = 0; i < 12; i++)
            items[i] = 0;

        FetchItems();
        for (int i = 0; i < 12; i++)
            if (items[i] == 1)
            {
                texts[i].text = "Equip";
            }
    }

    // Update is called once per frame
    void FetchItems()
    {
        string query = C.SelectTable("owns", PlayerPrefs.GetString("username"));
        Debug.Log(query);
        DataTable dt = DB.ExecuteQuery(query);
        foreach (DataRow row in dt.Rows)
        {
            items[Int32.Parse(row["item_id"].ToString())] = 1;
        }
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
                UpdateDBskin();
            }
            else
            {
                currentBackground = i;
                prompt.text = "Background " + i + " Equipped";
                PlayerPrefs.SetInt("current_bg", i);
                UpdateDBbg();

            }

        }
        else if (coins >= cost[i])
        {
            coins -= cost[i];
            PlayerPrefs.SetInt("coins", coins);
            string query = "Update playerprefs set coins = " + coins + " where username = \"" + PlayerPrefs.GetString("username", "user") + "\"";
            Debug.Log(query);
            DB.ExecuteNonQuery(query);
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
                PlayerPrefs.SetInt("current_bg", i);
            }
            UpdateDBowns(i);
        }
        else
        {
            prompt.text = "Not Enough Coins";
        }
    }
    void UpdateDBskin()
    {
        string query = C.SUpdateTable("playerprefs", "current_skin", PlayerPrefs.GetInt("current_skin").ToString(), PlayerPrefs.GetString("username"));
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
    }
    void UpdateDBbg()
    {
        string query = C.SUpdateTable("playerprefs", "current_bg", PlayerPrefs.GetInt("current_bg").ToString(), PlayerPrefs.GetString("username"));
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
    }
    void UpdateDBowns(int item)
    {
        string query = C.InsertTable("owns", new string[] { PlayerPrefs.GetString("username"), item.ToString() });
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
    }
}
