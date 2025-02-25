using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using unityroom.Api;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
        
    [SerializeField]
    private TextMeshProUGUI timerText;
    
    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private GameObject endSE;

    [SerializeField]
    private GameObject gameoverButtons;

    [field: SerializeField] public bool isGameStart;
    [field: SerializeField] public bool isGameFinished;
    [field: SerializeField] public float time; 
    [field: SerializeField] public float baseTime; 

    [SerializeField] public int score;

     [SerializeField] private PlayerController player; 

     private bool isCount;

     parameterController para;

    
    
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isGameFinished = false;

        gameoverButtons.SetActive(false);
        
        score = 0;

        isCount = true;

        para = ( GameObject.Find("FadeManager") ).GetComponent<parameterController>();
        para.Score = 0;//スコアリセット
        para.isFirst = true;

        //開始時にはミニタイマーを透明に
        //timerText.color = new Color(timerText.color.r, timerText.color.g, timerText.color.b, 0.0f);
        changeTimerColor(false);
    }

    void Update()
    {
        if (isGameStart)
        {
            if(isCount)time -= Time.deltaTime;
            if (time <= 0.0f)
            {
                
                player.limitDeadEffect();
                //isGameStart = false;
                //GameFinish();
                //DOVirtual.DelayedCall (3.0f, ()=> DoChangeScene());  
                //GameFinish();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = "正常: "+ score;
        timerText.text = ((int)time+1).ToString();
        
        //残り3秒以内
        if(isGameStart){
            if (time < 2.9f){
                timerText.color = Color.red;
            }else{
                timerText.color = Color.yellow;
            }
        }
        

    }
    
    //ゲーム終了時の処理
    public void GameFinish()
    {

        isGameStart = false;

        //gameOverText.text = "FINISH!";
        gameOverText.color = Color.yellow;
        //para.TotalScore = player.AnimalTotalHight;
        //para.TotalAnimalNum = player.AnimalNum;
        isGameFinished = true;
        
        /* TODO: ランキング送信の処理を入れる*/
        

        //gameoverButtons.SetActive(true);
        //Instantiate(endSE, Vector3.zero, Quaternion.identity);
        DOTween.KillAll();

        para.Score = score;
        UnityroomApiClient.Instance.SendScore(1, (float)score, ScoreboardWriteMode.Always);

        SceneManager.LoadScene("EndingScene");
        
    }

    void DoChangeScene()
    {
        //フェード遷移とか入れる
        //FadeManager.Instance.LoadScene ("Ending", 1.5f);
    }

    public void AddScore(){
        if(isGameStart && !isGameFinished ) score++;
    }

    public void ResetTime(){
        isCount = false;
        time = baseTime - (score / 7.0f);
        if (time < 1.0f) time = 1.0f;//下限の設定
        //time = baseTime;
        isCount = true;

    }

    public void changeTimerColor(bool on){

        Color color = timerText.color;

        if (on) color.a = 1.0f;
        else color.a = 0.0f;

        timerText.color = color;

        //Debug.Log(timerText.color);

    }
}
