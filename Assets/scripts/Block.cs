using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

  [SerializeField] AudioClip breakSound;
  [SerializeField] GameObject blockSparklesVFX;
  [SerializeField] int maxHits;
  [SerializeField] Sprite[] hitSprites;

  // cached reference
  Level level;

  // state variables
  [SerializeField] int timesHit; // TODO only serialize for debug purpose.

  private void Start() {
    CountBreakableBlocks();
  }

  private void CountBreakableBlocks() {
    level = FindObjectOfType<Level>();
    if (tag == "Breakable") {
      level.CountBlocks();
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (tag == "Breakable") {
      HandleHit();
    }
  }

  private void HandleHit() {
    timesHit++;
    if (timesHit >= maxHits) {
      DestroyBlock();
    } else {
      ShowNextHitSprite();
    }
  }

  private void ShowNextHitSprite() {
    int spriteIndex = timesHit - 1;
    GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
  }

  private void DestroyBlock() {
    PlayBlockDestroySFX();
    level.BlockDestroyed();
    FindObjectOfType<GameSession>().AddToScore();
    Destroy(gameObject);
    TriggerSparklesVFX();
  }

  private void PlayBlockDestroySFX() {
    AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
  }

  private void TriggerSparklesVFX() {
    GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
    Destroy(sparkles, 2);
  }

}
