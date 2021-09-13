using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject snap;
    public Color color;
    public Vector3 localScale;
    private Renderer brickRenderer;
    private List<Snap> snaps = new List<Snap>();
    
    


    void Start()
    {
        brickRenderer = gameObject.GetComponent<Renderer>();
        color = Color.white;
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
    public void addSnaps(Quaternion wallRotation){
        Vector3 brickPos = gameObject.transform.position;
        GameObject topSnap = Instantiate(snap, brickPos, wallRotation);
        topSnap.transform.parent = gameObject.transform;
        topSnap.transform.position += new Vector3(0, .05f, 0);
        snaps.Add(topSnap.GetComponent<Snap>());
        topSnap = Instantiate(snap, brickPos, wallRotation);
        topSnap.transform.parent = gameObject.transform;
        topSnap.transform.position += new Vector3((gameObject.transform.localScale.x / 2) + .05f, 0, 0);
        snaps.Add(topSnap.GetComponent<Snap>());
        topSnap = Instantiate(snap, brickPos, wallRotation);
        topSnap.transform.parent = gameObject.transform;
        topSnap.transform.position += new Vector3(-(gameObject.transform.localScale.x / 2) - .05f, 0, 0);
        snaps.Add(topSnap.GetComponent<Snap>());
    }
    public List<Snap> getSnaps(){
        return snaps;
    }
}
