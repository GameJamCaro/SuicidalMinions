using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public enum Mode {NoMode, ChangeDir, Comfort, Jump};
    public Mode mode;
    public Image[] buttonImages;
    Colors colors;

    private void Start()
    {
        colors = GetComponent<Colors>();
        ResetImages();
       

    }

    public void ChangeDirMode()
    {
        mode = Mode.ChangeDir;
        ResetImages();
        buttonImages[0].color = colors.active;

        
    }

    public void ComfortMode()
    {
        Debug.Log("Comfort");
        mode = Mode.Comfort;
        ResetImages();
        buttonImages[1].color = colors.active;
    }


    public void JumpMode()
    {
        mode = Mode.Jump;
        ResetImages();
        buttonImages[2].color = colors.active;
    }

    void ResetImages()
    {
        foreach (var image in buttonImages)
        {
            image.color = colors.inactive;
        }
    }
}
