﻿
using UnityEngine;

//
// Turmoil 1982 v2021.02.13
//
// 2021.02.04
//

public class RightArrowShipController : MonoBehaviour
{
    public static RightArrowShipController controller;

    public Transform deadEnemy;
    public Transform tank;

    private float shipSpeed;
    private int shipPoints;

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
        shipPoints = 10 * Random.Range(GameController.MINIMUM_ENEMY_POINTS, GameController.MAXIMUM_ENEMY_POINTS);
    }


    private void MoveShip()
    {
        Vector3 enemyShip = transform.position;
        
        enemyShip.x -= shipSpeed * Time.deltaTime;

        transform.position = enemyShip;

        if (transform.position.x < GameController.gameController.leftBoundary.position.x)
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


    private void DisableEnemyShip()
    {
        TransformEnemy();
    }


    private void DestroyEnemyShip()
    {
        deadEnemy.position = transform.position;

        ResetShipPosition();

        deadEnemy.gameObject.SetActive(true);

        RightDeadEnemyController.controller.spawner = spawner;
    }


    private void TransformEnemy()
    {
        tank.position = transform.position;

        ResetShipPosition();

        tank.gameObject.SetActive(true);

        RightTankController.controller.spawner = spawner;
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
            GameController.gameController.UpdatePlayer1Score(shipPoints);

            DestroyEnemyShip();
        }
    }


} // end of class