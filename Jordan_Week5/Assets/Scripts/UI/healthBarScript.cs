using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarScript : MonoBehaviour
{
    public Slider slider;
    gameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHealth(int health) 
    {
        slider.maxValue = gm.maxHealth;
        slider.value = gm.maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = gm.health;
    }
    
}
