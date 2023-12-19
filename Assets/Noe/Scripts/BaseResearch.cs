using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseResearch : MonoBehaviour
{
    Research research;

    public void Awake()
    {
        Material mat = Instantiate(transform.GetChild(2).GetComponent<Image>().material);
        transform.GetChild(2).GetComponent<Image>().material = mat;
    }
    public void SetSprite(Sprite sprite)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }

    public void SetResearch(Research research)
    {
        this.research = research;
        Actualise();
    }

    public void setPosition(Vector2 position)
    {
        transform.localPosition = new Vector3(position.x, position.y, 0);
    }

    public bool CanBeUnlocked()
    {
        return research.CanBeUnlocked();
    }

    void Actualise()
    {
        transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = research.name;
        transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text = research.unlocked.ToString() + "/" + research.total_unlocked.ToString();
        if (CanBeUnlocked())
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1);
            transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            transform.GetChild(4).gameObject.SetActive(true);
        }

        transform.GetChild(2).GetComponent<Image>().material.SetFloat("_progress", ((float)research.unlocked) / research.total_unlocked);
    }


    void Update()
    {

    }
}
