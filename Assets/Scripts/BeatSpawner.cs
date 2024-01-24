using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeatSpawner : MonoBehaviour
{
    public string filePath = "Assets/Resources/lvl1.txt"; // Adjust the file path as needed
    public List<GameObject> spawners; // List of spawner game objects
    public GameObject note; // Prefab to instantiate

    private bool CanStart = false;

    void Update()
    {
        if (!CanStart)
        {
            if (Input.anyKeyDown)
            {
                CanStart = true;
                StartSpawning();
            }
        }
    }

    void StartSpawning()
    {
        StartCoroutine(ReadTextFileWithDelay(filePath, 1f));
    }

    IEnumerator ReadTextFileWithDelay(string path, float delay)
    {
        // Check if the file exists
        if (File.Exists(path))
        {
            // Use StreamReader to read one line at a time
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Debug.Log("Read line: " + line);

                    // Convert the line to an integer
                    if (int.TryParse(line, out int spawnerNumber))
                    {
                        ActivateSpawner(spawnerNumber-1);
                    }
                    else
                    {
                        Debug.LogError("Invalid number in the text file: " + line);
                    }

                    yield return new WaitForSeconds(delay);
                }
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
