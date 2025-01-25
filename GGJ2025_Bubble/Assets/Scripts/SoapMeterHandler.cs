using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoapMeterHandler : MonoBehaviour
{
    private Slider bubbleSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        bubbleSlider = GetComponent<Slider>();
        bubbleSlider.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseSlider(float howMuch)
    {
        bubbleSlider.value -= howMuch;
    }
}
