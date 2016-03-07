using UnityEngine;
using System.Collections;

public class TextEditorCameraController : MonoBehaviour {

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    bool active = false;
    CharacterController pCharacterController = null; 

    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        pCharacterController = GetComponent<CharacterController>();

        EventBus.game.addListener("disableFPSCamera", onDisableCamera);
        EventBus.game.addListener("enableFPSCamera", onEnableCamera);
    }

    void OnDestroy()
    {
        EventBus.game.removeListener("disableFPSCamera", onDisableCamera);
        EventBus.game.removeListener("enableFPSCamera", onEnableCamera);
    }

    void Update()
    {
        if (!active) return;

        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }


        Vector3 forwardVel = transform.TransformDirection(Vector3.forward);
        Vector3 rightVel = transform.TransformDirection(Vector3.right);
        var vX = Input.GetAxis("Vertical") * 10;
        var vZ = Input.GetAxis("Horizontal") * -10;
        forwardVel *= vX;
        rightVel *= -vZ;

        pCharacterController.SimpleMove(forwardVel + rightVel);
        
    }

    void onDisableCamera(EventObject evt)
    {
        Debug.Log("disable camera control");
        active = false;
        pCharacterController.enabled = false;
    }
    void onEnableCamera(EventObject evt)
    {
        Debug.Log("enable camera control");
        active = true;
        pCharacterController.enabled = true;
    }
}