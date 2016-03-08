using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputFieldEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public void OnPointerEnter(PointerEventData data) 
	{
		EventBus.ui.dispatch (new EventObject ("textFieldEntered"));
	}

	public void OnPointerExit(PointerEventData data) 
	{
		EventBus.ui.dispatch (new EventObject ("textFieldExited"));
	}

}
