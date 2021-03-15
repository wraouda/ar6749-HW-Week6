using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class ASCIILevelLoader : MonoBehaviour
{

    public float xOffset;
    public float yOffset;
    public string file_name; 
    public GameObject player;
    public GameObject wall;
    public GameObject spike;
    public GameObject dragon;
    public GameObject final;
    public GameObject goal;
    public GameObject point;
    public GameObject currentPlayer;

    private Vector2 startPos; // starting position 

    public int currentLevel = 0;

    public int CurrentLevel
    {
        get { return currentLevel; } // get the current level variable
        set
        {
            currentLevel = value; // set it to value
            LoadLevel(); // load the level
        }
    }

    public GameObject level;


    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(); // call the function
    }

    void LoadLevel()
    {
        Destroy(level);
        level = new GameObject("Levels");
        
        string current_file_path = Application.dataPath + "/Levels/" + file_name.Replace(
            "Num",
            currentLevel + ""); // Find the level file

        string[] fileLines = File.ReadAllLines(current_file_path); // read all lines instead of all text

        for (int y = 0; 
            y < fileLines.Length; 
            y++) // read through the file array for the y positions
        {
            string lineText = fileLines[y]; // read the file that contains the level text

            char[] characters = lineText.ToCharArray(); // reading each character in its own spot


            for (int x = 0;
                x < characters.Length;
                x++) // read through the text and position of each character in the text
            {
                char c = characters[x];

                GameObject newObj;

                switch (c)
                {
                    case 'p':
                        newObj = Instantiate<GameObject>(player); // make a player
                        currentPlayer = newObj; // setting current player to new obj
                        startPos = new Vector2(x + xOffset, -y + yOffset); // setting the start position
                        
                        if (currentPlayer == null) //if not object on the currentPlayer
                        { 
                            currentPlayer = newObj; //store this object
                            startPos = new Vector2(
                                x + xOffset, -y + yOffset);
                        }
                        break;
                    case 'w':
                        newObj = Instantiate<GameObject>(wall); // make a wall
                        break;
                    case 's':
                        newObj = Instantiate<GameObject>(spike); // make a spike
                        break;
                    case 'g':
                        newObj = Instantiate<GameObject>(goal); // make a goal
                        break;
                    case 'd':
                        newObj = Instantiate<GameObject>(point); // make a diamond
                        break;
                    case 'x':
                        newObj = Instantiate<GameObject>(dragon); // make a dragon
                        break;
                    case 'k':
                        newObj = Instantiate<GameObject>(final); // make the final wizard
                        break;
                    default:
                        newObj = null; // set gameobject to null
                        break;
                }

                if (newObj != null) // if newobj is set to anything
                {
                        newObj.transform.parent = level.transform; // change levels

                        newObj.transform.position = new Vector3(x + xOffset, -y + yOffset, 0); // find the position in the file
                }
            }
        }
    }

    public void ResetPlayer()
    {
        currentPlayer.transform.position = startPos;
    } 

    // Update is called once per frame
    void Update()
    {
       
    }
}
