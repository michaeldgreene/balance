using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThrow : MonoBehaviour {
    public Rigidbody rb;
    // Use this for initialization
    void Start () {
        InvokeRepeating("Throw", 0, 5F);
    }

    void Throw()
    {
        rb.transform.position = new Vector3(2, 5, -3);
        rb.velocity = new Vector3(Random.Range(-4F, -1F), 0, 0);
    }

    // Update is called once per frame
    void Update () {

    }
}
