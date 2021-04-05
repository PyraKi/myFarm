using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    private int countLand = 0;
    private int countWood = 0;
    // Start is called before the first frame update
    void Start()
    {
        GeneraterLand(-4.5f, -3.5f, 3, 3);
        GeneraterLand(-4.5f, -0.5f, 9, 3);
        GeneraterLand(-1.5f, 2.5f, 9, 9);
        GeneraterLand(7.5f, 2.5f, 3, 9);
    }

    private void GeneraterLand(float x, float y, float rows, float cols)
    {
        GameObject wood = new GameObject("Wood" + (countWood++));

        for (int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                GameObject land = new GameObject("Land"+(countLand++));

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
}
