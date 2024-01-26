using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying = false;

    public BeatSpawner theBS;
    public float DelayBeforeMusic = 3.9f;

    public static GameManager instance;

    public int currentScore = 0;
    public int ScorePerNote = 75;
    public int ScorePerGoodNote = 100;
    public int ScorePerPerfectNote = 150;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    public int currentComboScore = 1;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThreshholds;
    
    //private float tempoINicialOficial;
    //private float startTime;

    private void Start()
    {
        instance = this;
        scoreText.text = "Score : 0";
        comboText.text = "Combo : 1x";
        currentMultiplier = 1;

        theMusic.Play();
        //tempoINicialOficial = Time.time;
        theMusic.Pause();
    }

    private void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {

                //float endTime = Time.time;
                //float totalTime = endTime - startTime;

                //Debug.Log("Total Time: " + totalTime);
                //Debug.Log(tempoINicialOficial);
                StartMusicAfterDelay();
                startPlaying = true;
                theBS.CanStart = true;
                theBS.StartSpawning();
            }
        }
    }

    void StartMusicAfterDelay()
    {
        StartCoroutine(WaitB4MusicPlay(DelayBeforeMusic));
    }

    IEnumerator WaitB4MusicPlay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        theMusic.UnPause();

    }

    public void NoteHit()
    {
        //Debug.Log("Note Hit");
        if (currentMultiplier - 1 < multiplierThreshholds.Length)
        {
            multiplierTracker++;

            if (multiplierThreshholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        currentComboScore++;
        comboText.text = "Combo : " + currentComboScore + "x";
        //currentScore += ScorePerNote * currentMultiplier;
        scoreText.text = "Score : " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += ScorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += ScorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += ScorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    public void NoteMiss()
    {
        Debug.Log("Note Miss");
        currentComboScore = 1;
        comboText.text = "Combo : " + currentComboScore + "x";
        currentMultiplier = 1;
        multiplierTracker = 0;

    }



}
