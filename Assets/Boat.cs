using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CI.QuickSave;

public class Boat : MonoBehaviour
{
    [SerializeField]
    private Vector3 target = new Vector3(1, 1, 0);
    [SerializeField]
    private float speed = 12;
    private bool frag = false;
    private float timemove = 10;
    private Vector3 cornercoordinates;
    public GameObject blocker;
    public GameObject container;
    public Button btnSell;
    public Button btnClose;
    private int total = 0;

    void Start()
    {
        container.SetActive(false);
        btnSell.onClick.AddListener(sell);
        btnClose.onClick.AddListener(close);
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
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
                        cornercoordinates = touchedObject.transform.position;
                        container.SetActive(true);
                        blocker.SetActive(true);
                    }
                }
            }
        }
        if (frag)
            BoatRun();
    }

    void sell()
    {
        frag = true;
        container.SetActive(false);
        blocker.SetActive(false);
        ////Save gold
        if (QuickSaveReader.Create("myFarmWarehouse").Exists("Gold"))
        {
            QuickSaveReader reader = QuickSaveReader.Create("myFarmWarehouse");
            CropsInfo cropsInfo = new CropsInfo();
            
            int amount = reader.Read<int>("Carrot");
            total += amount * cropsInfo.Carrot.HarvestMoney;

            amount = reader.Read<int>("Cabbage");
            total += amount * cropsInfo.Cabbage.HarvestMoney;

            amount = reader.Read<int>("Radish");
            total += amount * cropsInfo.Radish.HarvestMoney;

            amount = reader.Read<int>("Strawberry");
            total += amount * cropsInfo.Strawberry.HarvestMoney;

            amount = reader.Read<int>("Corn");
            total += amount * cropsInfo.Corn.HarvestMoney;

            amount = reader.Read<int>("Eggs");
            total += amount * 50;

            total += QuickSaveReader.Create("myFarmWarehouse").Read<int>("Gold");
        }
        //remove store
        if (QuickSaveReader.Create("myFarmWarehouse").Exists("Gold"))
            QuickSaveWriter.Create("myFarmWarehouse")
            .Write("Carrot", 0)
            .Write("Cabbage", 0)
            .Write("Radish", 0)
            .Write("Strawberry", 0)
            .Write("Corn", 0)
            .Write("Eggs", 0)
            .Commit();
    }

    void close()
    {
        container.SetActive(false);
        blocker.SetActive(false);
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
            QuickSaveWriter.Create("myFarmWarehouse").Write("Gold", total).Commit();
            total = 0;
        }
    }
}
