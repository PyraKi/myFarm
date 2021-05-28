using CI.QuickSave;
using UnityEngine;
using UnityEngine.EventSystems;

public class Eggs : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
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
                if (hitInformation.collider != null)
                {
                    //We should have hit something with a 2D Physics collider!
                    GameObject touchedObject = hitInformation.transform.gameObject;
                    //touchedObject should be the object someone touched.
                    if (touchedObject.tag == "Eggs")
                    {
                        if (QuickSaveReader.Create("myFarmWarehouse").Exists("Gold"))
                        {
                            int temmp = QuickSaveReader.Create("myFarmWarehouse").Read<int>("Eggs");
                            QuickSaveWriter.Create("myFarmWarehouse").Write("Eggs", ++temmp).Commit();
                            Destroy(touchedObject);
                        }
                    }
                }
            }
        }
    }
}
