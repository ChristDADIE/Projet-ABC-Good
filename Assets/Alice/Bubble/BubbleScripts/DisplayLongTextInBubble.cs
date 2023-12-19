using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DisplayTextInBubble))]
public class DisplayLongTextInBubble : MonoBehaviour
{

    // Instance statique du singleton
    private static DisplayLongTextInBubble instance;

    // Propriété d'accès public à l'instance unique
    public static DisplayLongTextInBubble Instance
    {
        get
        {
            // Si l'instance n'a pas encore été créée, créez-la
            if (instance == null)
            {
                instance = FindObjectOfType<DisplayLongTextInBubble>();

                // Si FindObjectOfType ne trouve pas d'instance, créez une nouvelle
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<DisplayLongTextInBubble>();
                }
            }

            return instance;
        }
    }


    ArrayList textChunks;
    int chunkIndex;
    int maxIndex;
    

    void Start() 
    {
        DisplayLongText("Lorsqu on ajoute une solution basique dans une solution acide, le pH de la solution acide augmente. Les industriels utilisent cette technique appelée neutralisation de manière à obtenir des solutions neutres (pH = 7) avant de les rejeter à l’égout. Les ions hydrogène réagissent avec les ions hydroxyde pour donner de l’eau.");
    }

    public void DisplayLongText(string text) 
    {
        chunkIndex = 0;

        textChunks = GetComponent<DisplayTextInBubble>().DivideLongText(text);

        // Pour les tutoriels : 
        textChunks.Add("Fin du tutoriel.");
        //

        UnityEngine.Debug.Log("textChunks : " + textChunks);
        Debug.Log("chunkIndex : " + chunkIndex);

        maxIndex = textChunks.Count - 1;

        Debug.Log("maxIndex : " + maxIndex);

        GetComponent<DisplayTextInBubble>().Display((string)textChunks[chunkIndex]);
    }

    public void NextText() 
    {
        Debug.Log("in NextText");
        Debug.Log("chunkIndex : " + chunkIndex);

        if (chunkIndex == maxIndex) 
        {
            chunkIndex += 1;
            Debug.Log("No more chunks. Stopping display.");
            GetComponent<DisplayTextInBubble>().StopDisplay();
        }

        else if (chunkIndex < maxIndex)
        {
            chunkIndex += 1;
            Debug.Log("Displaying next chunk: " + textChunks[chunkIndex]);
            GetComponent<DisplayTextInBubble>().Display((string)textChunks[chunkIndex]);
        }
    }

    public void PreviousText() 
    {
        Debug.Log("in PreviousText");
        Debug.Log("chunkIndex : " + chunkIndex);

        if (chunkIndex > 0 & chunkIndex <= maxIndex) 
        {
            chunkIndex -= 1;
            Debug.Log("Displaying previous chunk: " + textChunks[chunkIndex]);
            GetComponent<DisplayTextInBubble>().Display((string)textChunks[chunkIndex]);
        }
    }

}