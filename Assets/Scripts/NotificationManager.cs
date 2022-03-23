using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GIKIT;

public class NotificationManager : MonoBehaviour
{
    static NotificationManager instance = null;
    public static NotificationManager Instance { get { return instance; } }

    [SerializeField] Transform notificationPanel = null;
    [SerializeField] Image image = null;
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] AudioClip notificationSound = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
    }

    public void ShowNotification(Notification notification)
    {
        if (image)
        {
            image.sprite = notification.Sprite;
        }
        if (text)
        {
            text.text = notification.Text;
        }

        StartCoroutine(ShowPanel(notificationPanel, notification.Time));
        MusicManager.Instance.PlaySFX(notificationSound);
    }

    IEnumerator ShowPanel(Transform panel,float autoHideTime)
    {
        if (panel)
        {
            panel.gameObject.SetActive(true);
            yield return new WaitForSeconds(autoHideTime);
            panel.gameObject.SetActive(false);
        }
        
    }
}
