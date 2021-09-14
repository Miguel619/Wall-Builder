using System;
[Serializable]
public class BrickHandler
{
    // store color, scale, direction for parent, and the parents id
    // ideally we would look for the parent and add the brick to it with the color, scale, and direction
    public string color;
    public int scale;
    public string parentDirection;
    public int pId;

    public BrickHandler(string color, int scale, string pDir, int parentId){
        this.color = color;
        this.scale = scale;
        this.parentDirection = pDir;
        this.pId = parentId;
    }
}
