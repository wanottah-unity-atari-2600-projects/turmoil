
using UnityEngine;

//
// Turmoil 1982 v2021.02.14
//
// 2021.02.04
//

public class RightPickupController : MonoBehaviour
{
    public static RightPickupController controller;

    public Transform cannonBall;

    private int pickupPoints;

    [HideInInspector] public int spawner;


    private void Awake()
    {
        controller = this;
    }


    void Update()
    {
        RunPickupTimer();
    }


    private void RunPickupTimer()
    {
        if (!GameController.gameController.rightPickupCollected[spawner])
        {
            GameController.gameController.pickupLifeTime -= Time.deltaTime;

            if (GameController.gameController.pickupLifeTime <= 0)
            {
                TransformToCannonBall();
            }
        }
    }


    private void DisablePickup()
    {
        gameObject.SetActive(false);

        SpawnerController.spawnerController.rightSpawnerActive[spawner] = false;

        GameController.gameController.pickupLifeTime = GameController.PICKUP_LIFE_TIME;
    }


    private void TransformToCannonBall()
    {
        gameObject.SetActive(false);

        GameController.gameController.rightPickupActive[spawner] = false;

        GameController.gameController.pickupLifeTime = GameController.PICKUP_LIFE_TIME;

        cannonBall.position = transform.position;

        cannonBall.gameObject.SetActive(true);

        RightCannonBallController.controller.spawner = spawner;
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            pickupPoints = 10 * Random.Range(GameController.MINIMUM_PICKUP_POINTS, GameController.MAXIMUM_PICKUP_POINTS);

            GameController.gameController.UpdatePlayer1Score(pickupPoints);

            GameController.gameController.rightPickupCollected[spawner] = true;

            DisablePickup();
        }
    }


} // end of class
