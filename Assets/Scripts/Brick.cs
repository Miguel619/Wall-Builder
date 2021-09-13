using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class Brick : MonoBehaviour
{
    public GameObject snap;
    public Color color;
    public Vector3 localScale;
    private Renderer brickRenderer;
    private List<Snap> snaps = new List<Snap>();
    
    private GameObject preBrick;


    void Awake()
    {
        brickRenderer = gameObject.GetComponent<Renderer>();
        preBrick = GameObject.Find("AR Session Origin").GetComponent<BuildWall>().brick;
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
        // top snap
        GameObject curSnap = Instantiate(snap, brickPos, wallRotation);
        curSnap.transform.parent = gameObject.transform;
        curSnap.transform.position += new Vector3(0, .05f, 0);
        curSnap.GetComponent<Snap>().isTop = true;
        snaps.Add(curSnap.GetComponent<Snap>());
        // right snap
        curSnap = Instantiate(snap, brickPos, wallRotation);
        curSnap.transform.parent = gameObject.transform;
        curSnap.transform.position += new Vector3((gameObject.transform.localScale.x / 2) + .05f, 0, 0);
        curSnap.GetComponent<Snap>().isRight = true;
        snaps.Add(curSnap.GetComponent<Snap>());
        // left snap
        curSnap = Instantiate(snap, brickPos, wallRotation);
        curSnap.transform.parent = gameObject.transform;
        curSnap.transform.position += new Vector3(-(gameObject.transform.localScale.x / 2) - .05f, 0, 0);
        curSnap.GetComponent<Snap>().isLeft = true;
        snaps.Add(curSnap.GetComponent<Snap>());
    }
    public void addBrick(Quaternion wallRotation){
        Vector3 brickPos = gameObject.transform.position;
        // top brick 
        GameObject topBrick = Instantiate(preBrick, brickPos, wallRotation);
        topBrick.transform.parent = gameObject.transform.parent;
        topBrick.transform.position += new Vector3(0, .05f, 0);
        topBrick.GetComponent<Brick>().setColor(this.color);
        // right brick
        topBrick = Instantiate(preBrick, brickPos, wallRotation);
        topBrick.transform.parent = gameObject.transform.parent;
        topBrick.transform.position += new Vector3((gameObject.transform.localScale.x / 2) + .05f, 0, 0);
        topBrick.GetComponent<Brick>().setColor(this.color);
        // left brick
        topBrick = Instantiate(preBrick, brickPos, wallRotation);
        topBrick.transform.parent = gameObject.transform.parent;
        topBrick.transform.position += new Vector3(-(gameObject.transform.localScale.x / 2) - .05f, 0, 0);
        topBrick.GetComponent<Brick>().setColor(this.color);
    }
    public List<Snap> getSnaps(){
        return snaps;
    }
}
