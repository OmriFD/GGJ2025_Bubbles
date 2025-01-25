using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FingerPressHandler : MonoBehaviour
{
    private float pressDuration; // Tracks how long the finger is pressed
    private bool isPressing; // Flag to check if finger is down

    [SerializeField] private bool isMobileVersion;
    [Header("Bubble Info")]
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Transform placeToSpawn;
    [SerializeField] private float minStartScale;
    [SerializeField] private float maxStartScale;
    [SerializeField] private float amountToEnlargeBy;
    [SerializeField] private float bubbleSpeed;
    [SerializeField] private float bubbleMaxSize;

    [SerializeField] private float originalTimeBetween = 0.5f;
    //[SerializeField] private ParticleSystem bubblePopEffect;

    [Header("Soap Meter Info")] 
    [SerializeField] private float soapCostPerSecond;
    [SerializeField] private float startBubbleCost;

    private Rigidbody currentBubble;
    private SoapMeterHandler soapMeterHandler;
    private float currentSoapCost;

    private float timerBetweenBubbles;
    private bool bubbleIsOut;

    private void Start()
    {
        timerBetweenBubbles = originalTimeBetween;
        soapMeterHandler = FindObjectOfType<SoapMeterHandler>();
    }

    void Update()
    {
        if (bubbleIsOut)
        {
            timerBetweenBubbles -= Time.deltaTime;
            if (timerBetweenBubbles <= 0)
            {
                bubbleIsOut = false;
            }
        }
        
        if(!soapMeterHandler.HasSoap() || bubbleIsOut) return;

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
        //currentBubble.velocity = new Vector3(0, 0, 0);
        float startingScale = Random.Range(minStartScale, maxStartScale);
        currentBubble.transform.localScale = new Vector3(startingScale, startingScale, startingScale);
        currentSoapCost = startBubbleCost;
    }

    private void OnFingerPress()
    {
        if (currentBubble == null) return;
        
        float scaleIncrease = amountToEnlargeBy;
        currentBubble.transform.localScale += Vector3.one * scaleIncrease;

        currentSoapCost += soapCostPerSecond * Time.deltaTime;
        
        if (currentBubble.transform.localScale.x >= bubbleMaxSize)
        {
            currentBubble.GetComponent<MeshRenderer>().enabled = false;
            currentBubble.GetComponentInChildren<ParticleSystem>().Play();
            Destroy(currentBubble.gameObject,1.5f);
            currentBubble = null;
            soapMeterHandler.DecreaseSlider(currentSoapCost);
            currentSoapCost = 0;
        }
    }
    
    // Called when the finger is released
    private void OnFingerReleased()
    {
        if (currentBubble != null)
        {
            currentBubble.isKinematic = false;
            currentBubble.velocity = new Vector3(0, bubbleSpeed, 0);
            soapMeterHandler.DecreaseSlider(currentSoapCost);
        }
        timerBetweenBubbles = originalTimeBetween;
        bubbleIsOut = true;
        currentBubble = null;
        
        currentSoapCost = 0;
    }
}