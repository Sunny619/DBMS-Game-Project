using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    
    public SqliteDatabase DB;
    public Converter C;
    void Awake()
    {
        DB = new SqliteDatabase("GameDB.db");
        C = new Converter();
        slider.value = PlayerPrefs.GetInt("volume");   
    }
    public void UpdateVolume()
    {
        PlayerPrefs.SetInt("volume",(int)(slider.value));
        mixer.SetFloat("Volume",slider.value);
        string query = C.SUpdateTable("playerprefs","volume",PlayerPrefs.GetInt("volume").ToString(),PlayerPrefs.GetString("username"));
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
    }
}
