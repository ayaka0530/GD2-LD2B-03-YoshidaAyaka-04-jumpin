using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public class EnemyRespawn
    {
        public float timer;
        public Vector3 pos;
        public EnemyRespawn(float timer, Vector3 pos)
        {
            this.timer = timer;
            this.pos = pos;
        }
    };

    List<EnemyRespawn> enemyRespawnList;

    public int jumpCount;
    private float scoreCount;
    public Text limitTextComponent;
    private float limitCount = 30;
    public float maxPlayerDistance;

    public GameObject enemy1Prefab;

    //public float endTimeCount = 5;


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(640, 1024, false);
        Application.targetFrameRate = 60;
        enemyRespawnList = new List<EnemyRespawn>();
        DontDestroyOnLoad(gameObject);//シーンが変わっても消えなくなる


        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            //30秒経ったらリザルト画面に飛ばす関数を呼び出す
            Invoke("ChangeScene", 30.0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //endTimeCount -= Time.deltaTime;

        for (int i = 0; i < enemyRespawnList.Count; i++)
        {
            enemyRespawnList[i].timer -= Time.deltaTime;

            if (enemyRespawnList[i].timer <= 0)
            {
                Instantiate(enemy1Prefab, enemyRespawnList[i].pos, Quaternion.identity);
            }
        }
        //タイマーが０以下になる要素を全て削除
        enemyRespawnList.RemoveAll(t => t.timer <= 0);

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            limitCount -= Time.deltaTime;
            limitTextComponent.text = string.Format("{0:f0}",limitCount);//小数点の制限
        }

    }
    public void AddScoreCount()
    {
        scoreCount = scoreCount + 100;
        Debug.Log("scoreCount:" + scoreCount);
        //textComponent.text = "SCORE: " + scoreCount;
    }

    public void EnemyDead(Vector3 posision)
    {
        enemyRespawnList.Add(new EnemyRespawn(3, posision));
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Result");
    }
    public void NextScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
    public float TeachEnemyScore()
    {
        return scoreCount;
    }
    public float TeachDistance()
    {
        return maxPlayerDistance;
    }

    public void SetDistance(float distance)
    {
        if (distance > maxPlayerDistance)
        {
            maxPlayerDistance = distance;
        }
    }
}
