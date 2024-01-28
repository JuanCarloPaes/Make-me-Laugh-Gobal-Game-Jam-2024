using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class BeatDetector : MonoBehaviour
{
    // Reference to the text file
    public TextAsset beatTimeTextFile;

    public List<float> beatTimes = new List<float>();
    public List<float> beatIntervalos = new List<float>();

    //public GameObject estatua;
    public Animator anim;

    public int CurrentFace = 4;

    public List<Sprite> faceSprites = new List<Sprite>();
    private SpriteRenderer spriteRenderer; // Assuming you have a SpriteRenderer component attached to your GameObject

    void Start()
    {
        if (beatTimeTextFile != null)
        {
            ReadBeatTimesFromFile();
            MakeBeatIntervList();

            spriteRenderer = GetComponent<SpriteRenderer>(); // Assuming the script is attached to a GameObject with a SpriteRenderer component
            UpdateSprite();
        }
        else
        {
            Debug.LogError("Text file not assigned. Please assign the text file in the inspector.");
        }
    }

    private void Update()
    {
        UpdateSprite();
    }

    void ReadBeatTimesFromFile()
    {
        try
        {
            string[] lines = beatTimeTextFile.text.Split('\n');

            foreach (string line in lines)
            {
                if (float.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out float beatTime))
                {
                    beatTimes.Add(beatTime);
                }
                else
                {
                    //Debug.LogError("Failed to parse beat time: " + line);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading beat times from file: " + e.Message);
        }
    }

    void UpdateSprite()
    {
        if (CurrentFace >= 1 && CurrentFace <= faceSprites.Count)
        {
            spriteRenderer.sprite = faceSprites[CurrentFace - 1]; // Adjust index to start from 0
        }

        anim.SetInteger("Modes", CurrentFace);
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
