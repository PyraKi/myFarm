using System;

public class Land
{
    private Crops crops;
    private int sprite;
    private DateTime harvestTime;
    private DateTime wateringTime;

    public Land(Crops crops, int sprite, DateTime harvestTime, DateTime wateringTime)
    {
        this.crops = crops;
        this.sprite = sprite;
        this.harvestTime = harvestTime;
        this.wateringTime = wateringTime;
    }


    public Crops Crops { get => crops; set => crops = value; }
    public int Sprite { get => sprite; set => sprite = value; }
    public DateTime HarvestTime { get => harvestTime; set => harvestTime = value; }
    public DateTime WateringTime { get => wateringTime; set => wateringTime = value; }
}
