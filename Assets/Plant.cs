using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plant : MonoBehaviour, IPointerUpHandler
{
    private SpriteRenderer sprite;
    private GameObject touchedObject;
    private bool isShowUISeed = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isShowUISeed)
        {
            Vector3 pos = Input.GetTouch(0).position;
            //We transform the touch position into word space from screen space and store it.
            Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(pos);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            //Debug.Log("Touched " + touchPosWorld.x + "" +  touchPosWorld.y);
            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                if (touchedObject.tag == "Land")
                {
                    if (sprite)
                        sprite.sortingOrder = 1;
                    GameObject.Find("SVSeed").transform.position = new Vector2(pos.x + 140, pos.y + 140);
                    isShowUISeed = true;
                }
            }
            else
            {
                GameObject.Find("SVSeed").transform.position = new Vector2(-200, 0);
                isShowUISeed = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        string nameSeed = this.gameObject.name;
        int numSeed = Int32.Parse(nameSeed);
        if (touchedObject.tag == "Land")
        {
            touchedObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[numSeed-3];
            GameObject.Find("SVSeed").transform.position = new Vector2(-200, 0);
            isShowUISeed = false;
        }
    }
}
