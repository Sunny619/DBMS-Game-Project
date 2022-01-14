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
        string query = "Insert into Player VALUES (\""+username+"\",\""+password+"\",\""+ gender +"\",\""+ DOB + "\");";
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
        Invoke("GotoMainMenu",2f);
    }
    void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    bool UserExists()
    {
        DataTable a  = DB.ExecuteQuery("Select * from Player where username = \""+username+"\";");
        if(a.Rows.Count==0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
