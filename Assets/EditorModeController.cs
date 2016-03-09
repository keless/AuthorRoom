using UnityEngine;
using System.Collections;

public class EditorModeController : CommonMonoBehavior {

    public enum Mode
    {
        Text,
        Navigation
    }
    private Mode _mode;
    public Mode mode {
        get { return _mode; }
    }

	public bool textMode_inTextField;

	// Use this for initialization
	void Start () {
        _mode = Mode.Text;
		textMode_inTextField = false;

		SetListener ("escPressed", onNavSwitchPressed);
		SetListener ("rmbPressed", onRMBPressed);

		SetListener ("textFieldEntered", onTextFieldEntered);
		SetListener ("textFieldExited", onTextFieldExited);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void onTextFieldEntered(EventObject evt)
	{
		if (_mode == Mode.Text) {
			textMode_inTextField = true;
		}
	}

	void onTextFieldExited(EventObject evt)
	{
		if (_mode == Mode.Text) {
			textMode_inTextField = false;
		}
	}

	void onRMBPressed(EventObject evt)
	{
		Debug.Log ("onrmbpress");
		if (_mode == Mode.Text) {
			if (!textMode_inTextField) {
				//pressed 'rmb' while in text mode, but outside of text field-- go to nav mode
				setMode (Mode.Navigation);
			}
		} else {
			setMode (Mode.Text);
		}
	}

    void onNavSwitchPressed(EventObject evt)
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
