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
        GameObject brick;
        [SerializeField]
        public List<Snap> wallSnaps = new List<Snap>();
        [SerializeField]
        private Color activeColor = Color.red;
        [SerializeField]
        private Color inactiveColor = Color.gray;
        [SerializeField]
        private Camera arCamera;
        
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
        public GameObject spawnedObject { get; private set; }
        public GameObject spawnedChild { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

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

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    spawnedChild = Instantiate(brick, hitPose.position, hitPose.rotation);
                    spawnedChild.GetComponent<Renderer>().material.color = brick.GetComponent<Brick>().color;
                    spawnedChild.transform.parent = spawnedObject.transform;
                    Brick newBrick = spawnedChild.GetComponent<Brick>();
                    newBrick.addSnaps();
                    List<Snap> newSnaps = newBrick.getSnaps();
                    foreach(Snap s in newSnaps){
                        wallSnaps.Add(s);
                    }
                    addBrick(wallSnaps[0]);

                }
                
            }else if(spawnedObject != null){
                Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject)){
                    Snap snapObject = hitObject.transform.GetComponent<Snap>();
                    if(snapObject != null){
                        addBrick(snapObject);
                    }
                }
            }
        }

        void addBrick(Snap snapObject){
            foreach (Snap cur in wallSnaps){
                MeshRenderer meshRen = cur.GetComponent<MeshRenderer>();
                if(snapObject != cur){
                    cur.isSelected = false;
                    meshRen.material.color = inactiveColor;
                }else{
                    cur.isSelected = true;
                    meshRen.material.color = activeColor;
                }
            }
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
