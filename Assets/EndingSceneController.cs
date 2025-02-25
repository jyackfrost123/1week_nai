using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using unityroom.Api;


public class EndingSceneController : MonoBehaviour
{
    [SerializeField] private Image nyaruImage;

    [SerializeField] private Image nyaruTrueImage;

    [SerializeField] private GameObject bgmcontroller;

    [SerializeField] private GameObject heartSE;
    [SerializeField] private GameObject horroEffectSE;

    [SerializeField] private GameObject nyaruSE;

    [SerializeField] private GameObject deleteSE;

    [SerializeField] private TextMeshProUGUI endingText;

    [SerializeField] private GameObject buttons;

    parameterController para;

    void Start()
    {

        para = (GameObject.Find("FadeManager")).GetComponent<parameterController>();

        para.OneDten = UnityEngine.Random.Range(0,10);

        
        endingText.text = "";

        buttons.SetActive(false);
     
        Color color;
        color = nyaruImage.color;
        nyaruImage.color = new Color(color.r, color.g, color.b, 0.0f);

        color = nyaruTrueImage.color;
        nyaruTrueImage.color = new Color(color.r, color.g, color.b, 0.0f);

        Instantiate( heartSE, Vector3.zero, Quaternion.identity);//心臓音を待機
        Instantiate( horroEffectSE, Vector3.zero, Quaternion.identity);//恐怖音を待機

        var sequence = DOTween.Sequence(); 
        sequence.AppendInterval(1.0f);

        sequence.Append(nyaruImage.DOFade(1.0f, 1.0f))
                .Append(nyaruImage.DOFade(0.0f, 1.5f)
                        .OnStart(()=>{Instantiate( nyaruSE, Vector3.zero, Quaternion.identity);}))
                .Join(nyaruTrueImage.DOFade(1.0f, 2.2f))
                .AppendCallback( () => {
                    nyaruTrueImage.color = new Color(color.r, color.g, color.b, 0.0f);
                    Instantiate( deleteSE, Vector3.zero, Quaternion.identity);
                    })
                .Append(nyaruTrueImage.DOFade(0.0f, 0.5f))
                //.Append ( (nyaruTrueImage.rectTransform).DOScale(new Vector3(10.1f, 10.1f, 1), 0.5f))
                //.Append ( (nyaruTrueImage.rectTransform).DOScale(new Vector3(1.1f, 1.1f, 1), 2.2f))
                .AppendInterval(2.0f)
                .AppendCallback( () => {
                    endingText.text = "";
                    String tweet = "";

                    tweet += "人ならざるもの見たあなたは\n"+para.Score+"回逃げたが\n";
        
                    switch (para.OneDten){
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

                    tweet += "に襲われ\n破滅した。";

                    endingText.text = tweet;

                    buttons.SetActive(true);

                })
                ;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}
