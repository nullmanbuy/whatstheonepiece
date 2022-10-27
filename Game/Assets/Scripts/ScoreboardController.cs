using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreboardController : MonoBehaviour
{
    QuestionSaver _storer => QuestionSaver.Storer;
    [SerializeField] GameObject nextElement;
    [SerializeField] GameObject retryElement;

    [Header("answer")]
    [SerializeField] TMP_Text answerText;
    [SerializeField] Image answerImage;
    [SerializeField] Color32 correctColor, wrongColor;
    [SerializeField] Sprite correctSprite, wrongSprite;

    [Header("Questions box")]
    [SerializeField] Transform levelsColumn;
    [SerializeField] public List<Image> allQuestionBox;
    [SerializeField] private Color filledColor;

    [Header("LevelIndicator")]
    public List<GameObject> levelIndicators;

    private void Start() {
        for (int i = 0; i < _storer.totalOfQuestionsUsed; i++) {
            allQuestionBox[i].color = filledColor;
        }
        HandleLevelIndicator();


        if (_storer.hasAnsweredCorrectly)
            UpdateAnswerStyle(correctColor, correctSprite, "acertou");
        else
            UpdateAnswerStyle(wrongColor, wrongSprite, "errou");

        if(_storer.hasAnsweredCorrectly && !_storer.isTheLastQuestion) return;
        nextElement.SetActive(false);
        _storer.currentLevelHasChanged = false;
    }

    private void UpdateAnswerStyle(Color32 color, Sprite sprite, string text) {
        answerText.SetText(text);
        answerText.color = color;
        answerImage.sprite = sprite;
    }

    public void HandleQuizReset() {
        _storer.ResetQuiz();
        _storer.StartRemainingQuestions();
    }

    public void HandleLevelIndicator() {
        foreach (GameObject indicator in levelIndicators) {
            indicator.SetActive(false);
        }
        levelIndicators[_storer.currentLevelIndex].SetActive(true);
    }
}
