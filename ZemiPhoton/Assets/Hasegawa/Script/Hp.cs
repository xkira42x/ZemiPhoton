using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour {

	Slider slider;
	float hp = 0;

	void Start(){
		slider = GetComponent<Slider> ();
	}

	void Update(){
		hp += 0.01f;
		if (hp > 1)	hp = 0;
		slider.value = hp;
	}

}
