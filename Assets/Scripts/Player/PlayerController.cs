
using UnityEngine;

//
// Turmoil 1982 v2021.02.14
//
// 2021.02.04
//

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

    public Transform playerShip;
    public Transform[] missileLauncher;
    public Transform[] leftPickup;
    public Transform[] rightPickup;

    private float horizontalSpeed;
    private float verticalMoveDistance;

    [HideInInspector] public int line;

    private float moveCounter;
    private float moveRate;

    public Vector2 startPosition;

    public bool facingRight;

    private float fireCounter;
    private float fireRate;

    private const int TOP_LINE = 0;
    private const int BOTTOM_LINE = 6;


    private void Awake()
    {
        player = this;
    }

 
    void Update()
    {
        CounterController();
        KeyboardController();
    }


    public void Initialise()
    {
        horizontalSpeed = 1f;
        verticalMoveDistance = 0.21f;

        line = 3;

        moveRate = 0.08f;
        moveCounter = 0f;

        fireRate = 0.15f;
        fireCounter = 0f;

        startPosition = new Vector2(0, -0.025f);

        playerShip.position = startPosition;
        
        playerShip.gameObject.SetActive(true);

        facingRight = true;

        LivesController.livesController.UpdateLives(GameController.gameController.player1Lives);
    }


    private void KeyboardController()
    {
        HorizontalMovement();

        VerticalMovement();

        if (Input.GetKey(KeyCode.Space))
        {
            FireMissile();
        }
    }


    private void HorizontalMovement()
    {
        int direction = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;

            FlipSprite(direction);

            RotateMissileLauncher();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;

            FlipSprite(direction);

            RotateMissileLauncher();
        }


        if (GameController.gameController.leftPickupActive[line] || GameController.gameController.rightPickupActive[line])
        {
            playerShip.position += Vector3.right * direction * horizontalSpeed * Time.deltaTime;


            if (GameController.gameController.leftPickupActive[line])
            {
                if (playerShip.position.x < GameController.gameController.leftBoundary.position.x)
                {
                    Vector2 shipPosition = new Vector2(GameController.gameController.leftBoundary.position.x, playerShip.position.y);

                    playerShip.position = shipPosition;
                }

                if (playerShip.position.x > startPosition.x)
                {
                    Vector2 shipPosition = new Vector2(startPosition.x, playerShip.position.y);

                    playerShip.position = shipPosition;

                    if (GameController.gameController.leftPickupCollected[line])
                    {
                        GameController.gameController.leftPickupActive[line] = false;
                        GameController.gameController.leftPickupCollected[line] = false;
                    }
                }
            }


            if (GameController.gameController.rightPickupActive[line])
            {
                if (playerShip.position.x > GameController.gameController.rightBoundary.position.x)
                {
                    Vector2 shipPosition = new Vector2(GameController.gameController.rightBoundary.position.x, playerShip.position.y);

                    playerShip.position = shipPosition;
                }

                if (playerShip.position.x < startPosition.x)
                {
                    Vector2 shipPosition = new Vector2(startPosition.x, playerShip.position.y);

                    playerShip.position = shipPosition;

                    if (GameController.gameController.rightPickupCollected[line])
                    {
                        GameController.gameController.rightPickupActive[line] = false;
                        GameController.gameController.rightPickupCollected[line] = false;
                    }
                }
            }
        }
    }


    private void VerticalMovement()
    {
        if (GameController.gameController.leftPickupActive[line] || GameController.gameController.rightPickupActive[line])
        {
            if (playerShip.position.x != startPosition.x)
            {
                return;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }


    private void FlipSprite(int direction)
    {
        Vector3 transformLocalScale = playerShip.localScale;

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

        playerShip.localScale = localScale;
    }


    private void RotateMissileLauncher()
    {
        if (facingRight)
        {
            missileLauncher[line].eulerAngles = new Vector3(0, 0, 0);
        }

        if (!facingRight)
        {
            missileLauncher[line].eulerAngles = new Vector3(0, 0, 180);
        }
    }


    private void MoveUp()
    {
        if (moveCounter <= 0)
        {
            Vector2 position = playerShip.position;

            position.y += verticalMoveDistance;

            playerShip.position = new Vector2(playerShip.position.x, position.y);

            line -= 1;

            if (line < TOP_LINE)
            {
                line = TOP_LINE;
            }

            if (playerShip.position.y > GameController.gameController.topBoundary.position.y)
            {
                playerShip.position = new Vector2(playerShip.position.x, GameController.gameController.topBoundary.position.y);
            }

            RotateMissileLauncher();

            moveCounter = moveRate;
        }
    }


    private void MoveDown()
    {
        if (moveCounter <= 0)
        {
            Vector2 position = playerShip.position;

            position.y -= verticalMoveDistance;

            playerShip.position = new Vector2(playerShip.position.x, position.y);

            line += 1;

            if (line > BOTTOM_LINE)
            {
                line = BOTTOM_LINE;
            }

            if (playerShip.position.y < GameController.gameController.bottomBoundary.position.y)
            {
                playerShip.position = new Vector2(playerShip.position.x, GameController.gameController.bottomBoundary.position.y);
            }

            RotateMissileLauncher();

            moveCounter = moveRate;
        }
    }


    private void CounterController()
    {
        moveCounter -= Time.deltaTime;
        fireCounter -= Time.deltaTime;
    }


    private void FireMissile()
    {
        if (!GameController.gameController.leftPickupActive[line] && !GameController.gameController.rightPickupActive[line])
        {
            if (fireCounter <= 0f)
            {
                GameObject bullet = MissilePooler.pooler.GetPooledObject();

                if (bullet != null)
                {
                    bullet.transform.position = missileLauncher[line].position;

                    bullet.transform.rotation = missileLauncher[line].rotation;

                    bullet.SetActive(true);
                }

                fireCounter = fireRate;
            }
        }
    }


} // end of class
