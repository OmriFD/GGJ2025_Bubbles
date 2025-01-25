using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoapMeterHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas endCanvas;
    
    [SerializeField] private Slider bubbleSlider;

    public bool gameOngoing = true;
    // Start is called before the first frame update
    void Start()
    {
        //bubbleSlider = GetComponent<Slider>();
        StartGame();
    }

    public void StartGame()
    {
        bubbleSlider.value = 100;
        gameOngoing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbleSlider.value <= 0 && gameOngoing)
        {
            gameOngoing = false;
            StartCoroutine(StartEndScreen());
        }
    }

    private IEnumerator StartEndScreen()
    {
        yield return new WaitForSeconds(2f);
        gameCanvas.gameObject.SetActive(false);
        endCanvas.gameObject.SetActive(true);
        FindObjectOfType<ScoreHandler>().ShowFinalScore();
    }

    public void DecreaseSlider(float howMuch)
    {
        bubbleSlider.value -= howMuch;
    }

    public bool HasSoap()
    {
        return bubbleSlider.value > 0;
    }
}
