
using UnityEngine;

//
// Turmoil 1982 v2021.02.09
//
// 2021.02.04
//

public class Enemy5Controller : MonoBehaviour
{
    private float enemySpeed;
    private int enemyPoints;

    public bool movingRight;


    private void Start()
    {
        Initialise();
    }


    void Update()
    {
        MoveEnemy();
    }


    private void Initialise()
    {
        enemySpeed = Random.Range(0.4f, 0.7f);

        enemyPoints = (int)(100 * enemySpeed);
    }


    private void MoveEnemy()
    {
        Vector3 enemyShip = transform.position;

        if (movingRight)
        {
            enemyShip.x += enemySpeed * Time.deltaTime;
        }

        else
        {
            enemyShip.x -= enemySpeed * Time.deltaTime;
        }

        transform.position = enemyShip;

        if (movingRight)
        {
            if (transform.position.x > GameController.gameController.rightBoundary.position.x)
            {
                DestroyEnemy();
            }
        }

        else
        {
            if (transform.position.x < GameController.gameController.leftBoundary.position.x)
            {
                DestroyEnemy();
            }

        }
    }


    private void DestroyEnemy()
    {
        gameObject.SetActive(false);

        //LeftSpawnerLine0.spawner.DisableLine(0);
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player 1 Torpedo"))
        {
            GameController.gameController.UpdatePlayer1Score(enemyPoints);

            DestroyEnemy();
        }
    }


} // end of class
