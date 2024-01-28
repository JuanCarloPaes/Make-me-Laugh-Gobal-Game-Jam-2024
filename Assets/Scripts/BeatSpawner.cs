using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeatSpawner : MonoBehaviour
{
    // Reference to the text file
    public TextAsset filePath; // Drag and drop the text file here in the Unity Editor
    public List<GameObject> spawners; // List of spawner game objects
    public BeatDetector beatDetector; // Beat Timings (list and such)

    public float InitDelay = 1f;

    private int BeatDelayNumber = 0;

    public void StartSpawning()
    {
        StartCoroutine(ReadTextFileWithDelay(InitDelay));
    }

    IEnumerator ReadTextFileWithDelay(float delay)
    {
        List<string> itemsList = new List<string>();

        // Check if the filePath is not null
        if (filePath != null)
        {
            // Use StringReader to read the text content of the TextAsset
            using (StringReader reader = new StringReader(filePath.text))
            {
                while (reader.Peek() != -1)
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
            Debug.LogError("Text file not assigned. Please assign the text file in the inspector.");
        }
    }

    void ActivateSpawner(int spawnerNumber)
    {
        // Check if the spawnerNumber is within the valid range
        if (spawnerNumber >= 0 && spawnerNumber < spawners.Count)
        {
            // Activate the corresponding spawner
            spawners[spawnerNumber].GetComponent<SpawnPoint>().SpawnNote();

            // Optionally, you can deactivate other spawners if needed
            for (int i = 0; i < spawners.Count; i++)
            {
                if (i != spawnerNumber)
                {
                    //spawners[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Invalid spawner number: " + spawnerNumber);
        }
    }
}
