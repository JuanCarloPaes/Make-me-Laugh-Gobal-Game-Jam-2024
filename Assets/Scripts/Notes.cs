using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private float beatSpeedTempo = 240f;
    private bool canBePressed = false;
    private bool obtained = false;
    //private float startTime;

    public KeyCode keyToPress;

    // Class variable to store the spawn time
    //private static float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        beatSpeedTempo = beatSpeedTempo / 60;
        // Set the spawn time when the object is instantiated
        //spawnTime = Time.time;
        //startTime = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(beatSpeedTempo * Time.deltaTime, 0f, 0f);

        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                //float endTime = Time.time;
                //float totalTime = endTime - startTime;

                //Debug.Log("Total Time: " + totalTime);

                // This will resume the game (set time scale back to 1)
                //Time.timeScale = 1f;

                //GameManager.instance.NoteHit();

                obtained = true;
                gameObject.SetActive(false);

                if (Mathf.Abs(transform.position.x) < 7.6f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                }
                else if (Mathf.Abs(transform.position.x) < 7.9f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                }

                obtained = true;
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ButtonActive")
        {
            canBePressed = true;
            // Use the spawn time when the object enters the trigger
            //startTime = spawnTime;
            //Debug.Log(startTime);

            // This will pause the game
            //Time.timeScale = 0f;
        }

        else if (collision.tag == "NoteDeleter")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ButtonActive" && !obtained)
        {
            canBePressed = false;
            GameManager.instance.NoteMiss();
            // You can choose to resume the game when the object exits the trigger
            // Or you can let it stay paused, depending on your requirements
        }
    }
}
