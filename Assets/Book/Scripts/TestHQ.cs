using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHQ : MonoBehaviour
{
    public GameObject[] objOpen;
    public GameObject[] objClose;



    void Start()
    {
        for (int i = 0; i < objOpen.Length; i++)
        {
            objOpen[i].SetActive(true);
        }
        for (int i = 0; i < objClose.Length; i++)
        {
            objClose[i].SetActive(false);
        }
    }


    void Update()
    {
        
    }
}
