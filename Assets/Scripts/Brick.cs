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
            curSnap.transform.localPosition  += new Vector3(0, .05f, 0);
            curSnap.GetComponent<Snap>().isTop = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        if(this.hasRight == false){
            // right snap
            curSnap = Instantiate(snap, brickPos, Quaternion.identity);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.localPosition  += new Vector3((gameObject.transform.localScale.x / 2) + .05f, 0, 0);
            curSnap.GetComponent<Snap>().isRight = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        if(this.hasLeft == false){
            // left snap
            curSnap = Instantiate(snap, brickPos, Quaternion.identity);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.localPosition  += new Vector3(-(gameObject.transform.localScale.x / 2) - .05f, 0, 0);
            curSnap.GetComponent<Snap>().isLeft = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        
    }
    public Brick addTopBrick(Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        // top brick 
        GameObject topBrick = Instantiate(preBrick, brickPos, Quaternion.identity);
        topBrick.transform.parent = gameObject.transform.parent;
        
        topBrick.transform.localPosition  += new Vector3(0, .05f, 0);
        topBrick.GetComponent<Brick>().setColor(preColor);
        topBrick.GetComponent<Brick>().bottom = this;
        this.hasTop = true;
        topBrick.GetComponent<Brick>().addSnaps();
        return topBrick.GetComponent<Brick>();
    }
    public Brick addRightBrick(Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        
        GameObject rightBrick = Instantiate(preBrick, brickPos, Quaternion.identity);
       
        rightBrick.transform.parent = gameObject.transform.parent;
        rightBrick.transform.localPosition  += new Vector3((rightBrick.transform.localScale.x / 2) + .05f, 0, 0);
        rightBrick.GetComponent<Brick>().setColor(preColor);
        rightBrick.GetComponent<Brick>().hasLeft = true;
        this.hasRight = true;
        rightBrick.GetComponent<Brick>().addSnaps();
        return rightBrick.GetComponent<Brick>();
    }
    public Brick addLeftBrick(Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        // top brick 
        GameObject leftBrick = Instantiate(preBrick, brickPos, Quaternion.identity);
        leftBrick.transform.parent = gameObject.transform.parent;
        leftBrick.transform.localPosition  += new Vector3(-(leftBrick.transform.localScale.x / 2) - .05f, 0, 0);
        leftBrick.GetComponent<Brick>().setColor(preColor);
        leftBrick.GetComponent<Brick>().hasRight = true;
        this.hasLeft = true;
        leftBrick.GetComponent<Brick>().addSnaps();
        return leftBrick.GetComponent<Brick>();
    }
    public List<Snap> getSnaps(){
        return snaps;
    }
}
