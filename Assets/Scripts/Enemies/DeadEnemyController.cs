
using UnityEngine;

//
// Turmoil 1982 v2021.02.08
//
// 2021.02.04
//

public class DeadEnemyController : MonoBehaviour
{
    private float deadEnemySpeed;


    private void Start()
    {
        Initialise();
    }


    void Update()
    {
        MoveDeadEnemy();
    }


    private void Initialise()
    {
        deadEnemySpeed = Random.Range(0.4f, 0.7f);

        if (transform.position.x < PlayerController.player.transform.position.x)
        {
            deadEnemySpeed = -deadEnemySpeed;
        }
    }


    private void MoveDeadEnemy()
    {
        Vector3 deadEnemyPosition = transform.position;

        if (transform.position.x <= GameController.gameController.leftBoundary.position.x)
        {
            DisableDeadEnemy();
        }

        if (transform.position.x >= GameController.gameController.rightBoundary.position.x)
        {
            DisableDeadEnemy();
        }

        deadEnemyPosition.x += deadEnemySpeed * Time.deltaTime;

        transform.position = deadEnemyPosition;
    }


    private void DisableDeadEnemy()
    {
        gameObject.SetActive(false);
    }


} // end of class
