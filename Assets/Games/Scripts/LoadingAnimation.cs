using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour {

	public Text loading;
	float thesh=0.12f;
	float t=0;
	int num=1;

	void Update () {
		t += Time.deltaTime;
		if (t > thesh) {
			t = 0;
			set_loading (num);
			num++;
			if (num == 11)
				num = 1;
		}
	}

	void set_loading(int num){
		switch (num) {
		case 1:loading.text = "L";
			break;
		case 2:loading.text = "Lo";
			break;
		case 3:loading.text = "Loa";
			break;
		case 4:loading.text = "Load";
			break;
		case 5:loading.text = "Loadi";
			break;
		case 6:loading.text = "Loadin";
			break;
		case 7:loading.text = "Loading";
			break;
		case 8:loading.text = "Loading.";
			break;
		case 9:loading.text = "Loading..";
			break;
		case 10:loading.text = "Loading...";
			break;
		}
	}
}
