using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody ballRb;

    private bool ignoreNextCollision;
    private bool ignoreNextScore;

    private readonly int forceImpulse = 6;
    static public int score;
    static public int bestScore;

    [SerializeField] private GameObject sparyy;
    GameObject[] tempObject = new GameObject[7];

    GameManager gameManager;

    void Start()
    {
        score = 0;
        gameManager = FindObjectOfType<GameManager>();
        ballRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            CatchCircles(collision);
        }

        string objectTag = collision.gameObject.tag;

        if (ignoreNextCollision)
            return;


        switch (objectTag)
        {
            //Topun çarpmasý gereken renge çarpýnca olacaklar.
            case "FriendRing":
                ballRb.AddForce(Vector3.up * forceImpulse, ForceMode.Impulse);
                GameObject tempObject = Instantiate(sparyy, new Vector3(transform.position.x, transform.position.y - 0.120f, transform.position.z), Quaternion.Euler(-90, 0f, 0f));
                tempObject.transform.SetParent(collision.transform);
                gameManager.bounceSound.Play();
                Handheld.Vibrate();
                break;
            //Topun çarpmamasý gereken renge çarpýnca olacaklar.
            case "EnemyRing":
                ballRb.AddForce(Vector3.zero);
                ignoreNextScore = true;
                gameManager.failSound.Play();
                GameFinished(true);
                break;
            //Bitiþ dairesine gelince olacaklar.
            case "Finish":
                ignoreNextScore = true;
                gameManager.bounceSound.Play();
                GameFinished(ignoreNextScore);
                break;
            default:
                break;
        }

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (ignoreNextScore)
        {
            return;
        }

        DropCircles();
        score++;
        gameManager.scoreIncreaseSound.Play();
        Handheld.Vibrate();
        gameManager.ScoreWrite();
    }


    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    private void GameFinished(bool isFailed)//----- Oyun bitince olacaklar
    {
        if (isFailed)
        {
            ballRb.AddForce(Vector3.zero);
            Time.timeScale = 0;
            if (score >= bestScore)
            {
                bestScore = score;
            }
            else
            {
                bestScore = PlayerPrefs.GetInt("Skorum");
            }
            gameManager.BestScore();
        }

    }

    private void CatchCircles(Collision collis) // Platformdaki tüm objeleri yakalýyor.
    {
        for (int i = 0; i < tempObject.Length; i++)
        {
            GameObject myTemp = tempObject[i] = collis.transform.parent.GetChild(i).gameObject;
        }
    }

    private void DropCircles() // Platformdaki yakalanan tüm objelere Rb atayýp daðýtýyor.
    {
        for (int i = 0; i < tempObject.Length; i++)
        {
            if (tempObject[i].gameObject != null && !tempObject[i].GetComponent<Rigidbody>())
            {
                Rigidbody tempRb = tempObject[i].AddComponent<Rigidbody>().GetComponent<Rigidbody>();
                tempRb.AddExplosionForce(10, transform.position, 20, .01f, ForceMode.Impulse);
                tempRb.constraints = RigidbodyConstraints.FreezePositionY;
            }
            
            Destroy(tempObject[1].transform.parent.gameObject, .7f);
        }
    }
}
