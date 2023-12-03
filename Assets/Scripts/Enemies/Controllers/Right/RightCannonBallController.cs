
using UnityEngine;

//
// Turmoil 1982 v2021.02.13
//
// 2021.02.04
//

public class RightCannonBallController : MonoBehaviour
{
    public static RightCannonBallController controller;

    public Transform deadEnemy;

    private float cannonBallSpeed;
    private int cannonBallPoints;

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
        MoveCannonBall();
    }


    private void Initialise()
    {
        cannonBallSpeed = GameController.CANNON_BALL_SPEED;
        cannonBallPoints = 10 * Random.Range(GameController.MINIMUM_CANNON_BALL_POINTS, GameController.MAXIMUM_CANNON_BALL_POINTS);
    }


    private void MoveCannonBall()
    {
        Vector3 cannonBallPosition = transform.position;

        if (transform.position.x < GameController.gameController.leftBoundary.position.x)
        {
            cannonBallPosition.x = GameController.gameController.leftBoundary.position.x;

            ChangeDirection();
        }

        if (transform.position.x > GameController.gameController.rightBoundary.position.x)
        {
            cannonBallPosition.x = GameController.gameController.rightBoundary.position.x;

            ChangeDirection();
        }

        cannonBallPosition.x += cannonBallSpeed * Time.deltaTime;

        transform.position = cannonBallPosition;
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
        cannonBallSpeed *= -1;
    }


    private void DestroyCannonBall()
    {
        deadEnemy.position = transform.position;

        ResetShipPosition();

        deadEnemy.gameObject.SetActive(true);

        RightDeadEnemyController.controller.spawner = spawner;
    }


    private void DestroyPlayerShip()
    {
        ResetShipPosition();

        GameController.gameController.PlayerDestroyed();

        SpawnerController.spawnerController.rightSpawnerActive[spawner] = false;
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            DestroyPlayerShip();
        }

        if (target.CompareTag("Player 1 Torpedo"))
        {
            DestroyCannonBall();

            GameController.gameController.UpdatePlayer1Score(cannonBallPoints);
        }
    }


} // end of class
