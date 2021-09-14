using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class BuildWall : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
        [SerializeField]
        public GameObject brick;
        [SerializeField]
        public List<Snap> wallSnaps = new List<Snap>();
        
        [SerializeField]
        private ARSessionOrigin  origin;
        private Wall wallController;
        private PlaneDetectionController planeDetectionController;
        private Quaternion wallRotation;
        private List<Brick> storedBricks = new List<Brick>();
        
        
        private int index = 0;
        
        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedWall { get; private set; }
        public GameObject spawnedBrick { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            planeDetectionController = GetComponent<PlaneDetectionController>();
            
        }
        // private void Start() {
        //     // For testing JSON
            
        //     wallRotation = Quaternion.Euler(0, origin.camera.transform.rotation.eulerAngles.y, 0);
        //     spawnedWall = Instantiate(m_PlacedPrefab, gameObject.transform.position, wallRotation);
        //     wallController = spawnedWall.GetComponent<Wall>();
        //     spawnedBrick = Instantiate(brick, gameObject.transform.position, wallRotation);
        //     spawnedBrick.GetComponent<Brick>().setColor(brick.GetComponent<Brick>().color);
        //     spawnedBrick.transform.parent = spawnedWall.transform;
        //     Brick newBrick = spawnedBrick.GetComponent<Brick>();
        //     newBrick.scale = brick.GetComponent<Brick>().scale;
        //     newBrick.addSnaps();
        //     List<Snap> newSnaps = newBrick.getSnaps();
        //     foreach(Snap s in newSnaps){
        //         wallSnaps.Add(s);
        //     }
        //     planeDetectionController.TogglePlaneDetection();
        //     // add brick to wall
        //     storedBricks.Add(newBrick);

        //     Brick addedBrick = newBrick.addTopBrick(brick.GetComponent<Brick>().color);
        //     List<Snap> ns = addedBrick.getSnaps();
        //     foreach(Snap s in ns){
        //         wallSnaps.Add(s);
        //     }
        //     // add brick to wall
        //     storedBricks.Add(addedBrick);
        //     wallController.AddBrickToList(storedBricks[index].color, storedBricks[index].scale, "up");
        //     index++;
        // }
        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;
            // if initial brick already spawned
            if (spawnedWall != null){
                Ray ray = origin.camera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject)){
                    Snap snapObject = hitObject.transform.GetComponent<Snap>();
                    if(snapObject != null){
                        addBrick(snapObject);
                    }
                }
            }// case for initial brick
            else if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;
                wallRotation = Quaternion.Euler(0, origin.camera.transform.rotation.eulerAngles.y, 0);
                // spawn wall
                spawnedWall = Instantiate(m_PlacedPrefab, hitPose.position, wallRotation);
                wallController = spawnedWall.GetComponent<Wall>();
                // spawn initial brick
                spawnedBrick = Instantiate(brick, hitPose.position, wallRotation);
                spawnedBrick.GetComponent<Brick>().setColor(brick.GetComponent<Brick>().color);
                // make brick child of wall
                spawnedBrick.transform.parent = spawnedWall.transform;
                Brick newBrick = spawnedBrick.GetComponent<Brick>();
                newBrick.scale = brick.GetComponent<Brick>().scale;
                // add snaps to new brick
                newBrick.addSnaps();
                // add snaps to our list of all snaps
                List<Snap> newSnaps = newBrick.getSnaps();
                foreach(Snap s in newSnaps){
                    wallSnaps.Add(s);
                }
                // turn off plane detection to make detecting snaps easier
                planeDetectionController.TogglePlaneDetection();
                // add bricks to our list of bricks and enable saving the wall
                storedBricks.Add(newBrick);
                newBrick.id = index;
                
            }
        }

        void addBrick(Snap snapObject){
            // Look for the snap in our list of snaps
            foreach (Snap cur in wallSnaps){
                if(snapObject == cur){
                    // get a reference to the parent
                    Brick parentBrick = cur.GetComponentInParent<Brick>();
                    Brick addedBrick;
                    // depending on the side of the snap, spawn a brick
                    if(cur.isTop){
                        addedBrick = parentBrick.addTopBrick(brick.GetComponent<Brick>().color);
                        processBrick(addedBrick, parentBrick, "up");
                    }else if(cur.isRight){
                        addedBrick = parentBrick.addRightBrick(brick.GetComponent<Brick>().color);
                        processBrick(addedBrick, parentBrick, "right");
                    }else if(cur.isLeft){
                        addedBrick = parentBrick.addLeftBrick(brick.GetComponent<Brick>().color);
                        processBrick(addedBrick, parentBrick, "left");
                    }
                    
                }
            }
        }
        private void processBrick(Brick addedBrick, Brick parentBrick, string dir){
            // add snaps to list of all snaps
            List<Snap> newSnaps = addedBrick.getSnaps();
            foreach(Snap s in newSnaps){
                wallSnaps.Add(s);
            }

            // add brick to list of bricks
            storedBricks.Add(addedBrick);
            // store brick in file handler for json serialization
            wallController.AddBrickToList(storedBricks[index].color, storedBricks[index].scale, dir, parentBrick.id);
            // give the brick a new id
            index++;
            addedBrick.id = index;
        }

        public void saveWall(){
            if(spawnedWall != null){
                wallController.AddBrickToList(storedBricks[index].color, storedBricks[index].scale, "null", index);
                FileHandler.SaveToJSON<BrickHandler>(wallController.placedBricks, "SavedWall");
            }
            
        }
        
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}