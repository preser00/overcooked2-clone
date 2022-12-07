using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIUpdater : MonoBehaviour
{
    public TextMeshProUGUI textmesh;
    public GameManager gm; 

    public string displayType; //type: score, timer

    private void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        if(displayType == "score")
        {
            textmesh.text = "Score: " + gm.score.ToString();
        }
        else if(displayType == "timer")
        {
            textmesh.text = gm.timerDisplay; 
        }
    }
}
