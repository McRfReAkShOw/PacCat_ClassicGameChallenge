using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{


    public Animator Anim;
    public Game_Controller gameController;
    public Vector2 Direc;
    public AudioClip hitSound;
    public AudioClip pointBoostSound;
    


    public Dir direction;

    public bool lookingRight;
    public bool restart;
    public bool isPointBoost;

    public float speed = 1.0f;
    public float cloudCount;

    private Rigidbody2D rb2d;
    private AudioSource source;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        rb2d.velocity = new Vector2(0, 0);
        source = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            direction = Dir.left;
            Direc = new Vector3(-1, 0, 0);
            Anim.SetInteger("State", 1);

        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            direction = Dir.right;
            Direc = new Vector3(1, 0, 0);
            Anim.SetInteger("State", 0);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            direction = Dir.down;
            Anim.SetInteger("State", 2);
            Direc = new Vector3(0, -1, 0);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            direction = Dir.up;
            Direc = new Vector3(0, 1, 0);
            Anim.SetInteger("State", 3);
        }

        rb2d.velocity = Direc * speed;
        transform.Translate(Direc.x * Time.deltaTime * speed, Direc.y * Time.deltaTime * speed, 0);


    }
    void Flip()
    {
        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }

    public void ResetPlayer()
    {
        //gameObject.SetActive(false);
        if(gameController.GetComponent<Game_Controller>().isStage2 == true)
        {
            transform.position = gameController.GetComponent<Game_Controller>().level2Spawn.position;
        }
        else
        {
            transform.position = new Vector3(15, -15, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameController.GetComponent<Game_Controller>().EnemyHit();
            ResetPlayer();
            source.PlayOneShot(hitSound, 1f);
        }
        if (collision.gameObject.CompareTag("Pickup"))
        {
            gameController.GetComponent<Game_Controller>().AddScore(1);
            Destroy(collision.gameObject);

        }
        if(collision.CompareTag("Candy"))
        {
            gameController.GetComponent<Game_Controller>().AddScore(5);
            Destroy(collision.gameObject);
            source.PlayOneShot(pointBoostSound,1f);
        }
        
       
    }

    
}
    

