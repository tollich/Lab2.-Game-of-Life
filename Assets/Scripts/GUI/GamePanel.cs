using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePanel : GenericPanel {

	public Image startButtonImage;
	public Text startButtonText;
	public Text generationText;
	public Dropdown intervalDropDown;

	void Start () {
		FillIntervalDropDown (gui.engine.intervals);
		UpdateStartButton ();
	}

	public void FillIntervalDropDown (float[] values) {
		foreach (float v in values) {
			intervalDropDown.options.Add (new Dropdown.OptionData (string.Format("{0:0.00} sec", v)));
		}
		intervalDropDown.value = 1;
	}

	public void IntervalDropDownValueChanged (int i) {
		gui.engine.SetInterval (intervalDropDown.value);
	}

	public void UpdateFromGame (int generation) {
		generationText.text = "Gen: " + generation;
	}

	public void StartButtonClick () {
		gui.engine.ToggleState ();
		UpdateStartButton ();
	}

	public void ResetButtonClick () {
		gui.engine.Reset ();
		UpdateFromGame (0);
		UpdateStartButton ();
	}

	private void UpdateStartButton () {
		if (gui.engine.state == CellEngine.States.Idle) {
			startButtonText.text = "Start";
		} else {
			startButtonText.text = "Stop";
		}
	}

}
