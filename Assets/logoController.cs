using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class logoController : MonoBehaviour
{
    
    void Start()
    {
        TextMeshProUGUI tmpro = GetComponent<TextMeshProUGUI>();

        //DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(tmpro);

        /*for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
        {
            
        }*/

        tmpro.DOFade(0.0f, 0.9f).SetLoops(-1,  LoopType.Yoyo);

    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}
