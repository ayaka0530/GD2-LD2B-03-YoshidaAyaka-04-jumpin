using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Result : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;

    public Text enemyScoreText;
    public Text distanceScoreText;
    public Text scoreText;

    public float enemyScore;
    public float distance;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        enemyScore = gameManager.TeachEnemyScore();
        enemyScoreText.text = "" + enemyScore;

        distance = gameManager.TeachDistance();
        //distanceScoreText.text = "" + distance;
        distanceScoreText.text = string.Format("{0:f0}", distance);//è¨êîì_ÇÃêßå¿


        score = enemyScore * distance;
        scoreText.text = string.Format("{0:f0}", score);//è¨êîì_ÇÃêßå¿
    }

    public void result()
    {

    }
}
