using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class store_button : MonoBehaviour {
	public GameObject moneda, candado, textoBoton, sombra, selected, skin;
	GameObject textoMonedas, panel_reallyBuy, panel_choose, panel_no_money;
	string nombre;
	int precio, posicion;
	public Material[] materials;

	void Start () {
		//textoMonedas = GameObject.FindGameObjectWithTag ("texto_monedas");
		//panel_reallyBuy = GameObject.FindGameObjectWithTag ("panel_buy");
		//panel_choose=GameObject.FindWithTag("panel_choose");
		//panel_no_money=GameObject.FindWithTag("panel_no_money");
	}

	public void pressedButton(){
		GameObject.Find ("item"+ZPlayerPrefs.GetInt ("skin")).GetComponent<store_button>().selected.SetActive(false);
		ZPlayerPrefs.SetInt ("skin", posicion);
		selected.SetActive (true);
	}


	public void initButton(int estado, int posicion, float escala){
		this.posicion = posicion;
		if(estado==1){
			selected.SetActive (true);
		}
		textoBoton.GetComponent<Text> ().text = materials[posicion].name;
		skin.GetComponent<Renderer>().material = materials[posicion];
		skin.transform.localScale *= escala;
		skin.transform.localPosition = new Vector3(skin.transform.localPosition.x, skin.transform.localPosition.y * escala, skin.transform.localPosition.z);
	}
		
	// para en un futuro si vendemos los skins
	void regresaInfo(int posicion){
		switch(posicion){
		case 0:
			nombre = "RANDOM";
			precio = 0;
			//sprite = materials [posicion];
		break;
		case 1:
			nombre = "BLUE";
			precio = 20;
			//sprite = materials [posicion];
		break;
		case 2:
			nombre = "GREEN";
			precio = 20;
			//sprite = materials [posicion];
		break;
		case 3:
			nombre = "YELLOW";
			precio = 20;
			//sprite = materials [posicion];
		break;
		case 4:
			nombre = "GREY";
			precio = 20;
			//sprite = materials [posicion];
		break;
		default:
			nombre = "DEFAULT";
			precio = 0;
		break;
		}
	}
}
