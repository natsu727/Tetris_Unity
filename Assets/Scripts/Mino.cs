using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mino : MonoBehaviour
{
    public Vector3 acceleration;
    public float previousTime;
    // minoの落ちる時間
    public float fallTime=1.0f;

    // ステージの大きさ
    private static int width = 10;
    private static int height = 20;

    // mino回転
    public Vector3 rotationPoint;

    private static Transform[,] grid = new Transform[width, height];

    float FingerPosX0; //タップし、指が画面に触れた瞬間の指のx座標
    float FingerPosX1; //タップし、指が画面から離れた瞬間のx座標
    float FingerPosNow; //現在の指のx座標
    float PosDiff=0.5f;
    void Update()
    {
        foreach (Touch touch  in Input.touches){
                if(touch.phase == TouchPhase.Began){

                    FingerPosX0 = touch.position.x;
                }
                if(touch.phase == TouchPhase.Ended){
                
                    FingerPosX1 = touch.position.x;
                
                }
                
                if(touch.phase == TouchPhase.Moved){
                
                    FingerPosNow = touch.position.x;
                
                }
                MouseButton();
        }
        MinoMovememt();
    }

    private void MinoMovememt()
    {
        // 左矢印キーで左に動く
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(-1, 0, 0);
            }

        }
        // 右矢印キーで右に動く
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // 自動で下に移動させつつ、下矢印キーでも移動する
        else if (Input.GetKey(KeyCode.DownArrow)||Time.time - previousTime >= fallTime) 
        {
            transform.position += new Vector3(0, -1, 0);
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(0, -1, 0);
                // 今回の追加
                AddToGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<SpawnMino>().NewMino();
            }

            previousTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ブロックの回転
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if(!ValidMovement()){
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
    }

    public void MouseButton(){
        
        
        if (Mathf.Abs(FingerPosX0 - FingerPosX1) < PosDiff)
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if(!ValidMovement()){
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        
        //横移動の判断基準
        if (FingerPosNow - FingerPosX0 > PosDiff)
        {
            transform.position += new Vector3(1, 0, 0);
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (FingerPosNow - FingerPosX0 < -PosDiff)
        {
            transform.position += new Vector3(-1, 0, 0);
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        };
    }
    
    public void CheckLines()
    {
    	for(int i = height -1;i>=0;i--)
    	{
    		if(HasLine(i))
    		{
    			DeleteLine(i);
    			RowDown(i);
    		}
    	}
    }
    
    bool HasLine(int i)
    {
    	for(int j = 0;j<width;j++)
    	{
    		if(grid[j,i]==null)
    		{
    			return false;
    		}
    	}
    	
    	FindObjectOfType<GameManagement>().AddScore();
    	
    	return true;
    }
    
    void DeleteLine(int i)
    {
    	for(int j = 0;j<width;j++)
    	{
    		Destroy(grid[j,i].gameObject);
    		grid[j,i] =null;
    	}
    }
    
    public void RowDown(int i)
    {
    	for(int y =i;y<height;y++)
    	{
    		for(int j =0;j<width;j++)
    		{
    			if(grid[j,y] !=null)
    			{
    				grid[j,y-1] =grid[j,y];
    				grid[j,y] =null;
    				grid[j,y-1].transform.position -=new Vector3(0,1,0);
    			}
    		}
    	}
    }
    
    void AddToGrid() 
    {
        foreach (Transform children in transform) 
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundX, roundY] = children;
            
            
            if(roundY>=height -1)
            {
            	FindObjectOfType<GameManagement>().GameOver();
            }
        }
    }

    
    // minoの移動範囲の制御
    bool ValidMovement()
    {

        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            // minoがステージよりはみ出さないように制御
            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }
            // 今回の追加
            if (grid[roundX, roundY] != null)
            {
                return false;
            }

        }
        return true;
    }
}
