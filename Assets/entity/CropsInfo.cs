using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsInfo
{
    private Crops carrot;
    private Crops cabbage;
    private Crops radish;
    private Crops strawberry;
    private Crops corn;

    public CropsInfo()
    {
        this.Carrot = new Crops("Carrot", 80, 86, 5*60, 10);
        this.Cabbage = new Crops("Cabbage", 160, 194, 18*60, 40);
        this.Radish = new Crops("Radish", 180, 228, 25*60, 76);
        this.Strawberry = new Crops("Strawberry", 400, 542, 90*60, 8*60);
        this.Corn = new Crops("Corn", 220, 630, 12*60*60, 24*60);
    }

    public Crops Carrot { get => carrot; set => carrot = value; }
    public Crops Cabbage { get => cabbage; set => cabbage = value; }
    public Crops Radish { get => radish; set => radish = value; }
    public Crops Strawberry { get => strawberry; set => strawberry = value; }
    public Crops Corn { get => corn; set => corn = value; }
}
