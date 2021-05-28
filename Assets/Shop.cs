using UnityEngine;
using UnityEngine.UI;
using CI.QuickSave;
using System;
using System.Collections;

public class Shop : MonoBehaviour
{
    public Text TextName;
    public Text TextPrice;
    public Text TextHarvest;
    public Text TextGrowup;
    public Text TextTime;
    public Button BtnCarrot;
    public Button BtnCabbage;
    public Button BtnRadish;
    public Button BtnStrawberry;
    public Button BtnCorn;
    public Button BtnChicken;
    public Button btnBuy;
    public GameObject chickenPrefab;
    private int gold;
    // Start is called before the first frame update
    void Start()
    {
        BtnCarrot.onClick.AddListener(Carrot);
        BtnCabbage.onClick.AddListener(Cabbage);
        BtnRadish.onClick.AddListener(Radish);
        BtnStrawberry.onClick.AddListener(Strawberry);
        BtnCorn.onClick.AddListener(Corn);
        BtnChicken.onClick.AddListener(Chicken);
        btnBuy.onClick.AddListener(Buy);
        Carrot();
    }

    void OnEnable()
    {
        gold = QuickSaveReader.Create("myFarmWarehouse").Read<int>("Gold");
    }

    void Carrot()
    {
        TextName.text = "Tên: Cà rốt";
        TextPrice.text = "Giá mua: 80g";
        TextHarvest.text = "Thu hoạch: 86g";
        TextGrowup.text = "Thời gian sinh trưởng: 5 phút";
        TextTime.gameObject.SetActive(false);
        btnBuy.gameObject.SetActive(false);
    }

    void Cabbage()
    {
        TextName.text = "Tên: bắp cải";
        TextPrice.text = "Giá mua: 160g";
        TextHarvest.text = "Thu hoạch: 194g";
        TextGrowup.text = "Thời gian sinh trưởng: 18 phút";
        TextTime.gameObject.SetActive(false);
        btnBuy.gameObject.SetActive(false);
    }

    void Radish()
    {
        TextName.text = "Tên: củ cải";
        TextPrice.text = "Giá mua: 180g";
        TextHarvest.text = "Thu hoạch: 228g";
        TextGrowup.text = "Thời gian sinh trưởng: 25 phút";
        TextTime.gameObject.SetActive(false);
        btnBuy.gameObject.SetActive(false);
    }

    void Strawberry()
    {
        TextName.text = "Tên: dâu tây";
        TextPrice.text = "Giá mua: 400g";
        TextHarvest.text = "Thu hoạch: 542g";
        TextGrowup.text = "Thời gian sinh trưởng: 90 phút";
        TextTime.gameObject.SetActive(false);
        btnBuy.gameObject.SetActive(false);
    }

    void Corn()
    {
        TextName.text = "Tên: dâu tây";
        TextPrice.text = "Giá mua: 220g";
        TextHarvest.text = "Thu hoạch: 630g";
        TextGrowup.text = "Thời gian sinh trưởng: 12 tiếng";
        TextTime.gameObject.SetActive(false);
        btnBuy.gameObject.SetActive(false);
    }

    void Chicken()
    {
        TextName.text = "Tên: Gà tây";
        TextPrice.text = "Giá mua: 600g";
        TextHarvest.text = "Thu hoạch: 50g";
        TextGrowup.text = "Thời gian đẻ trứng: 5 phút";
        TextTime.text = "Thời gian mỗi lần cho ăn: 2 phút";
        TextTime.gameObject.SetActive(true);
        btnBuy.gameObject.SetActive(true);
        if (gold >= 600)
            btnBuy.interactable = true;
        else
            btnBuy.interactable = false;

    }

    void Buy()
    {
        StartCoroutine(SwapChicken());
    }

    IEnumerator SwapChicken()
    {
        //Save
        if (gold >= 600)
        {
            int chik = 0;
            if (QuickSaveReader.Create("myFarmStables").Exists("Chickens"))
            {
                chik = QuickSaveReader.Create("myFarmStables").Read<int>("Chickens");
            }
            chik++;
            QuickSaveWriter.Create("myFarmStables").Write("Chickens", chik).Commit();
            QuickSaveWriter.Create("myFarmStables").Write("TimeFeed", DateTime.Now.AddSeconds(2 * 60)).Commit();
            QuickSaveWriter.Create("myFarmStables").Write("TimeLay", DateTime.Now.AddSeconds(5 * 60)).Commit();

            gold -= 600;
            QuickSaveWriter.Create("myFarmWarehouse").Write("Gold", gold).Commit();

            //Delay 0.1s and swap chicken
            yield return new WaitForSecondsRealtime(0.1f);
            int posx = (int)RandomNumber(-4, 4);
            int posy = (int)RandomNumber(-11, -8);
            GameObject chicken = Instantiate(chickenPrefab, new Vector3(posx, posy, 0), Quaternion.identity);
            GameObject stables = GameObject.Find("stables");
            chicken.transform.parent = stables.transform;
            chicken.name = "Chicken" + (stables.transform.childCount + 1).ToString();

            Debug.Log("chicken");
        }
    }

    //Function to get a random number 
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
