using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestInfo : MonoBehaviour
{
    public string RewardText { set { if (rewardText) { rewardText.text = value; } } }
    public string TargetText { set { if (questTargetText) { questTargetText.text = value; } } }
    public bool Completed { set { if (image) { if (value) image.color = Color.green; } } }
    public bool Active { set { if (disabledImage) { if (value) { disabledImage.gameObject.SetActive(false);questTargetText.color = activeTextColor; } else { disabledImage.gameObject.SetActive(true);  } } } }
    public Sprite BusinessImage{ set { if (businessImage) { businessImage.sprite = value; if (smallImage){ smallImage.sprite = value; } } } }


    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI questTargetText;
    [SerializeField] Image businessImage;
    [SerializeField] Image smallImage;
    [SerializeField]Image image = null;
    [SerializeField] Image disabledImage = null;
    [SerializeField] Color activeTextColor;
    [SerializeField] Color inActiveTextColor;

  

}
