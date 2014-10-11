using UnityEngine;
using System.Collections;

public class TestRotation : MonoBehaviour {

	private void Update()
	{
		this.transform.Rotate(new Vector3(0, 0.3f, 0));
	}

	private void OnGUI()
	{
		//GUI.Label(new Rect(10, 10, 200, 60), this.transform.rotation.ToString());
		//GUI.Label(new Rect(10, 60, 200, 60), this.transform.eulerAngles.ToString());
	}
}
