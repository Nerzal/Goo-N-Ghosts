using UnityEngine;

namespace Riddles.Elemental {
  [CreateAssetMenu]
  public class Question : ScriptableObject {
    public string QuestionText;
    public string Answer;
  }
}