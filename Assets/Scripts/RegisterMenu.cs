using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class RegisterMenu : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    public GameObject registerSucessPanel;
    public TextMeshProUGUI userExistsText;
    SqliteDatabase DB;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Toggle genderInput;
    public TMP_InputField DOBInput;
    string username;
    string password;
    string gender;
    string DOB;

    Converter C = new Converter();
    void Awake()
    {
        PlayerPrefs.DeleteAll();
        DB = new SqliteDatabase("GameDB.db");
    }
    public void Register()
    {
        username = usernameInput.text;
        password = passwordInput.text;
        gender = genderInput.isOn?"m":"f";
        DOB = DOBInput.text;
        if(username.Contains("\"")||password.Contains("\""))
        {
            userExistsText.text = "Use of \" character is restricted";
            return;
        }
        if(UserExists())
        {
            RegistrationFail();
        }
        else
        {
            RegisterSuccess();
        }
        Debug.Log(username + " " + password+ " "+ gender + " " + DOB );
        
        
    }
    void RegistrationFail()
    {
        userExistsText.text="Username already exists. Please choose a different username.";
    }
    void RegisterSuccess()
    {
        string query = "Insert into player VALUES (\""+username+"\",\""+password+"\",\""+ gender +"\",\""+ DOB + "\");";
        Debug.Log(query);
        try{
            DB.ExecuteNonQuery(query);
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        
        PlayerPrefs.SetString("username",username);
        userExistsText.text="";
        registerSucessPanel.SetActive(true);
        usernameText.text+= PlayerPrefs.GetString("username","user");
        InitDB();
        Invoke("GotoMainMenu",1.5f);
    }
    void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    bool UserExists()
    {
        DataTable a  = DB.ExecuteQuery("Select * from player where username = \""+username+"\";");
        if(a.Rows.Count==0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    void InitDB()
    {
        string query = C.InsertTable("playerprefs",new string[]{PlayerPrefs.GetString("username","user"),"0","4000","0","0","6"});
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
        query = C.InsertTable("owns",new string[]{PlayerPrefs.GetString("username","user"),"0"});
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
        query = C.InsertTable("owns",new string[]{PlayerPrefs.GetString("username","user"),"6"});
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
        InitPrefs();
    }
    void InitPrefs()
    {
        string current_skin ="", current_bg="", coins="",progress="", volume="";
        DataTable dt= DB.ExecuteQuery(C.SelectTable("playerprefs",PlayerPrefs.GetString("username","user")));
        foreach(DataRow row in dt.Rows)
        {
            current_skin += row["current_skin"].ToString();
            current_bg += row["current_bg"].ToString();
            coins += row["coins"].ToString();
            progress += row["progress"].ToString();
            volume += row["volume"].ToString();
        }
        Debug.Log(current_skin+current_bg+coins+progress+volume);
        PlayerPrefs.SetInt("current_skin",Int32.Parse(current_skin));
        PlayerPrefs.SetInt("current_bg",Int32.Parse(current_bg));
        PlayerPrefs.SetInt("coins",Int32.Parse(coins));
        PlayerPrefs.SetInt("progress",Int32.Parse(progress));
        PlayerPrefs.SetInt("volume",Int32.Parse(volume));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
