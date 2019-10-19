// by @torahhorse

using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour
{

	void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

    void Update()
    {
        // Lock when mouse is clicked
        if ( Input.GetMouseButtonDown(0) && Time.timeScale > 0.0f )
    	{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Unlock when escape is hit
        if  ( Input.GetKeyDown(KeyCode.Escape) )
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}