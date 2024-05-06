
using UnityEngine;

//
// Turmoil 1982 v2021.02.05
//
// 2021.02.04
//

public class LivesController : MonoBehaviour
{
    public static LivesController livesController;

    public Transform[] lives;


    private void Awake()
    {
        livesController = this;
    }


    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < lives.Length; i++)
        {
            if (i + 1 == livesRemaining - 1)
            {
                lives[i].gameObject.SetActive(true);
            }
        }
    }


} // end of class
