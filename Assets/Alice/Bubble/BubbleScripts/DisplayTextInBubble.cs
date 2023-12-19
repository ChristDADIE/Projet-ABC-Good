using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DisplayTextInBubble : MonoBehaviour
{
    public float cooldown = 1000f; // Temps d'affichage de la bulle de texte en secondes
    const float timeDisplay = 1000f;

    [SerializeField] GameObject bubble;
    [SerializeField] TextMeshProUGUI textMeshPro;
    private int maxChar = 52;
    


    void Start()
    {
        // Bulle et texte invisibles au départ
        // à remettre après !!!
        //StopDisplay();

        //Display("Lorsqu on ajoute une solution basique dans une solution acide, le pH de la solution acide augmente. Les industriels utilisent cette technique appelée neutralisation de manière à obtenir des solutions neutres (pH = 7) avant de les rejeter à l’égout. Les ions hydrogène réagissent avec les ions hydroxyde pour donner de l’eau");
    }


    void StartDisplay(string text)
    {
        bubble.SetActive(true);

        textMeshPro.SetText(text);
    }

    public void StopDisplay() 
    {
        bubble.SetActive(false);
    }

    public void Display(string text)
    {
        // On initialise le timer et on affiche la bulle de texte
        cooldown = timeDisplay;
        StartDisplay(text);
    }

    void Update()
    {
        // Tant que le temps d'affichage n'est pas écoulé on continue d'afficher 
        // et on décrémente le timer
        if (cooldown >= 0) 
        {
            cooldown -= Time.deltaTime;
        }
        else 
        {
            StopDisplay();
        }
    }



    public ArrayList DivideLongText(string text) {

        ArrayList textChunks = new ArrayList();

        string[] words = text.Split(' ');

        string chunk = "";
        int length = 0; 

        foreach (string word in words) 
        {
            if (length + word.Length > maxChar) 
            {
                chunk = string.Concat(chunk, " ...");
                textChunks.Add(chunk);
                chunk = "";   
                length = 0;
            }

            else 
            {
                chunk = string.Concat(chunk, string.Concat(word, ' '));  // Remet un espace après chaque mot
                length += word.Length;
            }
        }

        if (!string.IsNullOrEmpty(chunk)) 
        {
            textChunks.Add(chunk.Trim());  // Trim pour enlever l'espace à la fin
        }

        return textChunks;
    }


}