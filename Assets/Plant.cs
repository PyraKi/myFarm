using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CI.QuickSave;
using System.Collections;

public class Plant : MonoBehaviour, IPointerUpHandler
{
    private SpriteRenderer sprite;
    private GameObject touchObject;
    private bool isShowUISeed = false;
    private bool isNeedWatering= false;
    private bool canHarvest = false;
    private GameObject watering;
    private Coroutine CoroutineGrowUp;
    private int cycleLife;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GameObject.Find("BtnCloseSeed").GetComponent<Button>().onClick.AddListener(ButtonClose);
        if (sprite)
            sprite.sortingOrder = 1;
        if (QuickSaveReader.Create("myFarmLand").Exists(this.gameObject.name))
        {
            /*Land land = QuickSaveReader.Create("myFarmLand").Read<Land>(this.gameObject.name);
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == null)
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[land.Sprite];
            CoroutineGrowUp = StartCoroutine(growup(this.gameObject));*/
            // remove DB
            QuickSaveWriter quickSaveWriter = QuickSaveWriter.Create("myFarmLand");
            quickSaveWriter.Delete(this.gameObject.name);
            quickSaveWriter.Commit();
        }
    }

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
                if (hitInformation.collider != null && !isShowUISeed)
                {
                    //We should have hit something with a 2D Physics collider!
                    touchObject = hitInformation.transform.gameObject;
                    //touchedObject should be the object someone touched.
                    if (touchObject.tag == "Land")
                    {
                       if (canHarvest)
                        {
                            if (QuickSaveReader.Create("myFarmLand").Exists(touchObject.name))
                            {
                                Land land = QuickSaveReader.Create("myFarmLand").Read<Land>(touchObject.name);
                                //Save to warehouse
                                int amount = 0;
                                if (QuickSaveReader.Create("myFarmWarehouse").Exists(land.Crops.Name))
                                {
                                    amount = QuickSaveReader.Create("myFarmWarehouse").Read<int>(land.Crops.Name);
                                }
                                amount++;
                                QuickSaveWriter.Create("myFarmWarehouse").Write(land.Crops.Name, amount).Commit();
                                // Remove land and SpriteRenderer
                                touchObject.GetComponent<SpriteRenderer>().sprite = null;
                                // Remove from db
                                QuickSaveWriter quickSaveWriter = QuickSaveWriter.Create("myFarmLand");
                                quickSaveWriter.Delete(touchObject.name);
                                quickSaveWriter.Commit();
                                canHarvest = false;
                            }
                        }
                        else if (isNeedWatering)
                        {
                            if (QuickSaveReader.Create("myFarmLand").Exists(touchObject.name))
                            {
                                Land land = QuickSaveReader.Create("myFarmLand").Read<Land>(touchObject.name);
                                if (watering.GetComponent<SpriteRenderer>().sprite != null)
                                {
                                    land.HarvestTime.AddSeconds(-land.Crops.ReductionTime);
                                    QuickSaveWriter.Create("myFarmLand").Write(touchObject.name, land).Commit();
                                    Destroy(watering);
                                    isNeedWatering = false;
                                }
                            }
                        }
                        else if (touchObject.GetComponent<SpriteRenderer>().sprite == null)
                        {
                            GameObject.Find("SVSeed").transform.position = new Vector2(pos.x + 100, pos.y + 100);
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
    }

    void ButtonClose()
    {
        GameObject.Find("SVSeed").transform.position = new Vector2(-200, 0);
    }

    IEnumerator growup(GameObject landObject)
    {
        while (true)
        {
            //Get land from db
            if (QuickSaveReader.Create("myFarmLand").Exists(landObject.name))
            {
                Land land = QuickSaveReader.Create("myFarmLand").Read<Land>(landObject.name);
                if (landObject.GetComponent<SpriteRenderer>().sprite != null)
                {
                    // Count to harvers time
                    if (DateTime.Compare(DateTime.Now, land.HarvestTime) >= 0)
                    {
                        String cropsName = land.Crops.Name;
                        int cropsSprite = 0;
                        switch (cropsName)
                        {
                            case "Carrot":
                                cropsSprite = 34;
                                break;
                            case "Cabbage":
                                cropsSprite = 39;
                                break;
                            case "Radish":
                                cropsSprite = 44;
                                break;
                            case "Strawberry":
                                cropsSprite = 49;
                                break;
                            case "Corn":
                                cropsSprite = 54;
                                break;
                        }
                        landObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[cropsSprite];
                        QuickSaveWriter.Create("myFarmLand").Write(landObject.name, land).Commit();
                        canHarvest = true;
                        isNeedWatering = false;
                        Debug.Log("Can harvest");
                        if(watering != null)
                            Destroy(watering);
                        if(CoroutineGrowUp != null)
                            StopCoroutine(CoroutineGrowUp);
                    }
                    else
                    {
                        //DateTime WateringTime is later than DateTime.Now => need to Watering
                        if (DateTime.Compare(DateTime.Now, land.WateringTime) >= 0 && !isNeedWatering)
                        {
                            if(cycleLife < 2)
                            {
                                land.Sprite -= 1;
                                landObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[land.Sprite];
                                cycleLife++;
                            }

                            //sign watering
                            watering = new GameObject("watering");
                            watering.AddComponent<SpriteRenderer>();
                            watering.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[71];
                            watering.transform.parent = landObject.transform;
                            watering.transform.position = new Vector3(landObject.transform.position.x, 
                                landObject.transform.position.y + 0.6f, 
                                landObject.transform.position.z);
                            watering.GetComponent<SpriteRenderer>().sortingOrder = 1;
                           
                            //set time next watering
                            land.WateringTime.AddSeconds(land.Crops.GrowingTime / 2);
                            QuickSaveWriter.Create("myFarmLand").Write(landObject.name, land).Commit();
                            isNeedWatering = true;
                        }
                    }
                }
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        string nameSeed = this.gameObject.name;
        int numSeed = Int32.Parse(nameSeed);
        int money = Int32.Parse(this.gameObject.transform.Find("Money").GetComponent<Text>().text);
        int gold = QuickSaveReader.Create("myFarmWarehouse").Read<int>("Gold");
        if (gold >= money)
        {
            if (touchObject.tag == "Land")
            {
                touchObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[numSeed - 1];
                GameObject.Find("SVSeed").transform.position = new Vector2(-200, 0);
                isShowUISeed = false;

                //save
                Crops crops = null;
                switch (numSeed)
                {
                    case 38:
                        crops = new CropsInfo().Carrot;
                        break;
                    case 43:
                        crops = new CropsInfo().Cabbage;
                        break;
                    case 48:
                        crops = new CropsInfo().Radish;
                        break;
                    case 53:
                        crops = new CropsInfo().Strawberry;
                        break;
                    case 58:
                        crops = new CropsInfo().Corn;
                        break;
                }
                Land land = new Land(crops, numSeed - 1, DateTime.Now.AddSeconds(crops.GrowingTime), DateTime.Now.AddSeconds(crops.GrowingTime / 2));
                QuickSaveWriter.Create("myFarmLand").Write(touchObject.name, land).Commit();
                cycleLife = 0;
                QuickSaveWriter.Create("myFarmWarehouse").Write("Gold", gold - money).Commit();
                CoroutineGrowUp = StartCoroutine(growup(touchObject));
            }
        }
    }
}
