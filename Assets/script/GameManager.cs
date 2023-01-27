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
    private int scoreCount;
    public Text textComponent;

    public GameObject enemy1Prefab;
    //public float endTimeCount = 5;


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(640, 1024, false);
        Application.targetFrameRate = 60;
        enemyRespawnList = new List<EnemyRespawn>();
        DontDestroyOnLoad(gameObject);//�V�[�����ς���Ă������Ȃ��Ȃ�
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
        //�^�C�}�[���O�ȉ��ɂȂ�v�f��S�č폜
        enemyRespawnList.RemoveAll(t => t.timer <= 0);

        //60�b�o�����烊�U���g��ʂɔ�΂��֐����Ăяo��
        Invoke("ChangeScene", 5.0f);
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
    public int TeachScore()
    {
        return scoreCount;
    }
}
