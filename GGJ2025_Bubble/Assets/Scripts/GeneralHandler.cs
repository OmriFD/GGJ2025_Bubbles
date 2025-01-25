using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas endCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        endCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        FindObjectOfType<SoapMeterHandler>().StartGame();
        FindObjectOfType<ScoreHandler>().StartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
