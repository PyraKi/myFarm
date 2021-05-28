using UnityEngine;
using UnityEngine.UI;
using CI.QuickSave;

public class Warehouse : MonoBehaviour
{
    public GameObject Carrots;
    public GameObject Cabbages;
    public GameObject Radishs;
    public GameObject Strawberrys;
    public GameObject Corns;
    public GameObject Eggs;
    public GameObject Total;
    public Button btnSell;
  
    void OnEnable()
    {
        if (QuickSaveReader.Create("myFarmWarehouse").Exists("Gold"))
        {
            QuickSaveReader reader = QuickSaveReader.Create("myFarmWarehouse");
            CropsInfo cropsInfo = new CropsInfo();
            int total = 0;
            int amount = reader.Read<int>("Carrot");
            Carrots.transform.Find("Amount").GetComponent<Text>().text = "x" + amount.ToString();
            Carrots.transform.Find("Total").GetComponent<Text>().text = (amount * cropsInfo.Carrot.HarvestMoney).ToString();
            total += amount * cropsInfo.Carrot.HarvestMoney;

            amount = reader.Read<int>("Cabbage");
            Cabbages.transform.Find("Amount").GetComponent<Text>().text = "x" + amount.ToString();
            Cabbages.transform.Find("Total").GetComponent<Text>().text = (amount * cropsInfo.Cabbage.HarvestMoney).ToString();
            total += amount * cropsInfo.Cabbage.HarvestMoney;

            amount = reader.Read<int>("Radish");
            Radishs.transform.Find("Amount").GetComponent<Text>().text = "x" + amount.ToString();
            Radishs.transform.Find("Total").GetComponent<Text>().text = (amount * cropsInfo.Radish.HarvestMoney).ToString();
            total += amount * cropsInfo.Radish.HarvestMoney;

            amount = reader.Read<int>("Strawberry");
            Strawberrys.transform.Find("Amount").GetComponent<Text>().text = "x" + amount.ToString();
            Strawberrys.transform.Find("Total").GetComponent<Text>().text = (amount * cropsInfo.Strawberry.HarvestMoney).ToString();
            total += amount * cropsInfo.Strawberry.HarvestMoney;

            amount = reader.Read<int>("Corn");
            Corns.transform.Find("Amount").GetComponent<Text>().text = "x" + amount.ToString();
            Corns.transform.Find("Total").GetComponent<Text>().text = (amount * cropsInfo.Corn.HarvestMoney).ToString();
            total += amount * cropsInfo.Corn.HarvestMoney;

            amount = reader.Read<int>("Eggs");
            Eggs.transform.Find("Amount").GetComponent<Text>().text = "x" + amount.ToString();
            Eggs.transform.Find("Total").GetComponent<Text>().text = (amount * 50).ToString();
            total += amount * 50;

            Total.GetComponent<Text>().text = "Total: " + total.ToString();
            if (total == 0)
                btnSell.interactable = false;
            else btnSell.interactable = true;
        }
    }
}
