using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A) == true)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 1, this.transform.rotation.eulerAngles.z);
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y - 1, this.transform.rotation.eulerAngles.z);
        }

        if (Input.GetKey(KeyCode.W) == true)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z+ 1);
        }

        if (Input.GetKey(KeyCode.W) == true)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z - 1);
        }
    }
}
