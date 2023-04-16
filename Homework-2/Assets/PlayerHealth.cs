using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 3;

    private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        GameObject healthBarObject = GameObject.Find("HealthBar");
        healthBar = healthBarObject.GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealth()
    {
        health--;
        healthBar.RemoveHeart(health);
    }

    public bool IsGameOver()
    {
        return health <= 0;
    }
}
