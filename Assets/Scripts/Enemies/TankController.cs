
using UnityEngine;

//
// Turmoil 1982 v2021.02.14
//
// 2021.02.04
//

public class TankController : MonoBehaviour
{
    private float tankSpeed;
    private int tankPoints;

    private int direction;

    public bool facingRight;


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
        tankSpeed = Random.Range(0.4f, 0.7f);

        tankPoints = (int)(100 * tankSpeed);

        direction = 0;
    }


    private void MoveTank()
    {
        Vector3 tankPosition = transform.position;

        if (transform.position.x <= GameController.gameController.leftBoundary.position.x)
        {
            direction = 1;

            FlipSprite();

            ChangeDirection();
        }

        if (transform.position.x >= GameController.gameController.rightBoundary.position.x)
        {
            direction = -1;

            FlipSprite();

            ChangeDirection();
        }

        tankPosition.x += tankSpeed * Time.deltaTime;

        transform.position = tankPosition;
    }


    private void ChangeDirection()
    {
        tankSpeed *= -1;
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


    private void DestroyTank()
    {
        gameObject.SetActive(false);

        //GameController.gameController.tankActive = false;
    }


    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.CompareTag("Player 1 Torpedo"))
        {
            DestroyTank();

            GameController.gameController.UpdatePlayer1Score(tankPoints);
        }
    }


} // end of class
