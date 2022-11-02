using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreboardController : MonoBehaviour
{
    QuestionSaver _storer => QuestionSaver.Storer;
    private TransitionController _transitor => TransitionController._controller;
    [SerializeField] GameObject nextElement;
    [SerializeField] GameObject retryElement;

    [Header("answer")]
    [SerializeField] TMP_Text answerText;
    [SerializeField] Image answerImage;
    [SerializeField] Color32 correctColor, wrongColor;
    [SerializeField] Sprite correctSprite, wrongSprite;

    [Header("Questions box")]
    [SerializeField] Transform levelsColumn;
    [SerializeField] public List<GameObject> allQuestionBox;
    [SerializeField] private Color filledColor;

    [Header("Tentativas")]
    [SerializeField] TMP_Text triesText;

    [Header("LevelIndicator")]
    public List<GameObject> levelIndicators;

    [Header("GameOver & GameWon")]
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameWonText;

    private void Start() {
        _transitor.OnStart?.Invoke();

        HandleAnswer();
            
    }

    private void HandleAnswer() {
        if(_storer.currentOfTries == 0) {
            print("Game Over");
            nextElement.SetActive(false);
            _storer.currentLevelHasChanged = false;
            gameOverText.SetActive(true);
        }

        if(_storer.isTheLastQuestion && _storer.hasAnsweredCorrectly) {
            nextElement.SetActive(false);
            gameWonText.SetActive(true);
        }

        for (int i = 0; i < _storer.totalOfQuestionsUsed; i++) {
            //allQuestionBox[i].color = filledColor;
            allQuestionBox[i].GetComponent<Animator>().SetTrigger("Fill");
        }
        if (_storer.hasAnsweredCorrectly)
            UpdateAnswerStyle(correctColor, correctSprite, "acertou");
        else
            UpdateAnswerStyle(wrongColor, wrongSprite, "errou");
        HandleLevelIndicator();
    }

    private void UpdateAnswerStyle(Color32 color, Sprite sprite, string text) {
        answerText.SetText(text);
        answerText.color = color;
        answerImage.sprite = sprite;
        triesText.SetText($"Tentativas {_storer.currentOfTries}");
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

    public void InvokeOutTransition() {
        _transitor.OnOut?.Invoke();
    }
}
