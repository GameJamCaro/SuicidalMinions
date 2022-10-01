using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public enum Mode {ChangeDir, Comfort, Jump};
    public Mode mode;
    public Image[] buttonImages;


    public void ChangeDirMode()
    {
        mode = Mode.ChangeDir;
        ResetImages();
        buttonImages[0].color = Color.yellow;

        
    }

    public void ComfortMode()
    {
        mode = Mode.Comfort;
        ResetImages();
        buttonImages[1].color = Color.yellow;
    }


    public void JumpMode()
    {
        mode = Mode.Jump;
        ResetImages();
        buttonImages[2].color = Color.yellow;
    }

    void ResetImages()
    {
        foreach (var image in buttonImages)
        {
            image.color = Color.white;
        }
    }
}
