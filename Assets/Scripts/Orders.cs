using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Orders : MonoBehaviour
{
    SqliteDatabase DB;
    public Converter C;
    public GameObject cardPrefab;
    public GameObject content;
    void Awake()
    {
        DB = new SqliteDatabase("GameDB.db");
        C = new Converter();
        FetchOrders();
    }
    //TODO:UPADTE ORDERS ON ORDER FETCH
    public void FetchOrders()
    {
        Debug.Log("Orders Updated");
        int childs = content.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }
        string query = C.SelectTable("orders", PlayerPrefs.GetString("username"));
        DataTable res = DB.ExecuteQuery(query);
        foreach (DataRow row in res.Rows)
        {
            GameObject card = Instantiate(cardPrefab) as GameObject;
            card.SetActive(true);
            card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = row["ord_id"].ToString();
            card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = row["card_no"].ToString();
            card.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = row["ord_date"].ToString();
            card.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = row["amount"].ToString();
            card.transform.SetParent(content.transform, false);
        }
    }
}
