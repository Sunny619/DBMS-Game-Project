using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{

    public GameObject canvas;
    public GameObject[] Objects;

    void Start()
    {
        canvas.SetActive(true);
    }
    public void activate(int i)
    {
        Objects[i].SetActive(true);
    }
}
