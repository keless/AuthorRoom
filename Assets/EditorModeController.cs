using UnityEngine;
using System.Collections;

public class EditorModeController : MonoBehaviour {

    public enum Mode
    {
        Text,
        Navigation
    }
    private Mode _mode;
    public Mode mode {
        get { return _mode; }
    }

	// Use this for initialization
	void Start () {
        _mode = Mode.Text;

        EventBus.ui.addListener("escPressed", onNavSwitchPressed);
	}
   
    void OnDestroy()
    {
        EventBus.ui.removeListener("escPressed", onNavSwitchPressed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onNavSwitchPressed(EventObject evt)
    {
        setMode(_mode == Mode.Text ? Mode.Navigation : Mode.Text);
    }

    public void setMode(Mode mode)
    {
        leaveMode(_mode);
        enterMode(mode);
    }

    void leaveMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Text:
                // send message to hide text
                EventBus.game.dispatch(new EventObject("dismissTextPanel"));
                break;
            case Mode.Navigation:
                EventBus.game.dispatch(new EventObject("disableFPSCamera"));
                break;
        }
    }
    void enterMode(Mode mode)
    {
        _mode = mode;
        switch (mode)
        {
            case Mode.Text:
                EventBus.game.dispatch(new EventObject("enableTextPanel"));
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case Mode.Navigation:
                EventBus.game.dispatch(new EventObject("enableFPSCamera"));
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
}
