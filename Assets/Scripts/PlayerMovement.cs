using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public LayerMask groundLayer;
    public GameObject interactables;
    public float movespeed = 2f; 

    public float jumpforce;
    
    bool moveright1 =false;
    bool moveleft1 =false;
    bool jump = false;

    public Vector2[] Checkpoints;

    public SpriteRenderer playerskin;
    public Sprite[] skins;

    public TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Awake()
    {
        coinsText.text = PlayerPrefs.GetInt("coins",0).ToString();
        playerskin = gameObject.GetComponent<SpriteRenderer>();
        playerskin.sprite = skins[PlayerPrefs.GetInt("current_skin")];
    }
    void Start()
    {
        
        rb= gameObject.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {    
        if(Input.GetKey(KeyCode.RightArrow)||moveright1)
            rb.velocity = new Vector2(movespeed,rb.velocity.y);
        else if(Input.GetKey(KeyCode.LeftArrow)||moveleft1)
            rb.velocity = new Vector2(-movespeed,rb.velocity.y);
        else if(!moveright1)
            rb.velocity = new Vector2(0,rb.velocity.y);
        else if(!moveleft1)
            rb.velocity = new Vector2(0,rb.velocity.y);    
        if(Input.GetKey(KeyCode.Space))
            singlejump();
        if(jump)
            {
                rb.velocity = new Vector2(rb.velocity.x,jumpforce);
                jump =false;
            }
        if(Input.GetKey(KeyCode.RightControl))
        {
            Interactables.I.checkinteract();
           //interactables.GetComponent<Interactables>.
        }
        
    }
    public void moveright()
    {
        Debug.Log("button pressed");
        moveright1 = true;
    }
    public void notmoveright()
    {
        moveright1 = false;
    }
    public void moveleft()
    {
        moveleft1 = true;
    }
    public void notmoveleft()
    {
        moveleft1 = false;
    }
    public void singlejump()
    {
        if(Physics2D.OverlapBox(transform.position,new Vector2(0.6f,0.6f),transform.rotation.z,groundLayer))
            jump = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag[0] =='C')
            transform.position = Checkpoints[col.gameObject.tag[col.gameObject.tag.Length-1]-'0'-1];          
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("coin"))
        {
            Destroy(col.gameObject);
            CoinAdded();
        }
    }
    void CoinAdded()
    {
        PlayerPrefs.SetInt("coins",PlayerPrefs.GetInt("coins",0)+50);
        coinsText.text = PlayerPrefs.GetInt("coins",0).ToString();
        GameManager.instance.UpdateDBCoins(PlayerPrefs.GetInt("coins",0));
    }
}
