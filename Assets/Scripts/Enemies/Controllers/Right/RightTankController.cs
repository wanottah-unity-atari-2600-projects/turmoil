
using UnityEngine;

//
// Turmoil 1982 v2021.02.13
//
// 2021.02.04
//

public class RightTankController : MonoBehaviour
{
    public static RightTankController controller;

    public Transform deadEnemy;
    public Transform playerBullet;

    private float tankSpeed;
    private int tankPoints;

    private int direction;
    public bool facingRight;

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
        MoveTank();
    }


    private void Initialise()
    {
        tankSpeed = Random.Range(GameController.MINIMUM_ENEMY_SPEED, GameController.MAXIMUM_ENEMY_SPEED);
        tankPoints = 10 * Random.Range(GameController.MINIMUM_ENEMY_POINTS, GameController.MAXIMUM_ENEMY_POINTS);
    }


    private void MoveTank()
    {
        Vector3 tankPosition = transform.position;

        if (tankPosition.x < GameController.gameController.leftBoundary.position.x)
        {
            tankPosition.x = GameController.gameController.leftBoundary.position.x;

            direction = 1;

            FlipSprite();

            ChangeDirection();
        }

        if (tankPosition.x > GameController.gameController.rightBoundary.position.x)
        {
            tankPosition.x = GameController.gameController.rightBoundary.position.x;

            direction = -1;

            FlipSprite();

            ChangeDirection();
        }

        tankPosition.x -= tankSpeed * Time.deltaTime;

        transform.position = tankPosition;
    }


    private void ChangeDirection()
    {
        tankSpeed *= -1;
    }


    private void ResetShipPosition()
    {
        gameObject.SetActive(false);

        Vector3 enemyShip = transform.position;

        enemyShip.x = SpawnerController.spawnerController.rightSpawner[spawner].position.x;

        transform.position = enemyShip;
    }


    private void DestroyTank()
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


    private void FlipSprite()
    {
        Vector3 transformLocalScale = transform.localScale;

        if (direction > 0 && !facingRight)
        {
            Flip(transformLocalScale);
        }

        if (direction < 0 && facingRight)
        {
            Flip(transformLocalScale);
        }
    }


    private void Flip(Vector3 localScale)
    {
        facingRight = !facingRight;

        localScale.x *= -1;

        transform.localScale = localScale;
    }



    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player 1 Torpedo"))
        {
            GameController.gameController.UpdatePlayer1Score(tankPoints);

            DestroyTank();
        }
    }


    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.CompareTag("Player"))
        {
            DestroyPlayerShip();
        }

        if (target.collider.CompareTag("Player 1 Torpedo"))
        {
            Debug.Log("collision");
            playerBullet.gameObject.SetActive(false);
            // knock back
        }
    }


} // end of class
