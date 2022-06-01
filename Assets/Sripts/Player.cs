using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float fireRate;
    [SerializeField] Transform firepoint;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip jumpClip;

    Rigidbody2D myBody;
    Animator myAnimator;
    float nextFire, shootingDelay, soundDeathTime, restartTime;
    int direcShooting;
    bool isGrounded = true;
    bool iJump = false;
    BoxCollider2D myBox;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBox = GetComponent<BoxCollider2D>();
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.BoxCast(myBox.bounds.center, myBox.bounds.size , 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground", "Enemy"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);

        isGrounded = ray.collider !=  null;

        jump();
        falling();
        fire();

    }
    void fire()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            if (Time.time > nextFire)
            {
                myAnimator.SetLayerWeight(1, 1);
                shootingDelay = Time.time + 0.5f;
                nextFire = Time.time + fireRate;
                if(direcShooting == 1)
                    Instantiate(bullet, firepoint.position, Quaternion.Euler(0,0,0));
                if(direcShooting == -1)
                    Instantiate(bullet, firepoint.position, Quaternion.Euler(0,180,0));
            }
                
        }
        if (Time.time > shootingDelay)
            myAnimator.SetLayerWeight(1, 0);

    }

    void jump()
    {
        if (isGrounded)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                AudioSource.PlayClipAtPoint(jumpClip, myBody.position);

                iJump = true;
            }
            
        }

        if (myBody.velocity.y != 0 && !isGrounded && iJump)
            myAnimator.SetBool("isJumping", true);
        else
            myAnimator.SetBool("isJumping", false);
            iJump = false;
    } 
    
    void falling()
    {
        if (myBody.velocity.y != 0 && !isGrounded && !iJump)
            myAnimator.SetBool("isFalling", true);
        else
            myAnimator.SetBool("isFalling", false);
    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");
        if (dirH > 0)
        {
            transform.localScale = new Vector2(1, 1);
            direcShooting = 1;
            myAnimator.SetBool("isRunning", true);
            myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
        }
        if (dirH < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            direcShooting = -1;
            myAnimator.SetBool("isRunning", true);
            myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
        }
        if (dirH == 0)
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
            myAnimator.SetBool("isRunning", false);
        }

    }
    public void hit()
    {
        StartCoroutine(isDeath());   
    }
    IEnumerator isDeath()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        myAnimator.SetBool("isDeath", true);
        Time.timeScale = 0;
        StartCoroutine(gm.StopMusic());
        yield return new WaitForSecondsRealtime(1f);
        AudioSource.PlayClipAtPoint(deathClip, myBody.position);
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
        StartCoroutine(gm.RestartGame());
        
    }

}
