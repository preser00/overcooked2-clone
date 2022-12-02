using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeController : MonoBehaviour
{
    
    public List<Recipe> currTasks = new List<Recipe>(); //Holds current tasks as scriptable objects
    public List<GameObject> visuals = new List<GameObject>(); //Holds the visual prefabs

    public GameObject gm; //Game Manager
    public GameObject RecipePreFab; //Recipe PreFab
    public int maxNumTasks = 5; //Max Number of Taaks

    public List<float> timers = new List<float>(); //Track how long each task has been on the board

    public Transform T; //Dummy var
    public int inTimer = 0; //Track the overall game timer
    public bool ranOnce = false; //Sentinel var

    private Vector3 ogSpawnPt = new Vector3(-16, 4.4f, -200); //Where to start spawning from
    public List<Vector3> spawnPoints = new List<Vector3>(); //List of spawn points down the row

    public int waitTime = 30;


    private void Start()
    {

        for (int i = 0; i < maxNumTasks; i++)
        {
            spawnPoints.Add(ogSpawnPt + new Vector3(3 * i, 4.4f, -200)); //Set spacing between spawns
            timers.Add(0); //Init
        }
        
    }
    private void Update()
    {

        for (int i = 0; i < currTasks.Count; i++)
        {
            timers[i] += Time.deltaTime; //Increment any active tasks
        }

        inTimer = (int) gm.GetComponent<GameManager>().timeRemaining; //Get game timer

        if (inTimer % waitTime != 0 && ranOnce) //sentinel behavior
        {
            ranOnce = false;
        }
        //Spawn tasks if X # of secs have passed or less than 2 tasks
        if ((currTasks.Count < maxNumTasks && inTimer % waitTime == 0 && !ranOnce) || currTasks.Count < 2)
        {
            GenerateRandomTask();
            ranOnce = true;
        }
    }
    void GenerateRandomTask()
    {
        int index = Random.Range(0, 2); //Random choice between shrimp and fish
        Recipe obj = ScriptableObject.CreateInstance("Recipe") as Recipe; //Make scriptable object
        if (index == 0) { obj.initValues("FishRecipe", "Fish", "Chop"); } //Give object traits
        else { obj.initValues("ShrimpRecipe", "Shrimp", "Chop");  }
        currTasks.Add(obj); //Add task to current tasks
        GenerateVisuals(obj);
    }
    
    void GenerateVisuals(Recipe addRecipe)
    {
        for (int i = 0; i < currTasks.Count; i++)
        {
           
            if (i+1 == currTasks.Count)
            {
                GameObject newRecipe = Instantiate(RecipePreFab, T); //Instantiate prefab w/ visuals
                
                if (addRecipe.name.Equals("FishRecipe"))
                {
                    //  ***Change Sprite to Fish***
                }
                else
                {
                    //  ***Change Sprite to Shrimp***
                }

                visuals.Add(newRecipe);
            }
            
            visuals[i].transform.position = spawnPoints[i]; //Move prefabs
        }
    }
}
