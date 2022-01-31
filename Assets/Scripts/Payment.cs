using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Payment : MonoBehaviour
{
    // Start is called before the first frame update
    SqliteDatabase DB;
    public TMP_InputField cardNo;
    public TMP_InputField cardName;
    public TMP_InputField expdate;
    public GameObject cardPrefab;

    public GameObject content;
    public Converter C;
    void Awake()
    {
        DB = new SqliteDatabase("GameDB.db");
        C = new Converter();
        FetchSavedCards();
    }

    public void SaveCard()
    {
        try
        {
            string query = C.InsertTable("payment", new string[] { PlayerPrefs.GetString("username"), cardNo.text, cardName.text, expdate.text });
            Debug.Log(query);
            DB.ExecuteNonQuery(query);
            FetchSavedCards();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("error");
        }
    }
    public void FetchSavedCards()
    {
        // for (int i = 0; i < 10; i++)
        // {
        //     GameObject card = Instantiate(cardPrefab) as GameObject;
        //     card.SetActive(true);
        //     card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "11234654";
        //     card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "11234654";
        //     card.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "11234654";
        //     card.transform.SetParent(content.transform, false);
        // }
        int childs = content.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }
        string query = C.SelectTable("payment", PlayerPrefs.GetString("username"));
        DataTable res = DB.ExecuteQuery(query);
        foreach (DataRow row in res.Rows)
        {
            GameObject card = Instantiate(cardPrefab) as GameObject;
            card.SetActive(true);
            card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = row["card_no"].ToString();
            card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = row["name"].ToString();
            card.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = row["exp_date"].ToString();
            card.transform.SetParent(content.transform, false);
        }
    }
}
