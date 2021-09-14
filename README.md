# Wall Builder

This is a simple wall builder AR app built on [*AR Foundation 4.2*](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.2/manual/index.html).
**ONLY TESTED ON IOS!**


## Instructions for running Wall Builder

1. Clone the latest version of the github repo.

2. Open Unity, and load the project at the root of the *Wall-Builder* repository.

3. Open the WallBuilder scene.

4. Build the App onto Xcode and Run application.


## Wall Build

This app includes options for changing the size and color of your brick. A preview of your brick is displayed in front of your display.

When a plane is detected, you can tap on the detected plane to place a brick on it. Once the initial brick is placed, you are able to add different bricks on top and on the sides of the placed bricks.

Once you have place the initial brick you are able to save your brick into a JSON file by pressing the save button.
# Wall Builder Assests
| File | Description |
| ------ | ------- |
| Brick  | This class is used to store the key of every brick such as color, scale, id, and neighbors. It contains essential methods for bricks such as addSnaps which adds new snap points as appropriate and addBrick functions that instantiate new bricks where snap points are clicked. |
| BrickHandler | This class stores serializable data used to convert the brick data into a JSON. |
| BuildWall | This is the main Game Manager used to detect taps and react with the appropriate response. In this class we store a list with all the snap points as well as a referce to the preview brick we use to instantiate the correct color and scale brick. Through this class we communicate with the UI and the brick, wall, and snap class |
| FileHandler | *Coming soon*. |
| Load | This class is used for all file handling such and converting our list to a json and saving it to a file. Credit to [Velvary](https://youtu.be/KZft1p8t2lQ) for their helpful video. |
| MenuManager | This class stores all the methods for navigating the UI menu as well as button functionality of the UI. |
| Panel | This class allows us to treat every canvas as a panel that is a part of our array of panels. |
| Snap | This class helps determine where we will be spawning our brick when the snap point is tapped. |
| Wall | This class keeps a list of all serializable bricks and has a method for adding bricks using it's constructor. |

![Alt Text](https://media.giphy.com/media/i1UjdVcwcLBh8Z1dOL/giphy-downsized-large.gif?cid=790b76113ffdc8338a6662a0d4674505b39083c2f8437873&rid=giphy-downsized-large.gif&ct=g)![Alt Text](https://media.giphy.com/media/fE12z05tDON5LDa2f6/giphy-downsized-large.gif?cid=790b76118074c7c1f26cd8035635cd88af2d7266eb94c777&rid=giphy-downsized-large.gif&ct=g)
![Alt Text](https://media.giphy.com/media/8BDC69mfp0ZJ381teR/giphy-downsized-large.gif?cid=790b76113f0d6e9e7464722440747743d25050c083b23aff&rid=giphy-downsized-large.gif&ct=g)




