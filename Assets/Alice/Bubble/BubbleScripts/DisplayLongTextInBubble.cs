using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DisplayTextInBubble))]
public class DisplayLongTextInBubble : MonoBehaviour
{
    public AudioSource sourceRobot;
    public AudioClip robotSound;


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
        //DisplayLongText("Lorsqu on ajoute une solution basique dans une solution acide, le pH de la solution acide augmente. Les industriels utilisent cette technique appelée neutralisation de manière à obtenir des solutions neutres (pH = 7) avant de les rejeter à l’égout. Les ions hydrogène réagissent avec les ions hydroxyde pour donner de l’eau.");
        sourceRobot = GetComponent<AudioSource>();
        DialogueEntree();
        //DialogueChimie();
    }

    public void DisplayLongText(string text) 
    {
        chunkIndex = 0;

        textChunks = GetComponent<DisplayTextInBubble>().DivideLongText(text);

        // Pour les tutoriels : 
        //textChunks.Add("Fin du tutoriel.");
        //

        UnityEngine.Debug.Log("textChunks : " + textChunks);
        Debug.Log("chunkIndex : " + chunkIndex);

        maxIndex = textChunks.Count - 1;

        Debug.Log("maxIndex : " + maxIndex);

        GetComponent<DisplayTextInBubble>().Display((string)textChunks[chunkIndex]);

        sourceRobot.PlayOneShot(robotSound, 0.5f);
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




    // TEXTES DIALOGUE PHYSIK 

    // Entrée dans le jeu
    //Mise en place du casque, ambiance forêt par défaut
    //Lorsque le robot est centré sur l’écran (l'utilisateur le regarde),
    // apparition d’une bulle de dialogue avec bruitages robotiques (drive/sons/robot) : 
    string textEntree1 = "Salut ! Appuie sur A pour lire la suite. Je suis φ-zic, votre assistant. Laissez-moi vous guider. Voici μ-zic, le tourne disque. Vous pourrez changer d’ambiance en interagissant avec lui (avec la gâchette du majeur, comme pour l’attraper) puis en choisissant une musique ! Le matériel changera en fonction des expériences. D’ailleurs en parlant d’expériences, ça te tente d’en lancer une ? Appuie sur le bouton 'menu' de la manette gauche pour ouvrir le menu.";


    //Expérience chimie - attaque :
    //Attaque
    string textChimie1 = "Oh non ! Des slimes nous attaquent, utilisons les fioles pour repousser les monstres. N’oublie pas de mettre tes gants ! Il semblerait que certaines fioles ne soient pas efficaces contre certains ennemis, essayons de faire correspondre la couleur des fioles avec celle des slimes. Du violet ?! Pourtant nous n’avons que des fioles bleues et rouges, qu’allons nous pouvoir faire ? Il existe sûrement une solution. Serait-il possible de mélanger le contenu des fioles ?";
    // //qques secondes plus tard
    // string textChimie2 = "Il semblerait que certaines fioles ne soient pas efficaces contre certains ennemis, essayons de faire correspondre la couleur des fioles avec celle des slimes.";
    // //qques secondes plus tard
    // string textChimie3 = "Du violet ?! Pourtant nous n’avons que des fioles bleues et rouges, qu’allons nous pouvoir faire ? Il existe sûrement une solution.";
    // //Indice
    // string textChimie4 = "Serait-il possible de mélanger le contenu des fioles ?"


    //Expérience physique :
    //Intro
    string textPhysique1 = "Bienvenue dans l’expérience physique sur les rayons lumineux ! Attention à ne pas regarder la source du laser directement, ça pourrait abîmer tes yeux ! Utilise plutôt tes mains ou place un autre objet sur la trajectoire du laser pour voir s’il fonctionne ou pas. La lumière se propage en ligne droite. Une des approches pour comprendre la lumière est de la représenter sous forme de rayons lumineux. Voici une source lumineuse fixe, ta mission est d’utiliser le matériel à disposition pour éclairer les cibles ! Les miroirs permettent de réfléchir la lumière pour modifier sa trajectoire. Les plaques semi-réfléchissantes laissent passer une moitié du rayon lumineux et en réfléchissent la deuxième. Essaye et observe bien !";
    // //sécurité
    // string textPhysique2 = "Attention à ne pas regarder la source du laser directement, ça pourrait abîmer tes yeux ! Utilise plutôt tes mains ou place un autre objet sur la trajectoire du laser pour voir s’il fonctionne ou pas.";
    // //Tuto (présentation du matériel et de son fonctionnement) :
    // string textPhysique3 = "La lumière se propage en ligne droite. Une des approches pour comprendre la lumière est de la représenter sous forme de rayons lumineux. Voici une source lumineuse fixe, ta mission est d’utiliser le matériel à disposition pour éclairer les cibles ! Les miroirs permettent de réfléchir la lumière pour modifier sa trajectoire. Les plaques semi-réfléchissantes laissent passer une moitié du rayon lumineux et en réfléchissent la deuxième. Essaye et observe bien !";


    public void DialogueEntree() 
    {
        DisplayLongText(textEntree1);
    }

    public void DialogueChimie() 
    {
        DisplayLongText(textChimie1);
    }

    public void DialoguePhysique() 
    {
        DisplayLongText(textPhysique1);
    }

}