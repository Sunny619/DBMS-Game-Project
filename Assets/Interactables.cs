using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject[] panel;
    public Collider2D[] interactable;


    public void checkinteract()
    {
        for(int i = 0; i<panel.Length;i++)
        {
            if(rb.IsTouching(interactable[i]))
            {
                panel[i].SetActive(true);
                break;
            }
               
        }     
    }

}
