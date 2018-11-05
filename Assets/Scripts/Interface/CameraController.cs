using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CameraController : MonoBehaviour {

    public Transform cam;
    [HideInInspector]
    public int current;
    public int max;
    public float cameraUpMovement;
    public float cameraUpRotation;
    public float camspeed;
    public float camRotationSpeed;
    public Texture2D cursorImage;
    public bool allowMovement;
    public GameObject escapeMenu;
    public TimeManager manager;

    public void Start()
    {
        Time.timeScale = 1;
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    public void Continue()
    {
        Time.timeScale = manager.slider.value;
        escapeMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Esc"))
        {
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0;
                escapeMenu.SetActive(true);
            }
            else if(escapeMenu.activeInHierarchy)
            {
                Time.timeScale = manager.slider.value;
                escapeMenu.SetActive(false);
            }
        }
        if (Time.timeScale != 0)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * Time.deltaTime * (Input.GetButton("Sprint") ? camspeed * 2 : camspeed));
            if (Input.GetButton("Fire3"))
            {
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * camRotationSpeed);
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
            {
                if (current == 0 && -Input.GetAxisRaw("Mouse ScrollWheel") < 0 || current == max && -Input.GetAxisRaw("Mouse ScrollWheel") > 0)
                {

                }
                else
                {
                    int add = (-Input.GetAxisRaw("Mouse ScrollWheel") > 0) ? 1 : -1;
                    current += add;
                    transform.position += new Vector3(0, cameraUpMovement, 0) * add;
                    cam.transform.Rotate(Vector3.right * cameraUpRotation * add);
                }
            }
        }
    }
}
