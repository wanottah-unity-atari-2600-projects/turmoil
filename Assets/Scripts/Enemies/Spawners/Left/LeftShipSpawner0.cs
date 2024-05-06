
using UnityEngine;

//
// Turmoil 1982 v2021.02.14
//
// 2021.02.04
//

public class LeftShipSpawner0 : MonoBehaviour
{
    public static LeftShipSpawner0 spawner;

    public Transform[] ship;


    private void Awake()
    {
        spawner = this;
    }


    public void SelectRandomShip(int spawner)
    {
        SpawnerController.spawnerController.leftSpawnerActive[spawner] = true;

        int randomShip = Random.Range(0, SpawnerController.NUMBER_OF_SPAWNABLE_ENEMIES);

        if (SpawnerController.spawnerController.leftSpawnerActive[spawner] || SpawnerController.spawnerController.rightSpawnerActive[spawner])
        {
            ship[randomShip].position = transform.position;

            ship[randomShip].gameObject.SetActive(true);
        }

        SpawnShip(randomShip, spawner);
    }


    private void SpawnShip(int randomShip, int spawner)
    {       
        switch (randomShip)
        {
            case SpawnerController.ENEMY_1: LeftEnemy1Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_2: LeftEnemy2Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_3: LeftEnemy3Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_4: LeftEnemy4Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_5: LeftEnemy5Controller.controller.spawner = spawner; break;
            case SpawnerController.ARROW_SHIP: LeftArrowShipController.controller.spawner = spawner; break;
            case SpawnerController.GHOST_SHIP: LeftGhostShipController.controller.spawner = spawner; break;

            case SpawnerController.PICKUP:

                GameController.gameController.leftPickupActive[spawner] = true;

                GameController.gameController.leftPickupCollected[spawner] = false;

                LeftPickupController.controller.spawner = spawner; 
                
                break;
        }
    }


} // end of class
