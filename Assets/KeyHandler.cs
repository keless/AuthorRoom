using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyHandler : MonoBehaviour {

	bool RMB_pressTrigger;

	// Use this for initialization
	void Start () {
		RMB_pressTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
        {
            // tab pressed
            EventBus.ui.dispatch(new EventObject("escPressed"));
        }

        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.F1.ToString())))
        {
            //hax
            SceneManager.LoadScene(1);
        }
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.F2.ToString())))
        {
            //hax
            SceneManager.LoadScene(2);
        }
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.F3.ToString())))
        {
            //hax
            SceneManager.LoadScene(3);
        }

		if (Input.GetMouseButtonDown (1)) {
			if (!RMB_pressTrigger) {
				EventBus.ui.dispatch (new EventObject ("rmbPressed"));
				RMB_pressTrigger = true;
			}
		} else {
			RMB_pressTrigger = false;
		}
    }
}
