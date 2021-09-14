
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;


public class Brick : MonoBehaviour
{
    // Brick data variables
    public int id;
    public int scale = 1;
    public Color color;
    public Vector3 localScale;
    private Renderer brickRenderer;
    // Reference to preview brick
    private GameObject preBrick;
    
    // Snap point data
    public GameObject snap;
    private List<Snap> snaps = new List<Snap>();
    
    // Surrounding bricks
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

    //                  [Adding Bricks]
    public Brick addTopBrick(Color preColor){
        GameObject topBrick = createNewBrick(preColor);
        // take into consideration differences in scale
        float difPos =  topBrick.transform.localScale.y - gameObject.transform.localScale.y;
        topBrick.transform.localPosition  += new Vector3(0, gameObject.GetComponent<Collider>().bounds.size.y + difPos/2, 0);
        // Keep note of brick pos
        Brick brickClass = topBrick.GetComponent<Brick>();
        brickClass.bottom = this;
        this.hasTop = true;
        // Add Snaps
        brickClass.addSnaps();
        return brickClass;
    }

    public Brick addRightBrick(Color preColor){
        GameObject rightBrick = createNewBrick(preColor);
        // take into consideration differences in scale
        float difPos =  rightBrick.transform.localScale.y - gameObject.transform.localScale.y;
        rightBrick.transform.localPosition  += new Vector3(.05f, difPos/2, 0);
        // Keep note of brick pos
        Brick brickClass = rightBrick.GetComponent<Brick>();
        brickClass.hasLeft = true;
        this.hasRight = true;
        this.right = brickClass;
        // Add Snaps
        brickClass.addSnaps();
        return this.right;
    }

    public Brick addLeftBrick(Color preColor){
        GameObject leftBrick = createNewBrick(preColor);
        // take into consideration differences in scale
        float difPos =  leftBrick.transform.localScale.y - gameObject.transform.localScale.y;
        leftBrick.transform.localPosition  += new Vector3(-.05f, difPos/2, 0);
        // Keep note of brick pos
        Brick brickClass = leftBrick.GetComponent<Brick>();
        brickClass.hasRight = true;
        this.hasLeft = true;
        // add snaps
        brickClass.addSnaps();
        this.left = brickClass;
        return this.left;
    }
    // Create a new brick that is a child of the wall
    private GameObject createNewBrick(Color preColor){
        GameObject newBrick = Instantiate(preBrick, gameObject.transform.position, gameObject.transform.rotation);
        // make the brick a child of the wall
        newBrick.transform.parent = gameObject.transform.parent;
        newBrick.GetComponent<Brick>().setColor(preColor);
        return newBrick;
    }
    
    // Setters
    public void setColor(Color color){
        this.color = color;
        brickRenderer.material.color = color;
    }

    public void setScale(Vector3 scale, int numScale){
        this.localScale = scale;
        gameObject.transform.localScale = scale;
        this.scale = numScale;
    }

    // Getters 
    public List<Snap> getSnaps(){
        return snaps;
    }
}