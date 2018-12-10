using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

  [SerializeField] int score;

  SceneLoader sceneLoader;

  public void start() {
    sceneLoader = FindObjectOfType<SceneLoader>();
  }

  public void UpdateScore(int newScore) {
    score += newScore;
  }

}
