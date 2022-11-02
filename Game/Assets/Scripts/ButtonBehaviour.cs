using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Button))]
public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private QuizController Quiz;
    public Option data;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Color colorOnHover, normalColor;
    private Button button;
    public Color correctColor, wrongColor;
    private bool hasClicked;

    private void Awake() {
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void ChangeTextColor(string colorText) {
        Color colorFromHex;
        ColorUtility.TryParseHtmlString(colorText, out colorFromHex);
        if(!hasClicked)
        buttonText.color = colorFromHex;
    }


    public void UpdateOptionText(string text) {
        buttonText.SetText(text);
    }

    public void BlinkButton(Color[] colors, float blinkTime) {
        StartCoroutine(BlinkCoroutine(colors, blinkTime));
    }

    IEnumerator BlinkCoroutine(Color[] colors, float blinkTime) {
        Image sprite = GetComponent<Image>();
        for (int i = 0; i < blinkTime; i++) {
            foreach (Color color in colors) {
                sprite.color = color;
                yield return new WaitForSeconds(0.1f);
            }
        }

        sprite.color = Color.white;
    }

    public void AnswerQuestion() {
        Quiz.HandleAnswer(data.correctAnwser);
        hasClicked = true;
        if (data.correctAnwser) {
            BlinkButton(new Color[] { Color.white, correctColor }, 2f);
        } else {
            BlinkButton(new Color[] {Color.white, wrongColor}, 2f);
        }
    }
}
