using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plant : MonoBehaviour, IPointerUpHandler
{
    private SpriteRenderer sprite;
    private GameObject touchedObject;
    private bool isShowUISeed = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        int perCent = RandomNumber(0, 100);
       /* if(perCent < 50 && sprite.sortingOrder == 0)
        {
            sprite.sortingOrder = 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("grass");
            Debug.Log(gameObject.name);
        }*/
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 pos = Input.GetTouch(0).position;
            //We transform the touch position into word space from screen space and store it.
            Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(pos);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            //Debug.Log("Touched " + touchPosWorld.x + "" +  touchPosWorld.y);
            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hitInformation.collider != null && !isShowUISeed)
            {
                //We should have hit something with a 2D Physics collider!
                touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                if (touchedObject.tag == "Land")
                {
                    if (sprite)
                        sprite.sortingOrder = 1;
                    if (touchedObject.GetComponent<SpriteRenderer>().sprite == null)
                    {
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
            touchedObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[numSeed-1];
            GameObject.Find("SVSeed").transform.position = new Vector2(-200, 0);
            isShowUISeed = false;
        }
    }

    private static readonly System.Random random = new System.Random();
    private static readonly object syncLock = new object();
    public static int RandomNumber(int min, int max)
    {
        lock (syncLock)
        { // synchronize
            return random.Next(min, max);
        }
    }
}
