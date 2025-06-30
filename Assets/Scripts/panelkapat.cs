using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelkapat : MonoBehaviour
{
    public GameObject soruPaneli;
    public GameObject targetGameObject;
    public static event System.Action OnPanelClosed;

    public void SoruKapat()
    {
        Debug.Log("SoruKapat çaðrýldý");
        soruPaneli.SetActive(false);
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(true);
        }

        // Event'i tetiklemeden önce null check
        if (OnPanelClosed != null)
        {
            Debug.Log("Panel kapatma eventi tetikleniyor");
            OnPanelClosed.Invoke();
        }
    }
}