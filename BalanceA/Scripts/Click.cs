using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        // this object was clicked - do something
        iTween.RotateBy(gameObject, iTween.Hash("z", .25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .04));
        //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 60), 0.5F);
    }
}
