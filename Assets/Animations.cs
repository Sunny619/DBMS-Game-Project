using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator[] anim;
    public void playanim(int i)
    {
        anim[i].Play("1");
    }
}
