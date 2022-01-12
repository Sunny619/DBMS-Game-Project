using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoginMenu : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    public GameObject loginSucessPanel;

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    string username;
    string password;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Login()
    {
        //TODO:Check is user exists
        //TODO:Check Password and store playerprefs change scene
        username = usernameInput.text;
        password = passwordInput.text;
        Debug.Log(username + " " + password);
        PlayerPrefs.SetString("username",username);
        LoginSuccess();
    }
    void LoginSuccess()
    {
        loginSucessPanel.SetActive(true);
        usernameText.text+= PlayerPrefs.GetString("username","user");
        Invoke("GotoMainMenu",2f);
    }
    void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void UserExists()
    {
        //TODO:Check is user exists
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
