using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField]
    private Vector3 target = new Vector3(1, 1, 0);
    [SerializeField]
    private float speed = 12;
    private bool frag = false;
    private float timemove = 10;
    private Vector3 cornercoordinates;

    private void Update()
    {
        //We check if we have one or more touch happening.
        //We also check if the first touches phase is Ended (that the finger was lifted)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !frag)
        {
            //We transform the touch position into word space from screen space and store it.
            Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            //Debug.Log("Touched " + touchPosWorld.x + "" +  touchPosWorld.y);
            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                if (touchedObject.transform.name == "Boat")
                {
                    frag = true;
                    cornercoordinates = touchedObject.transform.position;
                }
            }    
        }
        if (frag)
            BoatRun();
    }

    private bool isgoback = false;
    private void BoatRun()
    {
        if(timemove > 0)
        {
            // Debug.Log("run");
            timemove -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
        else
        {
            if (!isgoback)
            {
                isgoback = true;
                transform.Rotate(0, 180, 0);
            }
            goBack();
        }
    }

    private void goBack()
    {
        if (timemove > -10)
        {
            // Debug.Log("go back");
            timemove -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, cornercoordinates, Time.deltaTime * speed);
        }
        else
        {
            transform.Rotate(0, 180, 0);
            timemove = 10;
            frag = false;
        }
    }
}
