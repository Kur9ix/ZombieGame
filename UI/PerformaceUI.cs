using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformaceUI : MonoBehaviour
{
    public Text text;
    public float deltaTime;
    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		float fps = 1.0f / deltaTime;
		text.text = "FPS:" + Mathf.Ceil (fps).ToString ();
        
    }
}
