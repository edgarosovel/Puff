using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorChanger : MonoBehaviour {
	public Color [] color = new Color [7];
	public float duration = 10.0F;
	public int i;
	public int x;
	int last;
	public float t;
	public Image image;
	
	void Start() {
		ColorUtility.TryParseHtmlString ("#E74C3C", out color[0]);
		ColorUtility.TryParseHtmlString ("#F1A9A0", out color[1]);
		ColorUtility.TryParseHtmlString ("#9B59B6", out color[2]);
		ColorUtility.TryParseHtmlString ("#3498DB", out color[3]);
		ColorUtility.TryParseHtmlString ("#03C9A9", out color[4]);
		ColorUtility.TryParseHtmlString ("#F5D76E", out color[5]);
		ColorUtility.TryParseHtmlString ("#E67E22", out color[6]);
		i=Random.Range(0,7);
		do{
			x=Random.Range(0,7);
		}while(x==i);
	}
	
	void Update() {
		t = Mathf.PingPong(Time.time, duration) / duration;
		if (t>0.999f) {
			last=i;
			do{
			i=Random.Range(0,7);
			}while(i==last || i==x);
		}
		if (t<0.001f) {
			last=x;
			do{
			x=Random.Range(0,7);
			}while(x==i || x==last);
		}
		image.color = Color.Lerp(color[i], color[x],t);
	}
}
