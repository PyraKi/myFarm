using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private SpriteRenderer sprite;
    private int[] seed = { 32, 33, 34, 35, 36, 37, 39, 40, 41, 42, 44, 46, 47, 49, 50, 51, 52, 54, 55, 46, 57};
    private System.Random r = new System.Random();
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
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
                if(touchedObject.tag == "Land")
                {
                    touchedObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[seed[r.Next(0, seed.Length)]];
                    if (sprite)
                        sprite.sortingOrder = 1;
                }  
            }
        }
    }
}
