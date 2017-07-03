using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class N3_OnGUI : Photon.MonoBehaviour {


	void Start(){
		if (FindObjectOfType<EventSystemChecker> () == null) {
			var es = new GameObject ("EventSystem", typeof(EventSystem));
			es.AddComponent<StandaloneInputModule> ();
		}
		var textObject = new GameObject ("Text");
		textObject.transform.parent= GameObject.Find("Canvas").transform;
		var text = textObject.AddComponent<Text> ();
		text.rectTransform.anchoredPosition
		text.rectTransform.sizeDelta = Vector2.zero;
		text.rectTransform.anchorMin = Vector2.zero;
		text.rectTransform.anchorMax = Vector2.one;
		text.rectTransform.anchoredPosition = new Vector2 (0.5f, 0.5f);
		text.text="textTest";
		text.font = Resources.FindObjectsOfTypeAll<Font> () [0];
		text.fontSize = 20;
		text.color = Color.yellow;
		text.alignment = TextAnchor.MiddleCenter;
	}
}