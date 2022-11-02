using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utils;
using UnityEngine.Events;

public class QuizController : MonoBehaviour
{
    private QuestionSaver _storer => QuestionSaver.Storer;
    private TransitionController _transitor => TransitionController._controller;
    private Question currentQuestion;
    private int currentQuestionIndex;

    [Header("Timer Text")]
    private float currentTime;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Animator timeAnim;
    [SerializeField] float timeToAnswer => _storer.currentLevel.timeToQuestion;
    private bool timesOver;

    public UnityEvent OnTimesOver;

    [SerializeField] TMP_Text questionText;
    [SerializeField] GameObject[] questionButtons = new GameObject[4];
    public GameObject correctButton;
    public bool hasChoosed;

    [Header("heart")]
    [SerializeField] Transform heartHolder;
    [SerializeField] GameObject heart;

    [Header("Level Text")]
    [SerializeField] TMP_Text levelText;

    //externals 
    [SerializeField] SceneCaller _sceneCaller;
    private void Awake() {
        currentQuestion = QuestionSaver.Storer.currentQuestion;
    }

    private void Start() {
        _transitor.OnStart?.Invoke();
        questionText.SetText(currentQuestion.questionAsk + " ?");
        currentTime = timeToAnswer;

        for (int i = 0; i < _storer.currentOfTries; i++) {
            if (i > 0) Coroutines.DoAfter(() => Instantiate(heart, heartHolder), 0.15f, this);
            else Instantiate(heart, heartHolder);
        }

        for (int i = 0; i < questionButtons.Length; i++) {
            ButtonBehaviour opcao = questionButtons[i].GetComponent<ButtonBehaviour>();
            opcao.data = currentQuestion.options[i];
            opcao.UpdateOptionText(currentQuestion.options[i].optionText);

            if (opcao.data.correctAnwser) correctButton = questionButtons[i];
        }

        levelText.SetText($"Nível -  <color=#ff82bd>{_storer.currentLevel.dificultName.ToString()}");

        OnTimesOver.AddListener(() => { 
            if (!hasChoosed) {
                HandleAnswer(false);
                timeAnim.SetTrigger("timesout");
            }
        });
    }

    private void Update() {
        UpdateTimeText();
    }

    private void HandleWhenAnswerWrong() {
        _storer.currentOfTries--;
        print("Tries removed");

        RemoveLastHeart();

        correctButton.GetComponent<ButtonBehaviour>().BlinkButton(new Color[] { Color.white, correctButton.GetComponent<ButtonBehaviour>().correctColor }, 1f);
    }

    public void HandleAnswer(bool answer) {
        _storer.hasAnsweredCorrectly = answer;
        hasChoosed = true;
        if(!answer) {
            HandleWhenAnswerWrong();
        } 

        if (_storer.currentQuestionIndex >= _storer.currentLevel.totalLevelQestions) {
            if (_storer.currentLevelIndex < _storer.levels.Length - 1) {
                _storer.currentLevelIndex++;
                _storer.StartRemainingQuestions();
            }
        }

        Coroutines.DoAfter(() => {
            _transitor.OnOut?.Invoke();
            _sceneCaller.CallScene("ScoreScene", 0.5f);
        }, 2f, this);
    }

    private void UpdateTimeText() {
        if (currentTime > 1) {
            currentTime -= Time.deltaTime;
            timeText.SetText(Mathf.FloorToInt(currentTime).ToString());

            if (currentTime <= 1 && !timesOver) {
                print("times over");
                timesOver = true;
                OnTimesOver?.Invoke();
                return;
            }
        }
    }

    public void RemoveLastHeart() {
        Transform lastHeart = heartHolder.GetChild(0);
        StartCoroutine(BlinkCoroutine(new Color[] { Color.white, new Color(255f, 255f, 255f, 0.5f) }, 1.5f, lastHeart.GetComponent<Image>()));
        Coroutines.DoAfter(() => {
            lastHeart.gameObject.SetActive(false);
        }, 1.5f, this);
    }
    IEnumerator BlinkCoroutine(Color[] colors, float blinkTime, Image sprite) {
        for (int i = 0; i < blinkTime; i++) {
            foreach (Color color in colors) {
                sprite.color = color;
                yield return new WaitForSeconds(0.1f);
            }
        }

        sprite.color = Color.white;
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
