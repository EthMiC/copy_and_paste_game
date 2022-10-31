using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Transform enemy;
    [SerializeField]
    Text scoretext;

    public bool alive = true;
    [SerializeField]
    float TopSpawn;
    [SerializeField]
    float BottomSpawn;
    [SerializeField]
    float initTimer;
    [SerializeField]
    float timerCap;
    [SerializeField]
    float timerDescent;
    float timerTo;
    float timer;
    float score;


    // Start is called before the first frame update
    void Start()
    {
        timerTo = initTimer;
        timer = timerTo;
        var position = new Vector3(40, Random.Range(TopSpawn, BottomSpawn), 1);
        Transform enemyObj = Instantiate(enemy, position, Quaternion.identity);
        enemyObj.GetComponent<EnemyMovement>().gameManager = transform;
        scoretext.GetComponent<Text>().text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                var position = new Vector3(40, Random.Range(TopSpawn, BottomSpawn), 1);
                Transform enemyObj = Instantiate(enemy, position, Quaternion.identity);
                enemyObj.GetComponent<EnemyMovement>().gameManager = transform;
                timer = timerTo;
                if (timer > timerCap)
                {
                    timerTo -= timerDescent;
                }
            }

            score += Time.deltaTime;
            scoretext.GetComponent<Text>().text = score.ToString("0.0");
        }
    }
}
