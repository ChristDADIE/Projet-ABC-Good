using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    [SerializeField]
    Vector2Int quality = new Vector2Int(1920, 1080);

    [SerializeField]
    Sprite sprite_base_base, sprite_in_base, sprite_base_out, sprite_in_out;

    [SerializeField]
    BaseResearch template_research_ui;

    [SerializeField]
    int margin_w, margin_h;

    [SerializeField]
    int offset_h;

    [SerializeField]
    UILineRenderer template_line;

    [SerializeField]
    Sprite line_sprite_unlocked, line_sprite_semi_locked, line_sprite_locked;

    private void Awake()
    {
        DataResearch.Init();
    }



    void Start()
    {
        int nb_level = DataResearch.NbLevels();

        int delta_w = (quality.x - (margin_w * 2)) / (nb_level + 1);
        int pos_w = margin_w + delta_w - quality.x / 2;


        for (int level = 0; level != DataResearch.NbLevels(); ++level)
        {
            int nb_research = DataResearch.NbResearch(level);
            int delta_h = (quality.y - (margin_h * 2)) / (nb_research + 1);
            int pos_h = margin_h + delta_h + offset_h - quality.y / 2;
            for (int index = 0; index != DataResearch.GetLevel(level).Count; ++index)
            {
                Research current = DataResearch.GetLevel(level)[index];
                BaseResearch newresearch = Instantiate(template_research_ui, transform);
                newresearch.setPosition(new Vector2(pos_w, pos_h));
                newresearch.SetResearch(current);
                current.ui_research = newresearch;
                if (current.prerequisites.Length == 0)
                {
                    if (DataResearch.IsRequired(current))
                        newresearch.SetSprite(sprite_base_out);
                    else
                        newresearch.SetSprite(sprite_base_base);
                }
                else
                {
                    if (DataResearch.IsRequired(current))
                        newresearch.SetSprite(sprite_in_out);
                    else
                        newresearch.SetSprite(sprite_in_base);
                }
                pos_h += delta_h;
            }

            pos_w += delta_w;
        }

        for (int level = 0; level != DataResearch.NbLevels(); ++level)
        {
            for (int index = 0; index != DataResearch.GetLevel(level).Count; ++index)
            {
                Research current = DataResearch.GetLevel(level)[index];

                foreach (Research target in current.prerequisites)
                {
                    UILineRenderer newline = Instantiate(template_line, current.ui_research.transform);
                    Vector3 point1 = target.ui_research.transform.localPosition - current.ui_research.transform.localPosition + new Vector3(130, 5, 0);
                    Vector3 point2 = new Vector3(-120, 5, 0);
                    Vector3 center = (point1 + point2) / 2;
                    const float delta = 15 - 2;

                    Vector3 deltapoint1 = point1 - center;
                    Vector3 deltapoint2 = point2 - center;

                    point1 = deltapoint1.normalized * (deltapoint1.magnitude - delta) + center;
                    point2 = deltapoint2.normalized * (deltapoint2.magnitude - delta) + center;

                    newline.Points = new Vector2[] { point1, point2 };
                    if (current.CanBeUnlocked())
                        newline.texture = line_sprite_unlocked.texture;
                    else
                    {
                        if (target.CanBeUnlocked())
                            newline.texture = line_sprite_semi_locked.texture;
                        else
                            newline.texture = line_sprite_locked.texture;
                    }
                }
            }

        }


    }


    void Update()
    {

    }
}
