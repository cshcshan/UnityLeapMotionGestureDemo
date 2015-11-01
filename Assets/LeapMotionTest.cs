using UnityEngine;
using System.Collections;
using Leap;

public class LeapMotionTest : MonoBehaviour {

	Controller leapController;

	// Use this for initialization
	void Start () {
		if (leapController == null) {
			Debug.Log("START");

			leapController = new Controller();
			//leapController.EnableGesture (Gesture.GestureType.TYPECIRCLE); // 畫圓的手勢
			//leapController.EnableGesture (Gesture.GestureType.TYPE_INVALID); 

			leapController.EnableGesture (Gesture.GestureType.TYPE_KEY_TAP); // 手指往下點擊的動作
			//leapController.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", 30.0f); // 最小移動速度，預設為50mm/s
			//leapController.Config.SetFloat("Gesture.KeyTap.HistorySeconds", 0.2f); // 預設為0.1s
			//leapController.Config.SetFloat("Gesture.KeyTap.MinDistance", 10.0f); // 預設為3.0mm
			//leapController.Config.Save();

			leapController.EnableGesture (Gesture.GestureType.TYPESCREENTAP); // 手指往螢幕點擊的動作
			//leapController.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 30.0f); // 預設為50mm/s
			//leapController.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 0.2f); // 預設為0.1s
			//leapController.Config.SetFloat("Gesture.ScreenTap.MinDistance", 10.0f); // 預設為5.0mm
			//leapController.Config.Save();

			leapController.EnableGesture (Gesture.GestureType.TYPE_SWIPE); // 線性揮動的手勢
			//leapController.Config.SetFloat("Gesture.Swipe.MinLength", 3000.0f); // 預設為150mm
			//leapController.Config.SetFloat("Gesture.Swipe.MinVelocity", 1000f); // 預設為1000mm/s
			//leapController.Config.Save();
		} else {
			Debug.Log("無法偵測到Leap Motion裝置");
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if(leapController != null) {
			DetectSwipe();
		}
	}

	void DetectSwipe() {
		Frame frame = leapController.Frame ();
		GestureList gestures = frame.Gestures ();
		Gesture gesture;
		KeyTapGesture keyTapGesture;
		ScreenTapGesture screenTapGesture;
		SwipeGesture swipeGesture;

		for (int i = 0; i < gestures.Count; i++) {
			gesture = gestures[i];
			//Debug.Log("Gesture number: " + i + " " + gesture.Type);

			switch (gesture.Type) {
			case Gesture.GestureType.TYPECIRCLE:
				break;
			case Gesture.GestureType.TYPEINVALID:
				break;
			case Gesture.GestureType.TYPEKEYTAP:
				keyTapGesture = new KeyTapGesture();
				Debug.Log("Key Tap: " + keyTapGesture.State);
				break;
			case Gesture.GestureType.TYPESCREENTAP:
				screenTapGesture = new ScreenTapGesture();
				Debug.Log("Screen Tap: " + screenTapGesture.State);
				break;
			case Gesture.GestureType.TYPESWIPE:
				swipeGesture = new SwipeGesture(gesture);
				if (swipeGesture.State == Gesture.GestureState.STATE_START) {
					Debug.Log("Swipe StartPosition: " + swipeGesture.StartPosition);
					Debug.Log("Swipe Position: " + swipeGesture.Position);

					if (swipeGesture.Position.x - swipeGesture.StartPosition.x < -50) {
						Debug.Log("Swipe: LEFT");
					} else if(swipeGesture.Position.x - swipeGesture.StartPosition.x > 50) {
						Debug.Log("Swipe: RIGHT");
					}
					if (swipeGesture.Position.y - swipeGesture.StartPosition.y < -50) {
						Debug.Log("Swipe: UP");
					} else if(swipeGesture.Position.y - swipeGesture.StartPosition.y > 50) {
						Debug.Log("Swipe: DOWN");
					}
					if (swipeGesture.Position.z - swipeGesture.StartPosition.z < -50) {
						Debug.Log("Swipe: FORWARD");
					}
				} else if (swipeGesture.State == Gesture.GestureState.STATE_STOP) {
					Debug.Log("Swipe State: " + swipeGesture.State);
				} else if (swipeGesture.State == Gesture.GestureState.STATE_UPDATE) {
					//Debug.Log("Swipe State: " + swipeGesture.State);
				}
				break;
			}

			break;
		}
	}
}
