using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject perfab;
    GameObject carrot;
    GameObject cabbage;
    GameObject radish;
    GameObject strawberry;
    GameObject corn;

    void Start()
    {
        carrot = Instantiate(perfab, transform);
        carrot.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[38]; // Carrot
        carrot.AddComponent<Plant>();
        carrot.transform.Find("Money").GetComponent<Text>().text = "80";
        carrot.name = "38";
        carrot.tag = "Seed";

        cabbage = Instantiate(perfab, transform);
        cabbage.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[43]; // Bap cai
        cabbage.AddComponent<Plant>();
        cabbage.transform.Find("Money").GetComponent<Text>().text = "160";
        cabbage.name = "43";
        cabbage.tag = "Seed";

        radish = Instantiate(perfab, transform);
        radish.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[48]; // Cu cai
        radish.AddComponent<Plant>();
        radish.transform.Find("Money").GetComponent<Text>().text = "180";
        radish.name = "48";
        radish.tag = "Seed";

        strawberry = Instantiate(perfab, transform);
        strawberry.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[53]; // Dau
        strawberry.AddComponent<Plant>();
        strawberry.transform.Find("Money").GetComponent<Text>().text = "400";
        strawberry.name = "53";
        strawberry.tag = "Seed";

        corn = Instantiate(perfab, transform);
        corn.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[58]; // Bap
        corn.AddComponent<Plant>();
        corn.transform.Find("Money").GetComponent<Text>().text = "220";
        corn.name = "58";
        corn.tag = "Seed";

        StartCoroutine(Content());
    }

    IEnumerator Content()
    {
        while (true)
        {
            if (QuickSaveReader.Create("myFarmWarehouse").Exists("Gold"))
            {
                int money = QuickSaveReader.Create("myFarmWarehouse").Read<int>("Gold");
               
                if (money < 80)
                    carrot.GetComponent<Image>().color = Color.red;
                else
                    carrot.GetComponent<Image>().color = Color.white;


                if (money < 160)
                    cabbage.GetComponent<Image>().color = Color.red;
                else
                    cabbage.GetComponent<Image>().color = Color.white;


                if (money < 180)
                    radish.GetComponent<Image>().color = Color.red;
                else
                    radish.GetComponent<Image>().color = Color.white;


                if (money < 400)
                    strawberry.GetComponent<Image>().color = Color.red;
                else
                    strawberry.GetComponent<Image>().color = Color.white;


                if (money < 220)
                    corn.GetComponent<Image>().color = Color.red;
                else
                    corn.GetComponent<Image>().color = Color.white;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
