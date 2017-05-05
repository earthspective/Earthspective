using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateGameObject : MonoBehaviour {

    public GameObject targetObject; 

	public void Duplicate()
    {
        GameObject instOb = Instantiate(targetObject, targetObject.transform.parent);
        instOb.SetActive(true);
    }
}
