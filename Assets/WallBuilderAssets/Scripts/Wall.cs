using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
    // list of stored bricks ready for serialization   
    public List<BrickHandler> placedBricks = new List<BrickHandler>();

    public void AddBrickToList(Color color, int scale, string next, int pId){
        placedBricks.Add(new BrickHandler (color.ToString(), scale, next, pId));
    }
}
