using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthImageColorChanger : MonoBehaviour
{

    RawImage healthImage;
    Health health;
    Color color;

    // Start is called before the first frame update
    void Awake(){

        healthImage = GetComponent<RawImage>();
        health = GetComponentInParent<Health>();
        color = Color.green;
        healthImage.color = color;

    }

    public void ChangeColor(float currentHealth, float maxHealth) {

        color = ColorChanger.Lerp3(Color.red, Color.blue, Color.green, currentHealth / maxHealth);
        healthImage.color = color;

    }
}
