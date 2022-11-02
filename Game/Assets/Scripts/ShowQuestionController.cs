using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;
public class ShowQuestionController : MonoBehaviour
{
    //storer
    private QuestionSaver _storer => QuestionSaver.Storer;
    private TransitionController _transitor => TransitionController._controller;

    private Level _currentLevel => _storer.currentLevel;
    private int levelsLength => _storer.levels.Length;
    private int remainingQuestionsLength => _storer.currentLevel.remainingQuestions.Count;
    
    public TMP_Text questionText;
    public TMP_Text currentLevelText;
    public float timeToShowQuestion;

    private void Start() {
        _transitor.OnStart?.Invoke();
        _storer.currentQuestionIndex++;
        _storer.totalOfQuestionsUsed++;
        _storer.currentLevel = _storer.levels[_storer.currentLevelIndex];

        currentLevelText.SetText("Nível - " + _currentLevel.dificultName.ToString());
        QuestionSaver.Storer.currentQuestion = SetRandomQuestion();

        questionText.SetText(_storer.currentQuestion.questionAsk + "?");
        Coroutines.DoAfter(() => _transitor.OnOut?.Invoke(), timeToShowQuestion, this);
        SceneCaller.instance.CallScene("QuestionScene", timeToShowQuestion + 0.5f);
    }
    public Question SetRandomQuestion() {
        int randomIndex = Random.Range(0, remainingQuestionsLength);
        QuestionSaver.Storer.currentQuestion = _currentLevel.remainingQuestions[randomIndex];

        Question currentQuestion = QuestionSaver.Storer.currentQuestion;
        _currentLevel.remainingQuestions.RemoveAt(randomIndex);
        print("removing used question");

        if (_storer.currentQuestionIndex >= _currentLevel.totalLevelQestions && _currentLevel == _storer.levels[levelsLength - 1]) _storer.isTheLastQuestion = true;
        return currentQuestion;
    }
}
