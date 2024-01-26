using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeatSpawner : MonoBehaviour
{
    public string filePath = "Assets/Resources/Lvl 1/lvl1.txt"; // Adjust the file path as needed
    public List<GameObject> spawners; // List of spawner game objects
    public BeatDetector beatDetector; // Beat Timings (list and such)

    public float InitDelay = 1f;

    public bool CanStart = false;

    private int BeatDelayNumber = 0;


    public void StartSpawning()
    {
        StartCoroutine(ReadTextFileWithDelay(filePath, InitDelay));
    }

    IEnumerator ReadTextFileWithDelay(string path, float delay)
    {
        List<string> itemsList = new List<string>();

        // Check if the file exists
        if (File.Exists(path))
        {
            // Use StreamReader to read one line at a time
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    //Debug.Log("Read line: " + line);

                    // Convert the line to an integer
                    if (int.TryParse(line, out int spawnerNumber))
                    {

                        
                        if (BeatDelayNumber < beatDetector.beatIntervalos.Count)
                        {
                            delay = beatDetector.beatIntervalos[BeatDelayNumber];
                        }
                        BeatDelayNumber += 1;

                        //Debug.Log(delay + " : " + spawnerNumber);

                        ActivateSpawner(spawnerNumber - 1);
                    }
                    else
                    {
                        Debug.LogError("Invalid number in the text file: " + line);
                    }

                    // Add the line to the list
                    itemsList.Add(line);

                    yield return new WaitForSeconds(delay);
                }
            }

            // Do something with the generated list (itemsList) here if needed
            foreach (string item in itemsList)
            {
                //Debug.Log("Item: " + item);
            }
        }
        else
        {
            Debug.LogError("File not found at path: " + path);
        }
    }

    void ActivateSpawner(int spawnerNumber)
    {
        // Check if the spawnerNumber is within the valid range
        if (spawnerNumber >= 0 && spawnerNumber < spawners.Count)
        {
            // Activate the corresponding spawner
            spawners[spawnerNumber].SetActive(true);
            // Call SpawnNote method from the activated spawner
            spawners[spawnerNumber].GetComponent<SpawnPoint>().SpawnNote();

            // Optionally, you can deactivate other spawners if needed
            for (int i = 0; i < spawners.Count; i++)
            {
                if (i != spawnerNumber)
                {
                    spawners[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Invalid spawner number: " + spawnerNumber);
        }
    }



}
