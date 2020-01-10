using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonBehaviour : MonoBehaviour {

    public Canvas mainCanvas;
    public Canvas optionsCanvas;

	public void OnButtonPressed()
    {
        print("Options closed");
        mainCanvas.enabled = true;
        optionsCanvas.enabled = false;
    }
}
