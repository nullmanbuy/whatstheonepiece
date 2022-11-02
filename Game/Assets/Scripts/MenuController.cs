using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuController : MonoBehaviour {
    [SerializeField] TMP_Text buttonText;


    public void ChangeTextColor(string colorText) {
        Color colorFromHex;
        ColorUtility.TryParseHtmlString(colorText, out colorFromHex);
        buttonText.color = colorFromHex;
    }
}
