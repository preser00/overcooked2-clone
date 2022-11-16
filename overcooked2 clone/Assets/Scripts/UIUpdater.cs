using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIUpdater : MonoBehaviour
{
    public TextMeshProUGUI textmesh;
    public string displayType; //this script can be used for displaying score or timer text depending on this variable

    private void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        //commented for now until textmesh is set up 
        if(displayType == "score")
        {
            //textmesh.text = GameManager.score;
        }
        else if(displayType == "timer")
        {
            //textmesh.text = GameManager.timerDisplay; 
        }
    }
}
