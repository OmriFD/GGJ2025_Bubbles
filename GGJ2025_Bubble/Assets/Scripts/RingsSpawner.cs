using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsSpawner : MonoBehaviour
{
    [SerializeField] private float minTimeBetweenSpawns;
    [SerializeField] private float maxTimeBetweenSpawns;
    [SerializeField] private float minRingScale;
    [SerializeField] private float maxRingScale;
    [SerializeField] private float minVelocity;
    [SerializeField] private float maxVelocity;
    // [SerializeField] private Transform leftSpawnPos;
    // [SerializeField] private Transform rightSpawnPos;
    [SerializeField] private bool leftPoint;

    [SerializeField] private GameObject ring;

    private float timeBeforeNextRing;
    private float currentScale;
    private float currentVelocity;
    private GameObject currentRing;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBeforeNextRing = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeforeNextRing < 0)
        {
            currentScale = Random.Range(minRingScale, maxRingScale);
            currentVelocity = Random.Range(minVelocity, maxVelocity);
            if (leftPoint)
            {
                currentRing = Instantiate(ring, this.transform.position, Quaternion.identity);
                currentRing.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                currentRing.GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity, 0, 0);
            }
            else
            {
                currentRing = Instantiate(ring, this.transform.position, Quaternion.identity);
                currentRing.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                currentRing.GetComponent<Rigidbody>().velocity = new Vector3(-currentVelocity, 0, 0);
            }
            timeBeforeNextRing=Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }

        timeBeforeNextRing -= Time.deltaTime;
    }
}
