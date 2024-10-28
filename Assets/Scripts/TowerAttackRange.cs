using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
   private void Awake() {
    OffAttackRange();
   }
   public void OnAttackRange(Vector3 position, float range) {
    gameObject.SetActive(true);
    float diameter = range * 2.0f;
    transform.localScale = Vector3.one * diameter;
    Vector3 offsetPosition = position;
    offsetPosition.y += 1.0f;
    transform.position = offsetPosition;
   }
   public void OffAttackRange() {
     gameObject.SetActive(false);
   }
}
