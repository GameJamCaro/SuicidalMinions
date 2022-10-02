using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public ModeController modeController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                var selectionRen = selection.GetComponentInChildren<SkinnedMeshRenderer>();
                var selectionScript = selection.GetComponent<MinionController>();
                if (selectionScript != null)
                {
                    if (modeController.mode == ModeController.Mode.ChangeDir)
                    {
                        selectionScript.ChangeDir();
                    }
                    if (modeController.mode == ModeController.Mode.Comfort)
                    {
                        selectionScript.Comfort();
                    }
                    if (modeController.mode == ModeController.Mode.Jump)
                    {
                        selectionScript.Jump();
                    }
                }
            }
        }

    }
}
