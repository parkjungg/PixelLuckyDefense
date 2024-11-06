using System.Collections;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 0.5f;
    public float floatRange = 10f;

    private Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine("FloatText");
    }

    IEnumerator FloatText() {
        while(true) {
            float y = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            transform.position = initialPosition + new Vector3(0, y, 0);
            yield return null;
        }
    }
}
