using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation.Samples;

public class MenuManager : MonoBehaviour
{
    public Panel currentPanel;
    public Brick previewBrick;
    private List<Panel> panelHistory = new List<Panel>();

    private void Start(){
        SetupPanels();
    }

    private void SetupPanels(){
        Panel[] panels = GetComponentsInChildren<Panel>();

        foreach(Panel panel in panels)
            panel.Setup(this);
        currentPanel.Show();
    }


    public void GoToPrevious(){
        int lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
        Debug.Log("Previous panel is active");
    }

    public void SetCurrentWithHistory(Panel newPanel){
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
        Debug.Log(currentPanel + " is now Active");
    }

    private void SetCurrent(Panel newPanel){
        currentPanel.Hide();
        currentPanel = newPanel;
        currentPanel.Show();
    }
    public void doExitGame() {
     Application.Quit();
    }
    
    public void sceneTransition(string newScene){ 
        SceneManager.LoadScene(newScene);
    }
    public void colorPicker(string color){
        if(color == "Red"){
            previewBrick.setColor(Color.red);
        }
        if(color == "Blue"){
            previewBrick.setColor(Color.blue);
        }
        if(color == "Green"){
            previewBrick.setColor(Color.green);
        }
        if(color == "Black"){
            previewBrick.setColor(Color.black);
        }
        if(color == "White"){
            previewBrick.setColor(Color.white);
        }
    }
    public void scaleSelector(int scale){
        if(scale == 1){
            previewBrick.setScale(new Vector3(.05f, .05f, .05f));
        }
        if(scale == 2){
            previewBrick.setScale(new Vector3(.05f, .075f, .05f));
        }
        if(scale == 3){
            previewBrick.setScale(new Vector3(.05f, .1f, .05f));
        }
        if(scale == 4){
            previewBrick.setScale(new Vector3(.05f, .15f, .05f));
        }
        if(scale == 5){
            previewBrick.setScale(new Vector3(.05f, .2f, .05f));
        }
    }

    public void addSnap(){
        previewBrick.addLeftBrick(Color.red);
    }
    
}