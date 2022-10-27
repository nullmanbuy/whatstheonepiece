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

    private void Awake() {
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void ChangeTextColor(string colorText) {
        Color colorFromHex;
        ColorUtility.TryParseHtmlString(colorText, out colorFromHex);
        buttonText.color = colorFromHex;
    }


    public void UpdateOptionText(string text) {
        buttonText.SetText(text);
    }

    public void AnswerQuestion() {
        if (data.correctAnwser) {
            Quiz.GoToNextQuestion();
        } else {
            Quiz.HandleWhenAnswerWrong();
        }
    }
}
