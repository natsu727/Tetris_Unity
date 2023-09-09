using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Tetris");
    }

    public void OpenRule(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnPortal(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenRanking(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
