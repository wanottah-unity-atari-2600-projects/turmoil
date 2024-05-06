
using UnityEngine;

//
// Turmoil 1982 v2021.02.14
//
// 2021.02.04
//

public class SpawnerController : MonoBehaviour
{
    public static SpawnerController spawnerController;

    public Transform[] leftSpawner;
    public Transform[] rightSpawner;

    public bool[] leftSpawnerActive;
    public bool[] rightSpawnerActive;

    private float spawnTimer;

    public const int NUMBER_OF_SPAWNERS = 7;
    public const int NUMBER_OF_ENEMIES = 11;
    public const int NUMBER_OF_SPAWNABLE_ENEMIES = 8;

    public const int LEFT_SPAWNER = 1;

    public const int ENEMY_1 = 0;
    public const int ENEMY_2 = 1;
    public const int ENEMY_3 = 2;
    public const int ENEMY_4 = 3;
    public const int ENEMY_5 = 4;
    public const int ARROW_SHIP = 5;
    public const int GHOST_SHIP = 6;
    public const int PICKUP = 7;
    public const int TANK = 8;
    public const int CANNON_BALL = 9;
    public const int DEAD_ENEMY = 10;


    private void Awake()
    {
        spawnerController = this;
    }


    void Update()
    {
        RunSpawnTimer();
    }


    private void RunSpawnTimer()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SelectSpawner();

            spawnTimer = GameController.SPAWN_DELAY_TIMER;
        }
    }


    private void SelectSpawner()
    {
        // select a random line to spawn enemy
        int randomSpawner = Random.Range(0, NUMBER_OF_SPAWNERS);
        
        // if the spawners are not already active
        if (!leftSpawnerActive[randomSpawner] && !rightSpawnerActive[randomSpawner])
        {
            // 1 - 2
            int randomSide = Random.Range(1, 3);
            
            if (randomSide == LEFT_SPAWNER)
            {
                ActivateLeftSpawner(randomSpawner);
            }

            else
            {
                ActivateRightSpawner(randomSpawner);
            }
        }
    }


    private void ActivateLeftSpawner(int spawner)
    {              
        switch (spawner)
        {
            case 0: LeftShipSpawner0.spawner.SelectRandomShip(spawner); break;
            case 1: LeftShipSpawner1.spawner.SelectRandomShip(spawner); break;
            case 2: LeftShipSpawner2.spawner.SelectRandomShip(spawner); break;
            case 3: LeftShipSpawner3.spawner.SelectRandomShip(spawner); break;
            case 4: LeftShipSpawner4.spawner.SelectRandomShip(spawner); break;
            case 5: LeftShipSpawner5.spawner.SelectRandomShip(spawner); break;
            case 6: LeftShipSpawner6.spawner.SelectRandomShip(spawner); break;
        }
    }


    private void ActivateRightSpawner(int spawner)
    {
        switch (spawner)
        {
            case 0: RightShipSpawner0.spawner.SelectRandomShip(spawner); break;
            case 1: RightShipSpawner1.spawner.SelectRandomShip(spawner); break;
            case 2: RightShipSpawner2.spawner.SelectRandomShip(spawner); break;
            case 3: RightShipSpawner3.spawner.SelectRandomShip(spawner); break;
            case 4: RightShipSpawner4.spawner.SelectRandomShip(spawner); break;
            case 5: RightShipSpawner5.spawner.SelectRandomShip(spawner); break;
            case 6: RightShipSpawner6.spawner.SelectRandomShip(spawner); break;
        }
    }


} // end of class
