using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpningController : MonoBehaviour
{

    parameterController para;
    [SerializeField] GameObject restartButton;
    
    void Start()
    {
        para = GameObject.Find("FadeManager").GetComponent<parameterController>();

        if(para != null & !para.isFirst) restartButton.SetActive(false);
        else restartButton.SetActive(true);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}
