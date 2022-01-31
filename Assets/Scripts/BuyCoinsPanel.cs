using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyCoinsPanel : MonoBehaviour
{
    SqliteDatabase DB;
    public Converter C;
    public TMP_Dropdown TD;
    public Slider slider;
    int[] amounts = { 10, 20, 35, 50, 100 };
    int[] coins = { 100, 500, 1000, 2000, 5000 };
    List<string> cards = new List<string>();
    int flag = 0;
    void Awake()
    {
        DB = new SqliteDatabase("GameDB.db");
        C = new Converter();
        FetchSavedCards();
    }

    public void FetchSavedCards()
    {
        string query = C.SelectTable("payment", PlayerPrefs.GetString("username"));
        DataTable res = DB.ExecuteQuery(query);
        foreach (DataRow row in res.Rows)
        {
            cards.Add(row["card_no"].ToString());
        }
        if (cards.Count > 0)
        {
            flag = 1;
            TD.ClearOptions();
            TD.AddOptions(cards);
        }
    }
    public void Buy()
    {
        if (flag == 1)
        {
            int i = (int)slider.value;
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + coins[i]);
            string query = "Update playerprefs set coins = " + PlayerPrefs.GetInt("coins", 0) + " where username = \"" + PlayerPrefs.GetString("username", "user") + "\"";
            Debug.Log(query);
            DB.ExecuteNonQuery(query);
            CreateOrdersEntry(i);
        }

    }
    public void CreateOrdersEntry(int i)
    {
        int ord_id = 0;
        string query = "select max(ord_id) from orders";
        DataTable res = DB.ExecuteQuery(query);
        // foreach (DataRow row in res.Rows)
        // {
        //     foreach(var item in row.Values)
        //     {
        //         Debug.Log(item);
        //     }
        // }
        // Debug.Log(res.Rows[0].Values.ToString());
        if (res.Rows[0]["max(ord_id)"] != null)
        {
            ord_id = Int32.Parse(res.Rows[0]["max(ord_id)"].ToString())+1;
        }
            
        Debug.Log(ord_id);
        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
        query = C.InsertTable("orders", new string[] { PlayerPrefs.GetString("username", "user"), TD.options[TD.value].text, ord_id.ToString(), date, amounts[i].ToString() });
        try{
            DB.ExecuteNonQuery(query);
        }
        catch(Exception E)
        {
            Debug.Log(E);
        }

    }
}
