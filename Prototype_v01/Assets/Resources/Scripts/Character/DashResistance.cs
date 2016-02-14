using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DashResistance : MonoBehaviour {

	public int maxDashResistance;
	public int currentDashResistance;
	public int resistancePerDash;
	public Slider DashResistanceSlider;

	// Use this for initialization
	void Start ()
	{
		currentDashResistance = maxDashResistance;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (DashResistanceSlider.value < 100) 
		{
			DashResistanceSlider.value ++;
		}

		if (DashResistanceSlider.value >= resistancePerDash) 
		{
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				DashResistanceSlider.value -= resistancePerDash;
			}
		}
	}
}
