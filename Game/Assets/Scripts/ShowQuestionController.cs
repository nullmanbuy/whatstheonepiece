using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;
public class ShowQuestionController : MonoBehaviour
{
    //storer
    private QuestionSaver _storer => QuestionSaver.Storer;

    private Level _currentLevel => _storer.currentLevel;
    private int levelsLength => _storer.levels.Length;
    private int remainingQuestionsLength => _storer.currentLevel.remainingQuestions.Count;
    
    public TMP_Text questionText;
    public TMP_Text currentLevelText;
    public float timeToShowQuestion;

    private void Start() {
        _storer.currentQuestionIndex++;
        _storer.totalOfQuestionsUsed++;
        _storer.currentLevel = _storer.levels[_storer.currentLevelIndex];

        currentLevelText.SetText("Nível - " + _currentLevel.dificultName.ToString());
        QuestionSaver.Storer.currentQuestion = SetRandomQuestion();

        questionText.SetText(_storer.currentQuestion.questionAsk + "?");
        SceneCaller.instance.CallScene("QuestionScene", timeToShowQuestion);
    }
    public Question SetRandomQuestion() {
        int randomIndex = Random.Range(0, remainingQuestionsLength);
        QuestionSaver.Storer.currentQuestion = _currentLevel.remainingQuestions[randomIndex];

        Question currentQuestion = QuestionSaver.Storer.currentQuestion;
        _currentLevel.remainingQuestions.RemoveAt(randomIndex);
        print("removing used question");

        if (remainingQuestionsLength == 0 && _currentLevel == _storer.levels[levelsLength - 1]) _storer.isTheLastQuestion = true;
        return currentQuestion;
    }
}
