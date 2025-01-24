using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerPressHandler : MonoBehaviour
{
    private float pressDuration; // Tracks how long the finger is pressed
    private bool isPressing; // Flag to check if finger is down

    [SerializeField] private bool isMobileVersion;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Transform placeToSpawn;
    [SerializeField] private float minStartScale;
    [SerializeField] private float maxStartScale;
    [SerializeField] private float amountToEnlargeBy;
    [SerializeField] private float bubbleSpeed;
    [SerializeField] private float bubbleMaxSize;

    private Rigidbody currentBubble;

    void Update()
    {
        if (isMobileVersion)
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

                    OnFingerReleased();
                    isPressing = false;
                }
            }
            else
            {
                isPressing = false;
            }
            return;
        }

        //--------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pressDuration == 0)
            {
                CreateNewBubble();
                pressDuration += Time.deltaTime;
            }
        }
        
        if (pressDuration > 0)
        {
            pressDuration += Time.deltaTime;
            OnFingerPress();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnFingerReleased();
            pressDuration = 0;
        }
    }

    private void CreateNewBubble()
    {
        currentBubble = Instantiate(bubblePrefab, placeToSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
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
    private void OnFingerReleased()
    {
        if (currentBubble != null) 
            currentBubble.velocity = new Vector3(0, bubbleSpeed, 0);
        currentBubble = null;
    }
}