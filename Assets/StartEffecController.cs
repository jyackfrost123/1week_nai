using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class StartEffecController : MonoBehaviour
{

    [SerializeField] private Image[] withTextImage;

    [SerializeField] private TextMeshProUGUI[] beforeTexts;
    [SerializeField] private TextMeshProUGUI allText;
    [SerializeField] private Image nyaruImage;

    [SerializeField] private GameObject[] seObjects;
    
    int n;
    
    [SerializeField] private BGMController bgm;

    [SerializeField] private GameObject sakebiSE;

    void Start()
    {
        
        //image.DOFade(0, duration);

        Color color;

        foreach(TextMeshProUGUI ui in beforeTexts){
            color = ui.color;
            ui.color = new Color(color.r, color.g, color.b, 0.0f);
        }        

        foreach(Image img in withTextImage){
            if(img != null){
                color = img.color;
                img.color = new Color(color.r, color.g, color.b, 0.0f);
            }
        }


        color = allText.color;
        allText.color = new Color(color.r, color.g, color.b, 0.0f);

        color = nyaruImage.color;
        nyaruImage.color = new Color(color.r, color.g, color.b, 0.0f);

        var sequence = DOTween.Sequence(); 

        n = 0;

        sequence.AppendInterval(2.00f);

        for(int i = 0; i < beforeTexts.Length ; i++){
            TextMeshProUGUI ui = beforeTexts[i];

            float time = 2.0f;

            if(i < 2){
                time = 3.0f;//モノローグの場合はやや長く
            }

            sequence.Append( ui.DOFade(1, time).OnStart(() =>{ 
                    Instantiate( seObjects[n], Vector3.zero, Quaternion.identity);//音声
                    n++;
            }));
            
            if(i == 0){
                sequence.Join(withTextImage[0].DOFade(1, time));//男を出す
            }
            /*if(withTextImage[i] != null){
                sequence.Join(withTextImage[i].DOFade(1, time));
            }*/

            sequence.Append( ui.DOFade(0, time) ); //消す

            /*if(withTextImage[i] != null){
                sequence.Join(withTextImage[i].DOFade(0, time));
            }*/

            if(i == 1){
                sequence.Join(withTextImage[0].DOFade(0, time));//男を消す
            }

            Debug.Log(i);
        }


                /*sequence.Append( naiText.DOFade(1, 2.0f) ) //付ける
                .AppendCallback(() =>{ 
                    Instantiate( seObjects[0], Vector3.zero, Quaternion.identity);//音声
                })
                .Append( naiText.DOFade(0, 2.0f) ) //消す

                .Append( aruText.DOFade(1, 2.0f) ) //付ける
                .AppendCallback(() =>{ 
                    Instantiate( seObjects[1], Vector3.zero, Quaternion.identity);//音声
                })
                .Append( aruText.DOFade(0, 2.0f) ) //消す

                .Append( naiAruText.DOFade(1, 2.0f) ) //付ける
                .AppendCallback(() =>{ 
                    Instantiate( seObjects[2], Vector3.zero, Quaternion.identity);//音声
                })
                .Append( naiAruText.DOFade(0, 2.0f) ) //消す
                */

                sequence.AppendInterval(1.00f)

                .Append( allText.DOFade(1, 0.1f) ) //付ける
                .Join( nyaruImage.DOFade(1, 0.1f).OnStart(() =>{ 
                    Instantiate( seObjects[seObjects.Length - 1], Vector3.zero, Quaternion.identity);//音声
                    bgm.Stop();
                }) ) //付ける
                .AppendInterval(1.5f)
                .AppendCallback(()=>{
                    Instantiate( sakebiSE, Vector3.zero, Quaternion.identity );
                })
                .AppendInterval(1.5f)
                
                .AppendCallback(() =>
                {
                    SceneManager.LoadScene("ExplainScene");
                });

                sequence.Play();
        
    }



}
