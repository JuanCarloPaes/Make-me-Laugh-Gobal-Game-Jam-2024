using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;


public class BeatDetector : MonoBehaviour
{
    public string filePath = "Assets\\Resources\\Lvl 1\\lvl1BeatTime.txt"; // Adjust the file path as needed

    public List<float> beatTimes = new List<float>();
    public List<float> beatIntervalos = new List<float>();

    private float BeatTimeDif = 0;
    private int refTime = 1;

    void Start()
    {
        ReadBeatTimesFromFile();
        MakeBeatIntervList();
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GetBeatTimes(BeatTimeDif, refTime);
            refTime +=1;
        }
    }


    void ReadBeatTimesFromFile()
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (float.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out float beatTime))
                {
                    beatTimes.Add(beatTime);
                }
                else
                {
                    Debug.LogError("Failed to parse beat time: " + line);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading beat times from file: " + e.Message);
        }
    }


    void GetBeatTimes(float DelayDif, int beatCount)
    {
        //DelayDif = beatTimes[beatCount] - beatTimes[beatCount - 1];
        //Debug.Log(beatIntervalos[beatCount]);
    }

    void MakeBeatIntervList()
    {

        float BeatInterv;

        for (int item = 1; item < beatTimes.Count; item++)
        {

            BeatInterv = beatTimes[item] - beatTimes[item - 1];
            beatIntervalos.Add(BeatInterv);
        }
    }

}
