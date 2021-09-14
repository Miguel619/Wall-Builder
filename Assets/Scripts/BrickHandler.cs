using System;
[Serializable]
public class BrickHandler
{
    public string color;
    public int scale;
    public string nextAdd;

    public BrickHandler(string color, int scale, string next){
        this.color = color;
        this.scale = scale;
        this.nextAdd = next;
    }
}
