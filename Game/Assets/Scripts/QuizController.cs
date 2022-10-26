using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class QuizController : MonoBehaviour
{
    private QuestionSaver _storer => QuestionSaver.Storer;
    private Question currentQuestion;
    private int currentQuestionIndex;

    [Header("Timer Text")]
    private float currentTime;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] float timeToAnswer => _storer.currentLevel.timeToQuestion;

    public UnityEvent OnTimesOver;

    [SerializeField] TMP_Text questionText;
    [SerializeField] GameObject[] questionButtons = new GameObject[4];

    //externals 
    [SerializeField] SceneCaller _sceneCaller;
    private void Awake() {
        currentQuestion = QuestionSaver.Storer.currentQuestion;
    }

    private void Start() {
        questionText.SetText(currentQuestion.questionAsk + " ?");
        currentTime = timeToAnswer;

        for (int i = 0; i < questionButtons.Length; i++) {
            ButtonBehaviour opcao = questionButtons[i].GetComponent<ButtonBehaviour>();
            opcao.data = currentQuestion.options[i];
            opcao.UpdateOptionText(currentQuestion.options[i].optionText);
        }

        OnTimesOver.AddListener(() => print("Times over"));
    }

    private void Update() {
        UpdateTimeText();
    }

    public void GoToNextQuestion() {
        _storer.hasAnsweredCorrectly = true;
        if (_storer.currentLevel.dificultQuestions.Count == 0) {
            if(_storer.currentLevelIndex < _storer.levels.Length) {
               _storer.currentLevelIndex++;
            }
        }

        _sceneCaller.CallScene("ScoreScene", 0);
    }

    public void HandleWhenAnswerWrong() {
        //Destroy(QuestionSaver.Storer.gameObject);
        _storer.hasAnsweredCorrectly = false;
        _sceneCaller.CallScene("ScoreScene", 0f);
    }

    private void UpdateTimeText() {
        if (currentTime > 1) {
            currentTime -= Time.deltaTime;
            timeText.SetText(Mathf.FloorToInt(currentTime).ToString());

            if (currentTime <= 1) {
                OnTimesOver?.Invoke();
            }
        }
    }
}

[System.Serializable]
public class OptionButton {
    public string buttonText;
    public bool isTheCorrectAnwser;

    public OptionButton(string text, bool state) {
        buttonText = text;
        isTheCorrectAnwser = state;
    }
}
