using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public Dir direction;
    public Vector2 Direc;
    public float speed = 1.0f;
    private Rigidbody2D rb2d;

    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        SetDirection();
        
    }


    void Update()
    {
        rb2d.velocity = Direc * speed;
        transform.Translate(Direc.x * Time.deltaTime, Direc.y * Time.deltaTime, 0);
    }
    public void RandomDirection(int range)
    {
        
        
    }

    public void SetDirection()
    {
        switch(direction)
        {
            case Dir.up:
                Direc = new Vector3(0, 1, 0);
                break;
            case Dir.down:
                Direc = new Vector3(0, -1, 0);
                break;
            case Dir.left:
                Direc = new Vector3(-1, 0, 0);
                break;
            default:
                Direc = new Vector3(1, 0, 0);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Node"))
        {
            
            
            for (int i = 0; i < collision.GetComponent<Node>().dir.Length; i++)
            {
                transform.position = collision.gameObject.transform.position;
                var rand = Random.Range(0, collision.GetComponent<Node>().dir.Length);
                if (rand == 0)
                {
                    direction = collision.GetComponent<Node>().dir[0];
                }
                else if (rand == 1)
                {
                    direction = collision.GetComponent<Node>().dir[1];
                }
                else if (rand == 2)
                {
                    direction = collision.GetComponent<Node>().dir[2];
                }
                else if (rand == 3)
                {
                    direction = collision.GetComponent<Node>().dir[3];
                }
                SetDirection();
                
            }
            
        }
    }
}


// transform.poosition = new Vector 3(x, y,z);