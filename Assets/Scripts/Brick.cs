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
    public Brick bottom = null;
    public bool hasRight = false;

    public bool hasLeft = false;
    public bool hasTop = false;

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
    public void addSnaps(){
        Vector3 brickPos = gameObject.transform.position;
        GameObject curSnap;
        // top snap
        if(this.hasTop == false){
            curSnap = Instantiate(snap, brickPos, Quaternion.identity);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.position += new Vector3(0, .05f, 0);
            curSnap.transform.rotation = gameObject.transform.rotation;
            curSnap.GetComponent<Snap>().isTop = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        if(this.hasRight == false){
            // right snap
            curSnap = Instantiate(snap, brickPos, Quaternion.identity);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.position += new Vector3((gameObject.transform.localScale.x / 2) + .05f, 0, 0);
            curSnap.transform.rotation = gameObject.transform.rotation;
            curSnap.GetComponent<Snap>().isRight = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        if(this.hasLeft == false){
            // left snap
            curSnap = Instantiate(snap, brickPos, Quaternion.identity);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.position += new Vector3(-(gameObject.transform.localScale.x / 2) - .05f, 0, 0);
            curSnap.transform.rotation = gameObject.transform.rotation;
            curSnap.GetComponent<Snap>().isLeft = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        
    }
    public void addTopBrick(Quaternion wallRotation, Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        // top brick 
        GameObject topBrick = Instantiate(preBrick, brickPos, wallRotation);
        topBrick.transform.parent = gameObject.transform.parent;
        topBrick.transform.position += new Vector3(0, .05f, 0);
        topBrick.transform.rotation = gameObject.transform.rotation;
        topBrick.GetComponent<Brick>().setColor(preColor);
        topBrick.GetComponent<Brick>().bottom = this;
        this.hasTop = true;
        topBrick.GetComponent<Brick>().addSnaps();
    }
    public void addRightBrick(Quaternion wallRotation, Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        
        GameObject rightBrick = Instantiate(preBrick, brickPos, wallRotation);
       
        rightBrick.transform.parent = gameObject.transform.parent;
        rightBrick.transform.position += new Vector3((gameObject.transform.localScale.x / 2) + .05f, 0, 0);
        rightBrick.GetComponent<Brick>().setColor(preColor);
        rightBrick.GetComponent<Brick>().hasLeft = true;
        this.hasRight = true;
        rightBrick.GetComponent<Brick>().addSnaps();
    }
    public void addLeftBrick(Quaternion wallRotation, Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        // top brick 
        GameObject leftBrick = Instantiate(preBrick, brickPos, wallRotation);
        leftBrick.transform.parent = gameObject.transform.parent;
        leftBrick.transform.position += new Vector3(-(gameObject.transform.localScale.x / 2) - .05f, 0, 0);
        leftBrick.GetComponent<Brick>().setColor(preColor);
        leftBrick.GetComponent<Brick>().hasRight = true;
        this.hasLeft = true;
        leftBrick.GetComponent<Brick>().addSnaps();
    }
    public List<Snap> getSnaps(){
        return snaps;
    }
}
