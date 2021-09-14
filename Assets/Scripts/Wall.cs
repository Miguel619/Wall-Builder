using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
    
    public List<BrickHandler> placedBricks = new List<BrickHandler>();

    public void AddBrickToList(Color color, int scale, string next){
        placedBricks.Add(new BrickHandler (color.ToString(), scale, next));
    }
}
