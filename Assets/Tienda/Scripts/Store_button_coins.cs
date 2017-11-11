using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Store_button_coins : MonoBehaviour {
	
	public GameObject textoBoton, imagen;
	//GameObject purchaser;
	string nombre;
	int posicion;
	Sprite sprite;
	public Sprite[] sprites;

	public void pressedButton(){
		//purchaser = GameObject.Find("Purchaser");
		if (posicion == 0) {
			//purchaser.GetComponent<Purchaser> ().buyCoins1();
		} else if (posicion == 1) {
			//purchaser.GetComponent<Purchaser> ().buyCoins2();
		} else {
			//purchaser.GetComponent<Purchaser> ().buyCoins3();
		}
	}


	public void initButton(string estado_string, int posicion){
		this.posicion = posicion;
		regresaInfo(posicion);
		imagen.GetComponent<Image> ().sprite = sprite;
		textoBoton.GetComponent<Text> ().text = nombre;
	}


	void regresaInfo(int posicion){
		switch(posicion){
		case 0:
			nombre = "+300 coins";
			sprite = sprites [posicion];
			break;
		case 1:
			nombre = "+800 coins";
			sprite = sprites [posicion];
			break;
		case 2:
			nombre = "+2000 coins";
			sprite = sprites [posicion];
			break;

		default:
			nombre = "DEFAULT";
			break;
		}
	}
}
