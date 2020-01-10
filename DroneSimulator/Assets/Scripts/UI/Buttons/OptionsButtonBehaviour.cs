using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonBehaviour : MonoBehaviour {

    public Canvas mainCanvas;
    public Canvas optionsCanvas;

	public void OnButtonPressed()
    {
        print("Options opened");
        mainCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }
}
