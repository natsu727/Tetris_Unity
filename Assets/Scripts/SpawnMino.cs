using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;

    // Start is called before the first frame update
    void Start()
    {
            Invoke("NewMino",4.0f);
    }

    public void NewMino() 
    {
        Instantiate(Minos[Random.Range(0, Minos.Length)], transform.position, Quaternion.identity);
    }
}
