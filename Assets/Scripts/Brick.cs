using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Color color = Color.white;
    public Vector3 localScale;
    private Renderer brickRenderer;


    void Start()
    {
        brickRenderer = gameObject.GetComponent<Renderer>();
        brickRenderer.material.color = color;
        localScale = gameObject.transform.localScale;
    }

    public void setColor(Color color){
        this.color = color;
        brickRenderer.material.color = color;
    }

    public void setScale(Vector3 scale){
        this.localScale = scale;
        gameObject.transform.localScale = scale;
    }
}
