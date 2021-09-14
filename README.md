# Wall Builder

Built on [*AR Foundation 4.2*](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.2/manual/index.html).
ONLY TESTED ON IOS!


## Instructions for opening project

1. Download the latest version of Unity 2020.3 or later.

2. Open Unity, and load the project at the root of the *Wall-Builder* repository.

3. Open the WallBuilder scene.

4. Information for using coming soon.


## Wall Build

This is a good starting sample that enables point cloud visualization and plane detection. There are buttons on screen that let you pause, resume, reset, and reload the ARSession.

When a plane is detected, you can tap on the detected plane to place a cube on it. This uses the `ARRaycastManager` to perform a raycast against the plane.

| Action | Meaning |
| ------ | ------- |
| Pause  | Pauses the ARSession, meaning device tracking and trackable detection (e.g., plane detection) is temporarily paused. While paused, the ARSession does not consume CPU resources. |
| Resume | Resumes a paused ARSession. The device will attempt to relocalize and previously detected objects may shift around as tracking is reestablished. |
| Reset | Clears all detected trackables and effectively begins a new ARSession. |
| Reload | Completely destroys the ARSession GameObject and re-instantiates it. This simulates the behavior you might experience during scene switching. |

