using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{

    public TextAsset[] textAssets;
    private Story story;
    public TextMeshProUGUI textUi1;
    public TextMeshProUGUI textUi2;
    int counter;
  
    string currentText;
    public AudioSource voiceSource;
    public AudioClip[] voices;
    WaitForSeconds wait;
    public GameObject continueButton;

    


    // Start is called before the first frame update
    void Start()
    {
        continueButton.SetActive(false);
        StartCoroutine(WaitAndIntroduce());

        SetStory(0);
        // textUi1.text = story.Continue();
        


    }

    void SetStory(int index)
    {
        story = new Story(textAssets[index].text);
    }

    public void Continue()
    {
        GetNextStoryBlock();
    }


    // Load and potentially return the next story block
    public string GetNextStoryBlock()
    {

       

        string text = "";

        if (story.canContinue)
        {
            

            if (counter % 2 == 1) 
            {
                
                    StopAllCoroutines();
                    textUi1.text = currentText;
                    currentText = story.Continue();

                    StartCoroutine(TypeSentence(currentText, textUi2));

                }
                else
                {
                    StopAllCoroutines();
                    textUi2.text = currentText;
                    currentText = story.Continue();
                    StartCoroutine(TypeSentence(currentText, textUi1));
                   
                }
            
            
            counter++;
           


        }
        else 
        {
            
            Debug.Log("Dialog over");
            continueButton.SetActive(false);
            
            
            


        }
       

        return text;
    }

    public IEnumerator TypeSentence(string sentence, TextMeshProUGUI textElement)
    {
        textElement.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textElement.text += letter;
            float time = Random.Range(0.01f, 0.1f);
            yield return new WaitForSeconds(time);
            
            if (counter % 2 == 1)
            {
               
                    voiceSource.clip = voices[0];
                    voiceSource.pitch = Random.Range(.5f, 1.5f);
                    voiceSource.Play();
                
            }
            else
            {
                int count = 3;
               
                if (count % 3 == 0 )
                {

                   {
                        voiceSource.clip = voices[0];
                        voiceSource.pitch = Random.Range(.5f, 1);
                        voiceSource.Play();
                   }
                }
                count++;
            }
        }
    }

   
    IEnumerator WaitAndIntroduce()
    {
        yield return new WaitForSeconds(5);
        GetNextStoryBlock();

        continueButton.SetActive(true);



    }

}
