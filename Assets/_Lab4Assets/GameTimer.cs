using System;
using UnityEngine;
using TMPro;

// apparently : is like extends in Java
// MonoBehaviour already has a buncha default implementations
// just overriding the Update one 
public class GameTimer : MonoBehaviour {

  public GameRunController runController;
  public TextMeshProUGUI timerText;

  void Update() {
    if (!runController.Started) {
      timerText.text = "00:00";
      return;
    }

    TimeSpan elapsed = DateTime.UtcNow - runController.StartTimeUtc;
    timerText.text = string.Format("{0:00}:{1:00}", (int) elapsed.TotalMinutes, (int) elapsed.Seconds);
  }
}
