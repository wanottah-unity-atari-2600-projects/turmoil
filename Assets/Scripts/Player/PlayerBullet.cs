
using UnityEngine;

//
// Turmoil 1982 v2021.02.06
//
// 2021.02.04
//


public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;

    private float bulletSpeed;


    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();

        Initialise();
    }


    private void FixedUpdate()
    {
        MoveBullet();
    }


    private void Initialise()
    {
        bulletSpeed = 2.5f;
    }


    private void MoveBullet()
    {
        bulletRigidbody.velocity = transform.right * bulletSpeed;

        if (transform.position.x > GameController.gameController.rightBoundary.position.x)
        {
            DestroyMissile();
        }

        if (transform.position.x < GameController.gameController.leftBoundary.position.x)
        {
            DestroyMissile();
        }
    }


    private void DestroyMissile()
    {
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DestroyMissile();
        }

        if (collision.CompareTag("Arrow"))
        {
            DestroyMissile();
        }

        if (collision.CompareTag("Tank"))
        {
            DestroyMissile();
        }

        if (collision.CompareTag("Cannon Ball"))
        {
            DestroyMissile();
        }

        if (collision.CompareTag("Dead Enemy"))
        {
            DestroyMissile();
        }
    }


} // end of class
