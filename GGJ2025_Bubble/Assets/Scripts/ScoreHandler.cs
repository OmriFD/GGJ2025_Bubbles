using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private TMP_Text score;
    private int currentScore;
    
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
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
}