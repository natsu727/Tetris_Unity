using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{


    // スコア関連
    public Text scoreText;

    private int score;
    
    public int currentScore;
    // public int clearScore =1500;
    
    // public Text timerText;
    public float gameTime =120f;
    int seconds;
    
    public GameObject gamePauseUI;

    public Text CountText;

    public float countdown=3f;
    int count;
    
    // Start is called before the first frame update
    void Start()
    {
        GamePauseToggle();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown>=0)
        {
            countdown -=Time.deltaTime;
            count =(int)countdown;
            CountText.text =count.ToString();
        }
        if(countdown<=0)
        {
            CountText.text="";
            TimeManagement();
        }
    }

    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        score = 0;

    }
    
    public void TimeManagement()
    {
    	gameTime -= Time.deltaTime;
    	seconds =(int)gameTime;
    	// timerText.text =seconds.ToString();
    	
    	// if(seconds == 0)
    	// {
    	// 	Debug.Log("TimeOut");
    	// 	GameOver();
    	// }
    }

    // スコアの追加
    public void AddScore()
    {
        score += 100;
        currentScore += score;
        scoreText.text = "Score: " + currentScore.ToString();

        Debug.Log(currentScore);
        
        // if(currentScore >= clearScore)
        // {
        // 	GameClear();
        // 	//Debug.Log(clearScore);
        // }

    }
    
    public void GameOver()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void GameClear()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
    public void GamePause()
    {
    	GamePauseToggle();
    }
    
    public void GamePauseToggle()
    {
    	gamePauseUI.SetActive(!gamePauseUI.activeSelf);
    	
    	if(gamePauseUI.activeSelf)
    	{
    		Time.timeScale =0f;
    	}
    	else
    	{
    		Time.timeScale=1f;
    	}
    }

    public void Reload(){
        SceneManager.LoadScene(1);
    }
}
