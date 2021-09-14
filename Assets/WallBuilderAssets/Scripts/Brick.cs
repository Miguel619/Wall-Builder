
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;


public class Brick : MonoBehaviour
{
    public GameObject snap;
    public Color color;
    public int scale = 1;
    public Vector3 localScale;
    private Renderer brickRenderer;
    public int id;

    private List<Snap> snaps = new List<Snap>();
    
    private GameObject preBrick;
    // store surrounding bricks
    public Brick bottom = null;
    public Brick right = null;
    public Brick left = null;
    
    public bool hasRight = false;
    public bool hasLeft = false;
    public bool hasTop = false;

    void Awake()
    {
        // get a reference to the preview brick
        preBrick = GameObject.Find("AR Session Origin").GetComponent<BuildWall>().brick;
        // set the initial color of the brick white
        color = Color.white;
        brickRenderer = gameObject.GetComponent<Renderer>();
        brickRenderer.material.color = color;
        // set initial scale to 1
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
    // for adding snap points to bricks
    public void addSnaps(){
        // we will use the position of the brick to place snap points
        Vector3 brickPos = gameObject.transform.position;
        GameObject curSnap;
        // above level 0 we will mainly be adding snaps upwards
        if(this.hasTop == false){
            curSnap = Instantiate(snap, brickPos, gameObject.transform.rotation);
            curSnap.transform.parent = gameObject.transform;
            // use the collider height to place the snap point on top of the brick
            curSnap.transform.localPosition  += new Vector3(0, (gameObject.GetComponent<Collider>().bounds.size.y * gameObject.transform.localScale.y) + .8f, 0);
            
            curSnap.GetComponent<Snap>().isTop = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        // add right snap point if there is no brick to the right
        if(this.bottom == null && this.hasRight == false){
            curSnap = Instantiate(snap, brickPos, gameObject.transform.rotation);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.localPosition  += new Vector3(1, 0, 0);
            curSnap.GetComponent<Snap>().isRight = true;
            snaps.Add(curSnap.GetComponent<Snap>());
        }
        // add left snap point if there is no brick to the left
        if(this.bottom == null && this.hasLeft == false){
            curSnap = Instantiate(snap, brickPos, gameObject.transform.rotation);
            curSnap.transform.parent = gameObject.transform;
            curSnap.transform.localPosition  += new Vector3(-1, 0, 0);
            curSnap.GetComponent<Snap>().isLeft = true;
            snaps.Add(curSnap.GetComponent<Snap>());
            
        }
        
    }
    public Brick addTopBrick(Color preColor){
        // use this.brick's position  to place the next brick
        Vector3 brickPos = gameObject.transform.position;
        GameObject topBrick = Instantiate(preBrick, brickPos, gameObject.transform.rotation);
        // make the brick a child of the wall
        topBrick.transform.parent = gameObject.transform.parent;
        // take into consideration differences in scale
        float difPos =  topBrick.transform.localScale.y - gameObject.transform.localScale.y;
        topBrick.transform.localPosition  += new Vector3(0, gameObject.GetComponent<Collider>().bounds.size.y + difPos/2, 0);
        topBrick.GetComponent<Brick>().setColor(preColor);
        topBrick.GetComponent<Brick>().bottom = this;
        // note this.brick now has top
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
        // note this.brick now has a brick to the right
        this.hasRight = true;
        this.right = rightBrick.GetComponent<Brick>();
        rightBrick.GetComponent<Brick>().addSnaps();
        return this.right;
    }
    public Brick addLeftBrick(Color preColor){
        Vector3 brickPos = gameObject.transform.position;
        GameObject leftBrick = Instantiate(preBrick, brickPos, gameObject.transform.rotation);
        leftBrick.transform.parent = gameObject.transform.parent;
        float difPos =  leftBrick.transform.localScale.y - gameObject.transform.localScale.y;
        leftBrick.transform.localPosition  += new Vector3(-.05f, difPos/2, 0);
        leftBrick.GetComponent<Brick>().setColor(preColor);
        leftBrick.GetComponent<Brick>().hasRight = true;
        // note this.brick now has a brick to the left
        this.hasLeft = true;
        leftBrick.GetComponent<Brick>().addSnaps();
        this.left = leftBrick.GetComponent<Brick>();
        return this.left;
    }
    public List<Snap> getSnaps(){
        return snaps;
    }
}
