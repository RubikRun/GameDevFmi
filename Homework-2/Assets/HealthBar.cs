using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const int heartsCount = 3;
    GameObject[] heartsFull;
    GameObject[] heartsEmpty;

    // Start is called before the first frame update
    void Start()
    {
        heartsFull = new GameObject[heartsCount];
        heartsEmpty = new GameObject[heartsCount];
        for (int i = 0; i < heartsCount; i++)
        {
            heartsFull[i] = transform.GetChild(i).gameObject;
            heartsEmpty[i] = transform.GetChild(heartsCount + i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHeart(int health)
    {
        Debug.Log("Heart removed from health bar.");

        heartsFull[health].SetActive(false);
        heartsEmpty[health].SetActive(true);
    }
}
