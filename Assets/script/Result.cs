using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Result : MonoBehaviour
{
    GameManager gameManager;
    public Text textComponent;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManger").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        score = gameManager.TeachScore();
        textComponent.text = "SCORE: " + score;
    }

    public void result()
    {

    }
}
