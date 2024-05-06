
using System.Collections;
using UnityEngine;

//
// Turmoil 1982 v2021.02.13
//
// 2021.02.04
//

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    public Transform leftBoundary;
    public Transform rightBoundary;
    public Transform topBoundary;
    public Transform bottomBoundary;

    public Transform gameOverText;

    [HideInInspector] public int player1Lives;

    private int player1Score;
    private int highScore;

    //[HideInInspector] public bool tankActive;
    //[HideInInspector] public bool cannonBallActive;

    public bool[] leftPickupActive;
    public bool[] leftPickupCollected;

    public bool[] rightPickupActive;
    public bool[] rightPickupCollected;

    public bool[] leftCannonBallActive;
    public bool[] rightCannonBallActive;

    [HideInInspector] public bool canPlay;
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool highScoreSet;

    public float pickupLifeTime;

    public const float SPAWN_DELAY_TIMER = 1.2f;

    public const float MINIMUM_ENEMY_SPEED = 0.5f;
    public const float MAXIMUM_ENEMY_SPEED = 0.9f;
    public const int MINIMUM_ENEMY_POINTS = 2;
    public const int MAXIMUM_ENEMY_POINTS = 11;

    public const float CANNON_BALL_SPEED = 2.5f;
    public const int MINIMUM_CANNON_BALL_POINTS = 50;
    public const int MAXIMUM_CANNON_BALL_POINTS = 81;

    public const float PICKUP_LIFE_TIME = 7f;
    public const int MINIMUM_PICKUP_POINTS = 50;
    public const int MAXIMUM_PICKUP_POINTS = 110;



    private void Awake()
    {
        gameController = this;
    }


    void Start()
    {
        StartUp();
    }


    void Update()
    {
        GameLoop();
    }


    private void StartUp()
    {
        SpawnerController.spawnerController.leftSpawnerActive = new bool[SpawnerController.NUMBER_OF_SPAWNERS];
        SpawnerController.spawnerController.rightSpawnerActive = new bool[SpawnerController.NUMBER_OF_SPAWNERS];

        leftPickupActive = new bool[SpawnerController.NUMBER_OF_SPAWNERS];
        leftPickupCollected = new bool[SpawnerController.NUMBER_OF_SPAWNERS];

        rightPickupActive = new bool[SpawnerController.NUMBER_OF_SPAWNERS];
        rightPickupCollected = new bool[SpawnerController.NUMBER_OF_SPAWNERS];

        leftCannonBallActive = new bool[SpawnerController.NUMBER_OF_SPAWNERS];
        rightCannonBallActive = new bool[SpawnerController.NUMBER_OF_SPAWNERS];

        canPlay = false;
        gameOver = true;
        highScoreSet = false;

        DisableEnemy();
      
        PlayerController.player.playerShip.gameObject.SetActive(false);

        player1Score = 0;
        player1Lives = 0;
        highScore = 0;

        ScoreController.scoreController.InitialiseScores();
        LivesController.livesController.UpdateLives(player1Lives);

        gameOverText.gameObject.SetActive(true);
    }


    private void DisableEnemy()
    {
        pickupLifeTime = PICKUP_LIFE_TIME;

        for (int i = 0; i < SpawnerController.NUMBER_OF_SPAWNERS; i++)
        {
            SpawnerController.spawnerController.leftSpawnerActive[i] = false;
            SpawnerController.spawnerController.rightSpawnerActive[i] = false;
        }

        for (int i = 0; i < SpawnerController.NUMBER_OF_ENEMIES; i++)
        {
            LeftShipSpawner0.spawner.ship[i].gameObject.SetActive(false);
            LeftShipSpawner1.spawner.ship[i].gameObject.SetActive(false);
            LeftShipSpawner2.spawner.ship[i].gameObject.SetActive(false);
            LeftShipSpawner3.spawner.ship[i].gameObject.SetActive(false);
            LeftShipSpawner4.spawner.ship[i].gameObject.SetActive(false);
            LeftShipSpawner5.spawner.ship[i].gameObject.SetActive(false);
            LeftShipSpawner6.spawner.ship[i].gameObject.SetActive(false);

            RightShipSpawner0.spawner.ship[i].gameObject.SetActive(false);
            RightShipSpawner1.spawner.ship[i].gameObject.SetActive(false);
            RightShipSpawner2.spawner.ship[i].gameObject.SetActive(false);
            RightShipSpawner3.spawner.ship[i].gameObject.SetActive(false);
            RightShipSpawner4.spawner.ship[i].gameObject.SetActive(false);
            RightShipSpawner5.spawner.ship[i].gameObject.SetActive(false);
            RightShipSpawner6.spawner.ship[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < SpawnerController.NUMBER_OF_SPAWNERS; i++)
        {
            leftPickupActive[i] = false;
            leftPickupCollected[i] = false;
            leftCannonBallActive[i] = false;

            rightPickupActive[i] = false;
            rightPickupCollected[i] = false;
            rightCannonBallActive[i] = false;
        }
    }


    private void Initialise()
    {
        DisableEnemy();

        player1Score = 0;
        player1Lives = 5;

        ScoreController.scoreController.InitialiseScores();

        PlayerController.player.Initialise();

        gameOverText.gameObject.SetActive(false);

        gameOver = false;

        canPlay = true;
    }


    private void GameLoop()
    {
        if (gameOver)
        {
            KeyboardController();
        }
    }


    private void KeyboardController()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartOnePlayer();
        }
    }


    private void StartOnePlayer()
    {
        Initialise();
    }


    public void PlayerDestroyed()
    {
        PlayerController.player.playerShip.gameObject.SetActive(false);

        UpdatePlayer1Lives();
    }


    public void UpdatePlayer1Score(int points)
    {
        player1Score += points;

        ScoreController.scoreController.UpdateScoreDisplay(player1Score, ScoreController.PLAYER_1);
    }


    private void UpdatePlayer1Lives()
    {
        player1Lives -= 1;

        if (player1Lives == 0)
        {
            canPlay = false;

            gameOver = true;

            gameOverText.gameObject.SetActive(true);

            UpdateHighScore();
        }

        else
        {
            LivesController.livesController.UpdateLives(player1Lives);

            StartCoroutine(PlayerRespawnDelay());           
        }
    }


    private IEnumerator PlayerRespawnDelay()
    {
        yield return new WaitForSeconds(1f);

        Vector2 shipPosition = PlayerController.player.playerShip.position;

        shipPosition.x = PlayerController.player.startPosition.x;
        shipPosition.y = PlayerController.player.playerShip.position.y;

        PlayerController.player.playerShip.position = shipPosition;

        PlayerController.player.playerShip.gameObject.SetActive(true);
    }


    private void UpdateHighScore()
    {
        if (player1Score > highScore)
        {
            highScore = player1Score;
        }

        ScoreController.scoreController.UpdateScoreDisplay(highScore, ScoreController.HIGH_SCORE);

        highScoreSet = true;
    }


} // end of class
