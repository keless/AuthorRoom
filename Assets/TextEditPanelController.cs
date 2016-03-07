using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextEditPanelController : MonoBehaviour {

    public UnityEngine.UI.Dropdown dropPlaces;
    string[] placeNames = { "Mountain", "Woods", "Ruined City" };
    string[] placePaths = { "Assets/Scenes/Mountain1.unity", "Assets/Scenes/Nature1.unity", "Assets/Scenes/PostApoc1.unity" };
    int firstSceneIdx = 1;

    protected FileBrowser _fileBrowser;

    // Use this for initialization
    void Start () {

        EventBus.game.addListener("dismissTextPanel", onDismissPanel);
        EventBus.game.addListener("enableTextPanel", onEnablePanel);

        EventBus.ui.addListener("btnLoad", onBtnLoad);
        EventBus.ui.addListener("btnSave", onBtnSave);
    }

    void OnDestroy()
    {
        EventBus.game.removeListener("dismissTextPanel", onDismissPanel);
        EventBus.game.removeListener("enableTextPanel", onEnablePanel);
        EventBus.ui.removeListener("btnLoad", onBtnLoad);
        EventBus.ui.removeListener("btnSave", onBtnSave);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnLevelWasLoaded(int level)
    {
        level -= firstSceneIdx;
        Debug.Log("onlevelwasloaded " + level);

        //todo: fill dropdown
        List<string> places = new List<string>();

        
        Debug.Log(  SceneManager.GetActiveScene().name + " : " + SceneManager.GetActiveScene().path);

        for( var i=0; i<placeNames.Length; i++)
        {
            places.Add(placeNames[i]);
        }

        dropPlaces.AddOptions(places);
        dropPlaces.value = level; //set currently selected level as selected name
        dropPlaces.onValueChanged.AddListener(onDropPlaces);
    }
    void onDropPlaces(int idx)
    {
        var currentIdx = System.Array.IndexOf(placePaths, SceneManager.GetActiveScene().path);
        if( currentIdx > -1 && currentIdx != idx )
        {
            SceneManager.LoadScene(idx + firstSceneIdx);
        }
    }

    void onDismissPanel(EventObject e)
    {
        gameObject.SetActive(false);
    }
    void onEnablePanel(EventObject e)
    {
        gameObject.SetActive(true);
    }

    void onBtnLoad(EventObject e)
    {
        Debug.Log("todo: btn load");
        if (_fileBrowser != null) return;


    }

    void onBtnSave(EventObject e)
    {
        Debug.Log("todo: btn save");
        if (_fileBrowser != null) return;

        /*
        _fileBrowser = new FileBrowser(
                    new Rect(100, 100, 600, 500),
                    "Choose Text File",
                    FileSelectedCallback
                );
        */

        InputField inputField = GetComponentInChildren<InputField>();
        Debug.Assert(inputField);

        using (StreamWriter sw = new StreamWriter("text.txt"))
        {
            sw.Write(inputField.text);
        }
    }

    protected void FileSelectedCallback(string path)
    {
        _fileBrowser = null;
        string textPath = path;
    }

    void OnGUI()
    {
        if(_fileBrowser != null)
        {
            _fileBrowser.OnGUI();
        }
    }
}
