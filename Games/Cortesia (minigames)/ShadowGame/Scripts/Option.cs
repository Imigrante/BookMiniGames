using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {

	public bool chosen;

	public void Clicked() {

		var options = GameObject.FindGameObjectsWithTag("Option");

		foreach (var option in options) {

			option.GetComponent<Option>().chosen = false;
			option.GetComponent<Image>().color = Color.white;
		}

		chosen = true;
		gameObject.GetComponent<Image>().color = Color.gray;
		
		// print("cliquei");
	}

}