using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject player;
    [SerializeField] int healt;
    [SerializeField] float alcance;
    [SerializeField] float fireRate;
    [SerializeField] Transform firePosition;
    [SerializeField] AudioClip deathClipEnemy;
    private Animator myAnimator;
    private BoxCollider2D myCollider;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D shooting =  Physics2D.Raycast(transform.position, transform.right, alcance,LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, transform.right * alcance, Color.blue);

        if (shooting.collider != null && healt > 0)
        {
            shoot();
        }
        StartCoroutine(isDeath());
    }
    public void hit(int damage)
    {
        healt -= damage;
        if (healt <= 0)
        {
            
            myAnimator.SetBool("isDeath", true);
        }
    }

    IEnumerator isDeath()
    {

        if (healt <= 0)
        {
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Destruction Enemy") && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                myAnimator.SetBool("destroyed", true);
                myCollider.enabled = false;
                transform.position = new Vector2(transform.position.x, transform.position.y - .5f);
                StartCoroutine(FindObjectOfType<GameManager>().countStaticEnemys());
                AudioSource.PlayClipAtPoint(deathClipEnemy, player.transform.position);

                yield return null;

            }
        }
    }

    void shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Bullet, firePosition.position, transform.rotation);
        }

    }
}
