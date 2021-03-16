using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lives; // number of lives
    public int level; // number of levels

    private bool isGame = true; // Tells me if I'm in game or not
    
    public static GameManager instance; //this static var will hold the Singleton

    private float timer = 0; // initial timer

    public Text timerText; // text for the timer

    int currentLevel = 0; // level 0

    private int score; 

    public int Score // score propertyw
    {
        get { return score; } // get int score
        set
        {
            score = value; // set the score as Score value
        }
    }

    private List<int> finalScore; // list of all final scores

    private const string FILE_FINAL_SCORES = "/finalScores.txt"; // file to score the scores
    private string FILE_PATH_FINAL_SCORES; // creates the file path

    void Awake()
    {
        if (instance == null) //instance hasn't been set yet
        {
            DontDestroyOnLoad(gameObject);  //Dont Destroy this object when you load a new scene
            instance = this;  //set instance to this object
        }
        else  //if the instance is already set to an object
        {
            Destroy(gameObject); //destroy this new object, so there is only ever one
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0; // start timer
        lives = 3; // set lives

        FILE_PATH_FINAL_SCORES = Application.dataPath + FILE_FINAL_SCORES; // build the data path out to know where the final score is
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // count the time going up 
        
        if(!isGame) // if we are not in game, display scores
        {
            string finalScoreString = "Final Scores\n\n"; // Display the Final Score string
            

            for (var i = 0; i < finalScore.Count; i++) // goes through our final score list
            {
                finalScoreString += finalScore[i] + "\n"; // add a space between each score
            }

            timerText.text = finalScoreString; // fill in the text with the numbers in final score
        }
        else // if we are, display time and lives
        {
            timerText.text = "Time: " + (int)timer + " Lives: " + lives + " Score: " + score; // tells time and lives
        }

        if (lives == 0 || level == 3) // if you lose 3 lives or get to level 3
        {
            lives = -1; // so that the game doesn't keep loading
            level = 4; // so that the scene doesn't keep loading
            isGame = false; // game is over
            SceneManager.LoadScene(1); // load end game scene
            UpdateFinalScore(); // run the function below
        }
    }

    void UpdateFinalScore()
    {
        if (finalScore == null) // if there are no final scores
        {
            finalScore = new List<int>();

            string fileContents = File.ReadAllText(FILE_PATH_FINAL_SCORES); // populate the final score screen 

            string[] fileScores = fileContents.Split(','); // reads each entry as its own paramter and ignores ,

            for (var i = 0; i < fileScores.Length - 1; i++) // loop through the file scores
            {
                finalScore.Add(Int32.Parse(fileScores[i])); // convert the final scores into the file scores
            }
        }

        for (var i = 0; i < finalScore.Count; i++) // go through the final score count
        {
            if (score > finalScore[i]) // if the score is the highest between all the score counts
            {
                finalScore.Insert(i,score); // place it at the top
                break; // make sure not to break the loop
            }
        }

        string saveFinalScoreString = ""; // save the final scores in the file; 

        for (var i = 0; i < finalScore.Count; i++) // loop through the final score count
        {
            saveFinalScoreString += finalScore[i] + ","; // save the final scores and add commas between each 
        }

        File.WriteAllText(FILE_PATH_FINAL_SCORES, saveFinalScoreString); // Write out the text
    }
}
