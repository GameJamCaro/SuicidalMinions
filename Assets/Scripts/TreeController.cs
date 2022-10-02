using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    Vector3 rotation;
    public int rotationId; 
    public ModeController modeController;

    private void Start()
    {
       rotation = transform.rotation.eulerAngles;
    }
    public void ChangeDir()
    {
        if (rotationId == 0)
        {
            if (transform.localRotation.eulerAngles.y == 90)
            {
                transform.localRotation = Quaternion.Euler(rotation.x, -90, rotation.z);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(rotation.x, 90, rotation.z);
            }
        }
        else
        {
            if (transform.localRotation.eulerAngles.z == 90)
            {
                transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, -90);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 90);
            }
        }
    }

    private void OnMouseDown()
    {
        if(modeController.mode == ModeController.Mode.ChangeDir)
        ChangeDir();
    }
}
