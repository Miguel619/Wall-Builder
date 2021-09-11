using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}