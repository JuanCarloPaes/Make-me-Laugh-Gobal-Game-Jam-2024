using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusCoiso : MonoBehaviour
{

    public GameObject menuAbrir;
    
    public void QuitJogo()
    {
        Debug.Log("au");
        Application.Quit();

    }

    // Function to open another scene by name
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void mostrarMenu()
    {
        menuAbrir.SetActive(true);
    }

    public void NaoMostrarMenu()
    {
        menuAbrir.SetActive(false);
    }

}
