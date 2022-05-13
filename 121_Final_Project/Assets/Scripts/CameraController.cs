using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles camera movement, is the script used in the group project
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseMovement = 200; //[SerializedField] attribute used to show private variable in the inspector-- controls how fast we want to rotate
    //[SerializeField] private float _cameraRotationLock = 80; //Added by Patrick, variable to lock vertical rotation

    private Transform parent; //reference to our parent object
    private Camera _fpsCamera;


    private void Start()
    {
        _fpsCamera = Camera.main; //Accessing our object camera
        parent = transform.parent; //the parent of our object is the object we want to rotate
        Cursor.lockState = CursorLockMode.Locked; //locks mouse to the center of the screen
    }

    private void Update()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * _mouseMovement * Time.deltaTime; //horizontal rotation calculation
        float verticalRotation = Input.GetAxis("Mouse Y") * _mouseMovement * Time.deltaTime; //vertical rotation calculation


        parent.Rotate(0, horizontalRotation, 0); //rotate parent around the vertical axis-- horizontal movement
        _fpsCamera.transform.Rotate(-verticalRotation, 0, 0); //rotate camera around the horizontal axis-- vertical movement
    }
}
