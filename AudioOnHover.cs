using UnityEngine;
using TMPro;

public class AudioOnHover : MonoBehaviour
{
    public TextMeshProUGUI level1Text, level2Text, level3Text, level4Text, level5Text;

    public void IntegrantesHover() => FindObjectOfType<AudioManager>().Play("hoverButton");
    public void ExitButtonHover() => FindObjectOfType<AudioManager>().Play("exitButtonHover");
    public void JugarHover() => FindObjectOfType<AudioManager>().Play("jugarHover");
    public void JugarClick() => FindObjectOfType<AudioManager>().Play("jugarClick");

    public void LevelSelectHover(int levelID)
    {
        FindObjectOfType<AudioManager>().Play("hoverButton");
        level1Text.color = new Color32(255, 255, 255, 255);
        level2Text.color = new Color32(255, 255, 255, 255);
        level3Text.color = new Color32(255, 255, 255, 255);
        level4Text.color = new Color32(255, 255, 255, 255);
        level5Text.color = new Color32(255, 255, 255, 255);
        switch (levelID)
        {
            case 1: level1Text.color = new Color32(233, 134, 39, 255); break;
            case 2: level2Text.color = new Color32(233, 134, 39, 255); break;
            case 3: level3Text.color = new Color32(233, 134, 39, 255); break;
            case 4: level4Text.color = new Color32(233, 134, 39, 255); break;
            case 5: level5Text.color = new Color32(233, 134, 39, 255); break;
        }
    }
}
