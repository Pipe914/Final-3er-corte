using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStaticEnemy : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float fireRate;
    private Rigidbody2D myBulet;
    

    // Start is called before the first frame update
    void Start()
    {
        myBulet = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myBulet.velocity = myBulet.transform.right * velocidad;
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.hit();
        }
        else
            Destroy(gameObject);

    }
}
