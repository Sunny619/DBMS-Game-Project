using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    float speed,maxY,minY; 
    void Update()
    {
            move();     
    }
    void move()
    {
        Vector2 temp = transform.position;
        temp.y += speed*Time.deltaTime;
        transform.position = temp;
        if(transform.position.y>maxY)
        {
            transform.position = new Vector2 (transform.position.x , minY);
        } 
    }

}
