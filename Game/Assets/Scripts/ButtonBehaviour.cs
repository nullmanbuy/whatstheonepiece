using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private QuizController Quiz;
    public Option data;
    private TMP_Text buttonText;

    private void Awake() {
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void UpdateOptionText(string text) {
        buttonText.SetText(text);
    }

    public void AnswerQuestion() {
        print("respondido");
        if (data.correctAnwser) {
            Quiz.GoToNextQuestion();
        } else {
            print("errada X");
            Quiz.HandleWhenAnswerWrong();
        }
    }
}
