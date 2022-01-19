using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] backgrounds;
    int currentBackground;
    SpriteRenderer sr;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        currentBackground = PlayerPrefs.GetInt("current_bg", 0)-6;
        Debug.Log("Cuurent BG:" + currentBackground);
        sr.sprite = backgrounds[currentBackground];
        sr.size = new Vector2(300f,100f);
    }
}
