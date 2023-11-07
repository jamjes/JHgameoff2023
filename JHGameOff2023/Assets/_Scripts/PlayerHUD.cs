using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI ManaValue;

    public void UpdateManaValue(int value)
    {
        ManaValue.text = value.ToString();
    }
}
