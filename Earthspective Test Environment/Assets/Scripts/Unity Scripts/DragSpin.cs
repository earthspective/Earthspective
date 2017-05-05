using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSpin : MonoBehaviour {

    private Vector3 mouseStart;
    private Vector3 dragVelocity;
    Vector3 rotate = Vector3.zero;
    private Vector3 velocityStart;
    private Vector3 velocity;
    private float timeStamp;
    public float friction;
    bool reset = false;
    bool mouseOver = false;
    bool mouseOverStart = false;

    private int lerpTimeout = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && reset == false && mouseOver == true) {
            mouseStart = Input.mousePosition;
            velocityStart = mouseStart;
            timeStamp = Time.time;
            mouseOverStart = true;
        }

        if ((Input.GetMouseButtonDown(0) == true && mouseOver == false))
        {
            mouseOverStart = false;
        }

        if (Input.GetMouseButtonUp(0) && reset == false && mouseOverStart == true)
        {
            velocity = ((velocityStart - Input.mousePosition) / (timeStamp - Time.time))*.05f*(Camera.main.fieldOfView/60);
        }


        if(velocity.magnitude > 4) {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * friction);
        }else
        {
            velocity = Vector3.zero;
        }

        if (Camera.main.fieldOfView >= 5 && Camera.main.fieldOfView <= 60) {
            Camera.main.fieldOfView += -Input.mouseScrollDelta.y;
        }
        else {
            if (Camera.main.fieldOfView < 5) { Camera.main.fieldOfView = 5; }
            if (Camera.main.fieldOfView > 60) { Camera.main.fieldOfView = 60; }
        }

        if (Input.GetKeyDown(KeyCode.Space) == true && reset == false)
        {
            reset = true;
        }
        

        if(reset == true)
        {
            if (this.transform.rotation != Quaternion.identity && lerpTimeout < 300)
            {
                lerpTimeout++;
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, Time.deltaTime * 1f);
            }else
            {
                lerpTimeout = 0;
                reset = false;
            }
        }

        if (Input.GetMouseButton(0) == true && reset == false && mouseOverStart == true)
        {
            rotate.x = (mouseStart.x - Input.mousePosition.x);
            rotate.y = (mouseStart.y - Input.mousePosition.y);
            this.transform.Rotate(Vector3.up, rotate.x*.1f, Space.World);
            this.transform.Rotate(Vector3.right, -rotate.y*.1f,Space.World);
        }else
        {
            this.transform.Rotate(Vector3.up, -velocity.x * .1f, Space.World);
            this.transform.Rotate(Vector3.right, velocity.y * .1f, Space.World);
        }

        mouseStart = Input.mousePosition;

        //this.transform.eulerAngles.Set(this.transform.eulerAngles.x,this.transform.eulerAngles.y,0f);
        
    }
    
    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            mouseOver = true;
        }
        else
        {
            mouseOver = false;
        }
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
