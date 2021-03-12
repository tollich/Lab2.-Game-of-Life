using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartPanel : GenericPanel {

	public Button startButton;
	public Dropdown sizeDropDown;
	public ProgressBar progressBar;

	void Start () {
		FillSizeDropDown (gui.spawner.sizes);
		progressBar.Hide ();
	}

	public void FillSizeDropDown (int[] values) {
		foreach (int v in values) {
			sizeDropDown.options.Add (new Dropdown.OptionData (string.Format("{0}x{0}", v)));
		}
		sizeDropDown.value = 0;
		sizeDropDown.value = 5;
	}

	public void UpdateSpawningProgress (int p) {
		progressBar.Show ();
		progressBar.SetProgress (p);
	}

	public void SpawningFinish () {
		Invoke ("StartGame", 1f);
	}

	public void StartGame () {
		gui.ShowPanel (1);
		gui.cam.MoveToCenter(gui.spawner.size);
	}

	public void StartButtonClick () {
		sizeDropDown.interactable = false;
		startButton.interactable = false;
		gui.spawner.StartSpawning (sizeDropDown.value);
	}
}
