using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject notePrefab;


    private void Start()
    {
        //Instantiate(notePrefab, transform.position, Quaternion.identity);
    }

    public void SpawnNote()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);
    }


}
