using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bubble") || other.gameObject.CompareTag("Ring"))
        {
            if (other.gameObject.CompareTag("Ring"))
            {
                Destroy(other.transform.parent.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                other.transform.GetComponent<MeshRenderer>().enabled = false;
                other.transform.GetComponentInChildren<ParticleSystem>().Play();
                Destroy(other.gameObject, 1.5f);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble") || other.CompareTag("Ring"))
        {
            if (other.CompareTag("Ring"))
            {
                Destroy(other.transform.parent.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                other.transform.GetComponent<MeshRenderer>().enabled = false;
                other.transform.GetComponentInChildren<ParticleSystem>().Play();
                Destroy(other.gameObject, 1.5f);
            }
            
        }
    }
}
