using System;
using System.Collections;
using System.Linq;
using Events;
using Riddles.Elemental;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Riddles.Elements.Scripts {
  public class ElementalRiddleManager : MonoBehaviour {
    [SerializeField] private ElementalRiddleQuestions questions;
    [SerializeField] private GameEvent _riddleSolvedEvent;
    [SerializeField] private GameEvent _failEvent;
    [SerializeField] private GameEvent _buttonClickedEvent;

    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private Image _earthImage;
    [SerializeField] private Image _fireImage;
    [SerializeField] private Image _waterImage;
    [SerializeField] private Image _windImage;

    const string PenaltyMessage = "Your answer was wrong! With ever wrong answer you have to wait longer for a new question";
    private Question _currentQuestion;
    private int _lastQuestionIndex = -1;
    private int _wrongAnswerCount = 0;

    private readonly string[] _elements = { "Earth", "Water", "Fire", "Wind", };
    private bool _waitingForPenalty = false;

    private void Start() {
      SetNewQuestion();
    }

    private void SetNewQuestion() {
      int index = Random.Range(0, this.questions.Count());
      while (index == this._lastQuestionIndex) {
        index = Random.Range(0, this.questions.Count());
      }

      this._lastQuestionIndex = index;
      this._currentQuestion = this.questions[index];
      this._questionText.text = this._currentQuestion.QuestionText;
    }

    private bool AnswerQuestion(string element) {
      this._buttonClickedEvent.Raise();

      if (String.Compare(this._currentQuestion.Answer, element, StringComparison.Ordinal) == 0) {
        this._riddleSolvedEvent.Raise();
      } else {
        this._failEvent.Raise();
        float penalty = 2f + this._wrongAnswerCount++;
        StartCoroutine(nameof(WaitPenaltySetQuestion), penalty);
        return false;
      }
      return true;
    }

    private IEnumerator WaitPenaltySetQuestion(float penalty) {
      this._waitingForPenalty = true;
      this._questionText.text =
        PenaltyMessage;
      yield return new WaitForSeconds(penalty);
      this._waitingForPenalty = false;
      SetNewQuestion();
    }

    private IEnumerator ColorReset(Image image) {
      image.color = Color.red;
      yield return new WaitForSeconds(.5f);
      image.color = Color.white;
    }

    public void AnswerEarth() {
      if (this._waitingForPenalty) {
        return;
      }

      bool success = AnswerQuestion(this._elements[0]);
      if (!success) {
        StartCoroutine(nameof(ColorReset), this._earthImage);
      }
    }

    public void AnswerWater() {
      if (this._waitingForPenalty) {
        return;
      }

      bool success = AnswerQuestion(this._elements[1]);
      if (!success) {
        StartCoroutine(nameof(ColorReset), this._waterImage);
      }
    }

    public void AnswerFire() {
      if (this._waitingForPenalty) {
        return;
      }

      bool success = AnswerQuestion(this._elements[2]);
      if (!success) {
        StartCoroutine(nameof(ColorReset), this._fireImage);
      }
    }

    public void AnswerWind() {
      if (this._waitingForPenalty) {
        return;
      }

      bool success = AnswerQuestion(this._elements[3]);
      if (!success) {
        StartCoroutine(nameof(ColorReset), this._windImage);
      }
    }
  }
}