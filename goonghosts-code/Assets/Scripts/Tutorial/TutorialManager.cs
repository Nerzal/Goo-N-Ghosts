using System;
using System.Collections;
using System.Linq;
using Events;
using TMPro;
using UnityEngine;

namespace Tutorial {
  public class TutorialManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _tutorialTexts;
    [SerializeField] private string[] _hints;
    [SerializeField] private Animator _ghostAnimator;
    [SerializeField] private GameEvent _showGhostHint;

    private void OnTriggerEnter2D(Collider2D collision) {
      if (!collision.transform.CompareTag("Tutorial")) {
        return;
      }

      string objectName = collision.gameObject.name;
      int number = Int32.Parse(objectName.Last().ToString());
      this._tutorialTexts.text = this._hints[number - 1];

      CheckForGhost(number, collision);
    }

    private void CheckForGhost(int number, Collider2D collider2D) {
      if (number != 8) {
        this._tutorialTexts.color = Color.white;
        return;
      }

      this._tutorialTexts.color = Color.red;
      StartCoroutine(nameof(GetAngryAndDissapear), collider2D);
    }

    private IEnumerator GetAngryAndDissapear(Collider2D collider2D) {
      if (this._ghostAnimator == null) {
        yield break;
      }

      Destroy(collider2D.gameObject);
      this._ghostAnimator.SetTrigger("IsAngry");
      yield return new WaitForSeconds(5);
      this._ghostAnimator.SetTrigger("Disappear");
      Destroy(this._ghostAnimator.gameObject, 1.8f);
      this._showGhostHint.Raise();
    }
  }
}
