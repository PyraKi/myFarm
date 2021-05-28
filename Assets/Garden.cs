using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    private int countLand = 0;
    private int countWood = 0;
    public GameObject chickenPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GeneraterLand(-4.5f, -3.5f, 15, 6);
        GeneraterLand(-1.5f, 2.5f, 9, 9);
        GeneraterLand(7.5f, 2.5f, 3, 9);

        //Load chickend from db
        if (QuickSaveReader.Create("myFarmStables").Exists("Chickens"))
        {
            int chik = QuickSaveReader.Create("myFarmStables").Read<int>("Chickens");
            while (chik > 0)
            {
                chik--;
                int posx = (int)RandomNumber(-4, 4);
                int posy = (int)RandomNumber(-11, -8);
                GameObject chicken = Instantiate(chickenPrefab, new Vector3(posx, posy, 0), Quaternion.identity);
                GameObject stables = GameObject.Find("stables");
                chicken.transform.parent = stables.transform;
                chicken.name = "Chicken" + (stables.transform.childCount + 1).ToString();
            }
        }
    }

    private void GeneraterLand(float x, float y, float rows, float cols)
    {
        GameObject wood = new GameObject("Wood" + (countWood++));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject land = new GameObject("Land" + (countLand++));

                float posX = row;
                float posY = col;

                land.transform.position = new Vector2(posX, posY);
                land.AddComponent<BoxCollider2D>();
                land.AddComponent<SpriteRenderer>();
                land.AddComponent<Plant>();
                land.tag = "Land";
                land.transform.parent = wood.transform;
            }
        }

        wood.transform.position = new Vector2(x, y);
        wood.transform.parent = GameObject.Find("WoodLand").transform;
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
