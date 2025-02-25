using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    UIController ui;

    public int correctRoad;

    public int sunity = 40;

    [SerializeField] private TextMeshProUGUI sanityText;

    [SerializeField] private GameObject trueSE;
    [SerializeField] private GameObject falseSE;

    [SerializeField] private GameObject forGamebuttons;

    [SerializeField] private TextMeshProUGUI damageEffect;

    [SerializeField] private RectTransform backInroom;
    [SerializeField] private Image backRoomImage;

    Sequence sequence;

    [SerializeField] private Sprite correctSprites;
    [SerializeField] private Sprite[] imcorrectBacksprites;

    [SerializeField] private Image nyaruImage;

    
    bool isClicked = false;
    
    void Start()
    {
        ui = GetComponent<UIController>();

        correctRoad = 0;

        sequence = DOTween.Sequence(); 

        sequence.AppendInterval(2.5f);

        nyaruImage.gameObject.SetActive(false);

        BackGroundReset();

        /*sequence.Append(backInroom.DOScale(1.2f, 5.0f))
                .Append(backInroom.DOScale(0.85f, 5.0f));
                */
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        sanityText.text = "SAN: "+sunity.ToString();
    }

    public void BackGroundReset(){
        sequence.AppendCallback(()=>{
            backInroom.localScale = Vector3.zero;

            //異常(=1)か正常(=0)かの抽選
            if(ui.score<=5){//最初の数回は正常
                correctRoad = 0;
            }else{
                correctRoad = Random.Range(0,2);
            }

            if(correctRoad == 0){
                backRoomImage.sprite = correctSprites;
            }else{
                backRoomImage.sprite = imcorrectBacksprites[Random.Range(0, imcorrectBacksprites.Length)];
            }

            });
        sequence.Append(backInroom.DOScale(new Vector3(1, 1, 1), 0.5f)
                        .OnStart( ()=> {/*backInroom.localScale = Vector3.zero;*/} )
                        )
        .AppendCallback(()=>{if(isClicked) isClicked = false;});
        sequence.Play();

        

    }

    // 0番目の道(進む)
    public void GoRoad(){
        if(!isClicked && ui.isGameStart){

            isClicked = true;

            backInroom.DOPause();

            sequence = DOTween.Sequence();//初期化

            sequence.Append(backInroom.DOScale(1.4f, 0.4f));//1.2f, 0.5f

            BackGroundReset();
            //sequence.Play();

            ui.ResetTime();

            if(correctRoad == 0){
                correctRoadEffect();
            }else{
                falseRoadEffect();
            }

            
            

        }

        

    }

    // 1番目の道(戻る)
    public void BackRoad(){

        if(!isClicked && ui.isGameStart){

        isClicked = true;

        backInroom.DOPause();

        sequence = DOTween.Sequence();//初期化

        /*if(correctRoad == 1){
            correctRoadEffect();
        }else{
            falseRoadEffect();
        }*/

        sequence.Append(backInroom.DOScale(0.85f, 0.4f));////0.85f, 0.5f
        BackGroundReset();
        //sequence.Play();

        ui.ResetTime();

        if(correctRoad == 1){
            correctRoadEffect();
        }else{
            falseRoadEffect();
        }

        
        
        }

    }

    public void correctRoadEffect(){
            
        if(ui.isGameStart){
            ui.score++;
            //trueSEを流す
            Instantiate(trueSE, Vector3.zero, Quaternion.identity);
        }
    
    }

    public void falseRoadEffect(){

        if(ui.isGameStart){
            //falseSEを流す
            Instantiate(falseSE, Vector3.zero, Quaternion.identity);

            //sequence.

            if(!nyaruImage.gameObject.activeSelf){
                nyaruImage.gameObject.SetActive(true);
            }


            //(nyaruImage)
            //SAN値を削る
            sunity-=10;
            //SAN値が0ならゲームオーバー処理
            if(sunity <= 0){
                sunity = 0;
                //GameOver
                ui.GameFinish();
                forGamebuttons.SetActive(false);
            }else if(sunity <= 11){
                Color c = sanityText.color;
                sanityText.color = Color.red;//文字を赤色にする
            }

            /*一瞬ニャルが見える*/
            Color color = nyaruImage.color;
            nyaruImage.color = new Color(color.r, color.g, color.b, 0.8f);

            nyaruImage.DOFade(0.0f, 0.2f) 
                    .OnComplete( ()=> {nyaruImage.gameObject.SetActive(false);} );


            color = damageEffect.color;
            damageEffect.color = new Color(color.r, color.g, color.b, 1.0f);

            damageEffect.DOFade(0.0f, 0.8f).SetDelay(0.5f);//.SetEase(Ease.Linear);
        }

    }

    public void limitDeadEffect(){

        BackGroundReset();

        falseRoadEffect();

        ui.ResetTime();

    }
}
