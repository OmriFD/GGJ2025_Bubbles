using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text finalScore;
    
    private int currentScore;
    
    // Start is called before the first frame update
    void Start()
    {
StartGame();
    }

    public void StartGame()
    {
        currentScore = 0;
        score.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int scoreAddition)
    {
        currentScore += scoreAddition;
        score.text = currentScore.ToString();
    }

    public void ShowFinalScore()
    {
        finalScore.text = currentScore.ToString();
    }
}