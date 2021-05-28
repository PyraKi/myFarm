using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops
{
    private string name;
    private int money;
    private int harvestMoney;
    private long growingTime;
    private long reductionTime;

    public Crops(string name, int money, int harvestMoney, long growingTime, long reductionTime)
    {
        this.Name = name;
        this.Money = money;
        this.HarvestMoney = harvestMoney;
        this.GrowingTime = growingTime;
        this.ReductionTime = reductionTime;
    }

    public string Name { get => name; set => name = value; }
    public int Money { get => money; set => money = value; }
    public long GrowingTime { get => growingTime; set => growingTime = value; }
    public long ReductionTime { get => reductionTime; set => reductionTime = value; }
    public int HarvestMoney { get => harvestMoney; set => harvestMoney = value; }
}
