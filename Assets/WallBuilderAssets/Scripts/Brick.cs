
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
    public Brick right = null;
    public Brick left = null;
    public int scale = 1;
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

    public void setScale(Vector3 scale, int numScale){
        this.localScale = scale;
        gameObject.transform.localScale = scale;
        this.scale = numScale;
    }
    public void addSnaps(){
        Vector3 brickPos = gameObject.transform.position;
        GameObject curSnap;
        // top snap
        if(this.hasTop == false){
            curSnap = Instantiate(snap, brickPos, gameObject.transform.rotation);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.localPosition  += new Vector3(0, (gameObject.GetComponent<Collider>().bounds.size.y * gameObject.transform.localScale.y) + .8f, 0);
            
            curSnap.GetComponent<Snap>().isTop = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        if(this.hasRight == false){
            // right snap
            if(this.bottom == null){
                curSnap = Instantiate(snap, brickPos, gameObject.transform.rotation);
                curSnap.transform.parent = gameObject.transform;

                curSnap.transform.localPosition  += new Vector3(1, 0, 0);
                curSnap.GetComponent<Snap>().isRight = true;
                snaps.Add(curSnap.GetComponent<Snap>());
            }
            
        }
        if(this.hasLeft == false){
            // left snap
            if(this.bottom == null){
                
                curSnap = Instantiate(snap, brickPos, gameObject.transform.rotation);
                curSnap.transform.parent = gameObject.transform;
                curSnap.transform.localPosition  += new Vector3(-1, 0, 0);
                curSnap.GetComponent<Snap>().isLeft = true;
                snaps.Add(curSnap.GetComponent<Snap>());
            }
            
        }
        
    }
    public Brick addTopBrick(Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        // top brick 
        GameObject topBrick = Instantiate(preBrick, brickPos, gameObject.transform.rotation);
        topBrick.transform.parent = gameObject.transform.parent;
        float difPos =  topBrick.transform.localScale.y - gameObject.transform.localScale.y;
        topBrick.transform.localPosition  += new Vector3(0, gameObject.GetComponent<Collider>().bounds.size.y + difPos/2, 0);
        topBrick.GetComponent<Brick>().setColor(preColor);
        topBrick.GetComponent<Brick>().bottom = this;
        this.hasTop = true;
        topBrick.GetComponent<Brick>().addSnaps();
        return topBrick.GetComponent<Brick>();
    }
    public Brick addRightBrick(Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        
        GameObject rightBrick = Instantiate(preBrick, brickPos, gameObject.transform.rotation);
       
        rightBrick.transform.parent = gameObject.transform.parent;
        
        float difPos =  rightBrick.transform.localScale.y - gameObject.transform.localScale.y;
        rightBrick.transform.localPosition  += new Vector3(.05f, difPos/2, 0);
        rightBrick.GetComponent<Brick>().setColor(preColor);
        rightBrick.GetComponent<Brick>().hasLeft = true;
        this.hasRight = true;
        this.right = rightBrick.GetComponent<Brick>();
        rightBrick.GetComponent<Brick>().addSnaps();
        return this.right;
    }
    public Brick addLeftBrick(Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        // top brick 
        GameObject leftBrick = Instantiate(preBrick, brickPos, gameObject.transform.rotation);
        leftBrick.transform.parent = gameObject.transform.parent;
        
        float difPos =  leftBrick.transform.localScale.y - gameObject.transform.localScale.y;
        leftBrick.transform.localPosition  += new Vector3(-.05f, difPos/2, 0);
        leftBrick.GetComponent<Brick>().setColor(preColor);
        leftBrick.GetComponent<Brick>().hasRight = true;
        this.hasLeft = true;
        leftBrick.GetComponent<Brick>().addSnaps();
        this.left = leftBrick.GetComponent<Brick>();
        return this.left;
    }
    public List<Snap> getSnaps(){
        return snaps;
    }
}
