using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying = false;

    public BeatSpawner theBS;


    private void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.CanStart = true;
                theBS.StartSpawning();
                theMusic.Play();
            }
        }
    }

}
