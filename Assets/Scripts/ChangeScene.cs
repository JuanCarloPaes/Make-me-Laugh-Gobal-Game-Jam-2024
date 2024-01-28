using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Function to open another scene by name
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
