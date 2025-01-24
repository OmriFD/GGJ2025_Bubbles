using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerPressHandler : MonoBehaviour
{
    private float pressDuration = 0f; // Tracks how long the finger is pressed
    private bool isPressing = false; // Flag to check if finger is down

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Transform placeToSpawn;
    [SerializeField] private float minStartScale;
    [SerializeField] private float maxStartScale;
    [SerializeField] private float amountToEnlargeBy;
    [SerializeField] private float bubbleSpeed;
    [SerializeField] private float bubbleMaxSize;

    private GameObject currentBubble;

    void Update()
    {
        // Check if there's at least one touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Finger just pressed down
            if (touch.phase == TouchPhase.Began)
            {
                isPressing = true;
                pressDuration = 0f; // Reset duration
                CreateNewBubble();
            }
            // Finger is being held down
            else if (touch.phase is TouchPhase.Stationary or TouchPhase.Moved)
            {
                if (isPressing)
                {
                    pressDuration += Time.deltaTime; // Accumulate time
                    OnFingerPress();
                }
            }
            // Finger lifted
            else if (touch.phase is TouchPhase.Ended or TouchPhase.Canceled)
            {
                if (!isPressing) return;
                
                OnFingerReleased(pressDuration);
                isPressing = false;
            }
        }
        else
        {
            isPressing = false;
        }
    }

    private void CreateNewBubble()
    {
        currentBubble = Instantiate(bubblePrefab, placeToSpawn.position, Quaternion.identity);
        float startingScale = Random.Range(minStartScale, maxStartScale);
        currentBubble.transform.localScale = new Vector3(startingScale, startingScale, startingScale);
    }

    private void OnFingerPress()
    {
        if (currentBubble == null) return;
        
        currentBubble.transform.localScale += Vector3.one * amountToEnlargeBy;
        if (currentBubble.transform.localScale.x >= bubbleMaxSize)
        {
            Destroy(currentBubble.gameObject);
            currentBubble = null;
        }
    }
    
    // Called when the finger is released
    private void OnFingerReleased(float duration)
    {
        if (currentBubble != null) 
            currentBubble.GetComponent<Rigidbody>().velocity = new Vector3(0, bubbleSpeed, 0);
        currentBubble = null;
    }
}
