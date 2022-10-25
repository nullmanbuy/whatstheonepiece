using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardController : MonoBehaviour
{
    QuestionSaver _storer => QuestionSaver.Storer;
    [SerializeField] GameObject nextElement;
    [SerializeField] GameObject retryElement;

    private void Start() {
        if(_storer.hasAnsweredCorrectly) {
            if (!_storer.isTheLastQuestion) return;
            retryElement.SetActive(true);
            nextElement.SetActive(false);
        } else {
            retryElement.SetActive(true);
            nextElement.SetActive(false);
        }
    }

    public void HandleQuizReset() {
        _storer.ResetQuiz();
    }
}
