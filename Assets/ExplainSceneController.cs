using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class ExplainSceneController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI explainText;

    private string[] explainWords={
        "我は\"ナイアルラトホテップ\"",
        "我の化身から逃れるさまを見せよ",
        "この迷宮を進み\n異変を感じたら引き返せ",
        "躊躇したり、誤った選択をすれば\n正気に関わるであろう",
        "ここから逃れる術は「ない」\nできるだけあがけ"
    };

    [SerializeField] private GameObject[] nyaru_voice_se;

    [SerializeField] private GameObject changeSE;

    int n;

    [SerializeField] private RectTransform backInroom; 
    
    void Start()
    {

        n = 0;

        //explainText.text = "我は、ナイアルラトテップ";
        
        Instantiate( changeSE, Vector3.zero, Quaternion.identity);//音声

        backInroom.localScale = Vector3.zero;
        backInroom.DOScale(new Vector3(1, 1, 1), 0.5f);

        var sequence = DOTween.Sequence(); 

        //sequence.AppendCallBack(() => {});

        sequence.AppendInterval(2.5f);

        for(int i = 0; i < explainWords.Length ; i++){

            sequence.Append(explainText.DOFade(1.0f, 3.0f) 
                    .OnStart(()=>{
                        colorLess();
                        explainText.text = explainWords[n];
                        Instantiate( nyaru_voice_se[n], Vector3.zero, Quaternion.identity);//音声
                    }))
                    .Append(explainText.DOFade(0.0f, 1.0f))
                    .AppendCallback(()=>{
                        n++;
                    });
        }

        sequence.AppendInterval(2.0f)
                .AppendCallback(()=>{
                    SceneManager.LoadScene("GameScene");
                });
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void colorLess(){

        explainText.text = "";
        Color c = explainText.color;

        c.a = 0;

        explainText.color = c;

    }
}
