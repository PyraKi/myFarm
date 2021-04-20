using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject perfab;
    private void Start()
    {
        Populate();
    }
    private void Populate()
    {
        GameObject seed;
        seed = Instantiate(perfab, transform);
        seed.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[38]; // Carrot
        seed.AddComponent<Plant>();
        seed.name = "38";
        seed.tag = "Seed";

        seed = Instantiate(perfab, transform);
        seed.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[43]; // Bap cai
        
        seed.AddComponent<Plant>();
        seed.name = "43";
        seed.tag = "Seed";

        seed = Instantiate(perfab, transform);
        seed.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[48]; // Cu cai
        seed.AddComponent<Plant>();
        seed.name = "48";
        seed.tag = "Seed";

        seed = Instantiate(perfab, transform);
        seed.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[53]; // Dau
        seed.AddComponent<Plant>();
        seed.name = "53";
        seed.tag = "Seed";

        seed = Instantiate(perfab, transform);
        seed.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("farming")[58]; // Bap
        seed.AddComponent<Plant>();
        seed.name = "58";
        seed.tag = "Seed";
    }
}
