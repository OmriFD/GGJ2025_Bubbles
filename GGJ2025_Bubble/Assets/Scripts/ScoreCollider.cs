using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bubble")) return;
        
        if (transform.parent.transform.localScale.x < 65)
        {
            FindObjectOfType<ScoreHandler>().IncreaseScore((int)other.transform.localScale.x * 100 + 65);
        }
        else
        {
            FindObjectOfType<ScoreHandler>().IncreaseScore((int)other.transform.localScale.x * 100);
        }
    }
}
