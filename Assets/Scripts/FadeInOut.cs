using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

    public float fadeTime;

    private Image fadePanel;
    private Color currentColor = Color.black;


	// Use this for initialization
	void Start () {

        fadePanel = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.timeSinceLevelLoad < fadeTime)
        {
            float alphaChange = Time.deltaTime / fadeTime;
            currentColor.a -= alphaChange;
            fadePanel.color = currentColor;
        }
        else // turn this object off so user can interact with UI
            gameObject.SetActive(false);
		
	}
}
