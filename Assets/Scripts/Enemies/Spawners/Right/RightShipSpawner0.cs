
using UnityEngine;

//
// Turmoil 1982 v2021.02.14
//
// 2021.02.04
//

public class RightShipSpawner0 : MonoBehaviour
{
    public static RightShipSpawner0 spawner;

    public Transform[] ship;


    private void Awake()
    {
        spawner = this;
    }


    public void SelectRandomShip(int spawner)
    {
        SpawnerController.spawnerController.rightSpawnerActive[spawner] = true;

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
            case SpawnerController.ENEMY_1: RightEnemy1Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_2: RightEnemy2Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_3: RightEnemy3Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_4: RightEnemy4Controller.controller.spawner = spawner; break;
            case SpawnerController.ENEMY_5: RightEnemy5Controller.controller.spawner = spawner; break;
            case SpawnerController.ARROW_SHIP: RightArrowShipController.controller.spawner = spawner; break;
            case SpawnerController.GHOST_SHIP: RightGhostShipController.controller.spawner = spawner; break;

            case SpawnerController.PICKUP:

                GameController.gameController.rightPickupActive[spawner] = true;

                GameController.gameController.rightPickupCollected[spawner] = false;

                RightPickupController.controller.spawner = spawner; 
                
                break;
        }
    }


} // end of class
