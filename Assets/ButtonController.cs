using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using unityroom.Api;

public class ButtonController : MonoBehaviour
{

   parameterController para;
   //public GameObject[] Omakes;

   UIController ui;

   bool isChanged;
    
    
    void Start()
    {
     
     if(GameObject.Find("FadeManager") != null){
         para = GameObject.Find("FadeManager").GetComponent<parameterController>(); 
     }
     
     ui = GameObject.Find("Canvas").GetComponent<UIController>();

     isChanged = false;
     //para.
      
    }

    public void goTweet(){
       //naichilab.UnityRoomTweet.Tweet ("gameID", "このゲームは"+100+"点"+"とったテストです。", "unity1week", "testGame");
       
       //if(ui != null){
       if(para != null){

        String tweet = "";

        //ui.score

        //int i = UnityEngine.Random.Range(0, 10);
        int i = para.OneDten;

        tweet += "人ならざるもの見たあなたは"+para.Score+"回逃げたが、";
        
        switch (i){
            case 0: tweet += "健忘症"; break;
            case 1: tweet += "激しい恐怖症"; break;
            case 2: tweet += "幻覚"; break;
            case 3: tweet += "奇妙な性的嗜好"; break;
            case 4: tweet += "強烈な執着"; break;
            case 5: tweet += "制御できない震え"; break;
            case 6: tweet += "心因性視覚障害"; break;
            case 7: tweet += "支離滅裂な幻覚"; break;
            case 8: tweet += "偏執症"; break;
            default: tweet += "強迫観念"; break;
        }

        tweet += "に襲われ、破滅した。";
            

        naichilab.UnityRoomTweet.Tweet ("nyaiaru_iaia", tweet, "unity1week", "nyaiaru_1week");

       }
       
       //StartCoroutine(TweetWithScreenShot.TweetManager.TweetWithScreenShot("ツイート本文をここに書く"));//画像あり
    }

    public void goRanking(){
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking (para.score);
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking (0, 0);
        if(para != null) UnityroomApiClient.Instance.SendScore(para.Score, 1, ScoreboardWriteMode.Always);
    }

    public void goResult(int score){
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking (para.score);
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking (score, 0);
        UnityroomApiClient.Instance.SendScore(1, 1, ScoreboardWriteMode.Always);
    }
    
    
    /*
    public void go2ndRanking(){
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (0, 1);
    }

    public void go2ndResult(int score){
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking (para.score);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (score, 1);
    }
    */
    

    public void ReStart(){
        if(!isChanged){
            isChanged = true;
            FadeManager.Instance.LoadScene ("GameScene", 0.5f);
        }

        /*
        if(para != null && para.notFirst == false){
            FadeManager.Instance.LoadScene ("Tutorial", 0.5f);
            para.notFirst = true;
        }else{
          FadeManager.Instance.LoadScene ("GameScene", 1.0f);
        }
        */
        
        
    }

    public void FastReStart(){
         SceneManager.LoadScene("GameScene");
    }

    public void Re2ndStart(){

        //FadeManager.Instance.LoadScene ("EndressGameScene", 1.0f);

        /*
        if(para != null && para.notFirst == false){
            FadeManager.Instance.LoadScene ("Tutorial", 0.5f);
            para.notFirst = true;
        }else{
          FadeManager.Instance.LoadScene ("EndressGameScene", 1.0f);
        }
        */
        
        
    }

     public void Fast2ndReStart(){
         SceneManager.LoadScene("EndressGameScene");
    }

    public void goTutorial(){
        FadeManager.Instance.LoadScene ("Tutorial", 0.5f);
    }

    public void goTitle(){
        FadeManager.Instance.LoadScene ("Title", 0.5f);
    }

    public void goEffectGame(){
        if(!isChanged){
            isChanged = true;
            FadeManager.Instance.LoadScene ("EffectScene", 0.5f);
        }
    }

 
}
