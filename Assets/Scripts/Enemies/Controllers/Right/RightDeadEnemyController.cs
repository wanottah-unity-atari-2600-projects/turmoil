
using UnityEngine;

//
// Turmoil 1982 v2021.02.13
//
// 2021.02.04
//

public class RightDeadEnemyController : MonoBehaviour
{
    public static RightDeadEnemyController controller;

    private float shipSpeed;

    [HideInInspector] public int spawner;


    private void Awake()
    {
        controller = this;
    }


    private void Start()
    {
        Initialise();
    }


    void Update()
    {
        MoveShip();
    }


    private void Initialise()
    {
        shipSpeed = Random.Range(GameController.MINIMUM_ENEMY_SPEED, GameController.MAXIMUM_ENEMY_SPEED);

        if (transform.position.x < 0)
        {
            ChangeDirection();
        }
    }


    private void MoveShip()
    {
        Vector3 enemyShip = transform.position;
        
        enemyShip.x += shipSpeed * Time.deltaTime;

        transform.position = enemyShip;

        if (transform.position.x < GameController.gameController.leftBoundary.position.x)
        {
            DisableEnemyShip();
        }

        if (transform.position.x > GameController.gameController.rightBoundary.position.x)
        {
            DisableEnemyShip();
        }
    }


    private void ResetShipPosition()
    {
        gameObject.SetActive(false);

        Vector3 enemyShip = transform.position;

        enemyShip.x = SpawnerController.spawnerController.rightSpawner[spawner].position.x;

        transform.position = enemyShip;
    }


    private void ChangeDirection()
    {
        shipSpeed *= -1;
    }


    private void DisableEnemyShip()
    {
        ResetShipPosition();

        SpawnerController.spawnerController.rightSpawnerActive[spawner] = false;
    }


} // end of class
