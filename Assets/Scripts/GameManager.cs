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

    private float totalNotes;
    private int biggestComboScore = 0;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject ResultsMenu;
    public TextMeshProUGUI biggestComboCount, percentageHitCount, normalHitsCount, goodHitsCount, perfectHitsCount, missedHitCount, finalScoreCount, totalScoreCount;

    //Barra de humor da estatua
    public int moodValue = 4;
    private bool gameOver = false;
    private BeatDetector beatDetectorScript; // Reference to the BeatDetector script

    private bool isPaused = false;

    public int specialModeCounter = 0;
    public bool specialModeBool = false;
    private SpecialModeObj specialModeObjScript;

    public Animator animProta;
    public Animator animEstatua;

    //private float tempoINicialOficial;
    //private float startTime;

    private void Start()
    {
        // Search for the object with the tag "BeatGenerator"
        GameObject beatGenerator = GameObject.FindGameObjectWithTag("BeatGenerator");
        GameObject specialModer = GameObject.FindGameObjectWithTag("SpecialMode");

        instance = this;
        scoreText.text = "Score : 0";
        comboText.text = "Combo : 1x";
        gameOver = false;
        currentMultiplier = 1;

        totalNotes = theBS.beatDetector.beatTimes.Count;


        // Search for the object with the tag "SpecialMode"
        GameObject specialModeObject = GameObject.FindGameObjectWithTag("SpecialMode");

        // Check if the object was found
        if (specialModeObject != null)
        {
            // Access and call a method from the SpecialMode object
            specialModeObjScript = specialModeObject.GetComponent<SpecialModeObj>();

            if (specialModeObjScript != null)
            {
                // Access and do something with the SpecialMode object if needed
            }
            else
            {
                Debug.LogError("SpecialModeObj script not found on SpecialMode object");
            }
        }
        else
        {
            Debug.LogError("SpecialMode object not found on scene");
        }


        // Check if the object was found
        if (beatGenerator != null)
        {
            // Access and call a method from the BeatGenerator object
            beatDetectorScript = beatGenerator.GetComponent<BeatDetector>();

            if (beatDetectorScript != null)
            {
                beatDetectorScript.CurrentFace = moodValue;
                // Access and do something with the beatGenerator object if needed
            }
            else
            {
                Debug.LogError("BeatDetector script not found on BeatGenerator object");
            }
        }
        else
        {
            Debug.LogError("BeatGenerator not found on scene");
        }

        theMusic.Play();
        //tempoINicialOficial = Time.time;
        theMusic.Pause();

    }

    private void Update()
    {
        if (specialModeCounter >= 1000 && !specialModeBool)
        {
            specialModeCounter = 1000;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                specialModeBool = true;
                specialModeCounter = 0;

                if (specialModeObjScript != null)
                {
                    specialModeObjScript.MakeSpecialModeHappen();
                    specialModeBool = false;
                }
                else
                {
                    Debug.LogError("SpecialModeObj script not assigned to specialModeObjScript");
                }
            }
        }

        // Check for pause/resume input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // Update moodValue
        moodValue = Mathf.Clamp(moodValue, 0, 10); // Ensure moodValue is within the valid range

        // Set CurrentFace in BeatDetector script
        if (beatDetectorScript != null)
        {
            beatDetectorScript.CurrentFace = moodValue;
        }
        else
        {
            Debug.LogError("BeatDetector script not assigned to beatDetectorScript");
        }


        //Regular Mood Score
        if (moodValue > 10)
        {
            moodValue = 10;
        }
        else if (moodValue <= 0)
        {
            gameOver = true;
            Time.timeScale = 0f; // Pause time scale
        }

        if (!startPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                //float endTime = Time.time;
                //float totalTime = endTime - startTime;

                //Debug.Log("Total Time: " + totalTime);
                //Debug.Log(tempoINicialOficial);
                StartMusicAfterDelay();
                animEstatua.SetBool("comecoJogo", true);
                animProta.SetBool("comecar", true );
                startPlaying = true;
                //theBS.CanStart = true;
                theBS.StartSpawning();
            }
        }

        else
        {
            if (!theMusic.isPlaying && !ResultsMenu.activeInHierarchy && !gameOver)
            {
                // Check if the audio has reached the end
                if (theMusic.time >= theMusic.clip.length)
                {
                    ShowMenu();
                }
            }

            else if (gameOver)
            {
                theMusic.Pause();
                ShowMenu();
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
        // Check if the current combo is the biggest and update if necessary
        if (currentComboScore > biggestComboScore)
        {
            biggestComboScore = currentComboScore;
        }

        //currentScore += ScorePerNote * currentMultiplier;
        scoreText.text = "Score : " + currentScore;

    }

    public void NormalHit()
    {
        currentScore += ScorePerNote * currentMultiplier;
        normalHits++;
        //Mood Score
        moodValue += 0;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += ScorePerGoodNote * currentMultiplier;
        goodHits++;
        //Mood Score
        moodValue+= 1;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += ScorePerPerfectNote * currentMultiplier;
        perfectHits++;
        specialModeCounter++;
        //Mood Score
        moodValue+=2;
        NoteHit();
    }

    public void NoteMiss()
    {
        //Debug.Log("Note Miss");
        missedHits++;
        moodValue--;
        currentComboScore = 1;
        comboText.text = "Combo : " + currentComboScore + "x";
        currentMultiplier = 1;
        multiplierTracker = 0;

    }

    public void PauseGame()
    {
        isPaused = true;
        theMusic.Pause();
        ShowMenu(); // mostra só o menu FINAL
        Time.timeScale = 0f; // Pause time scale
    }

    public void ResumeGame()
    {
        isPaused = false;
        ResultsMenu.SetActive(false); // esconde menu FINAL
        theMusic.UnPause();
        Time.timeScale = 1f; // Resume time scale
    }

    private void ShowMenu()
    {
        ResultsMenu.SetActive(true);

        normalHitsCount.text = "" + normalHits.ToString();
        goodHitsCount.text = "" + goodHits.ToString();
        perfectHitsCount.text = "" + perfectHits.ToString();
        missedHitCount.text = "" + missedHits.ToString();

        biggestComboCount.text = "" + biggestComboScore.ToString() + "x";
        totalScoreCount.text = "" + currentScore.ToString();

        float totalHit = normalHits + goodHits + perfectHits;
        float percentageHit = (totalHit / totalNotes) * 100f;

        percentageHitCount.text = "" + percentageHit.ToString("F2") + "%";
    }


}
