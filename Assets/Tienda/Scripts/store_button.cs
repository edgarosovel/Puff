using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class store_button : MonoBehaviour {
	public GameObject moneda, candado, textoBoton, sombra, selected, skin;
	GameObject textoMonedas, panel_reallyBuy, panel_choose, panel_no_money;
	//string nombre;
	//int precio, posicion;
	string skin_name;
	public Material[] materials;

	void Start () {
		//textoMonedas = GameObject.FindGameObjectWithTag ("texto_monedas");
		//panel_reallyBuy = GameObject.FindGameObjectWithTag ("panel_buy");
		//panel_choose=GameObject.FindWithTag("panel_choose");
		//panel_no_money=GameObject.FindWithTag("panel_no_money");
	}

	public void pressedButton(){
		GameObject.Find ("item"+ZPlayerPrefs.GetString ("skin")).GetComponent<store_button>().selected.SetActive(false);
		ZPlayerPrefs.SetString ("skin", skin_name);
		selected.SetActive (true);
	}


	public void initButton(string selected_skin, int posicion, float escala){
		//this.posicion = posicion;
		skin_name=materials[posicion].name;
		gameObject.name = "item" + skin_name;
		textoBoton.GetComponent<Text> ().text = skin_name;
		Debug.Log (selected_skin + " : " + skin_name);
		if(selected_skin==skin_name){
			selected.SetActive (true);
		}
		skin.GetComponent<Renderer>().material = materials[posicion];
		skin.transform.localScale *= escala;
		skin.transform.localPosition = new Vector3(skin.transform.localPosition.x, skin.transform.localPosition.y * escala, skin.transform.localPosition.z);
	}
		
	// para en un futuro si vendemos los skins
	void regresaInfo(int posicion){
		switch(posicion){
		case 0:
			//nombre = "RANDOM";
			//precio = 0;
			//sprite = materials [posicion];
		break;
		case 1:
			//nombre = "BLUE";
			//precio = 20;
			//sprite = materials [posicion];
		break;
		case 2:
			//nombre = "GREEN";
			//precio = 20;
			//sprite = materials [posicion];
		break;
		case 3:
			//nombre = "YELLOW";
			//precio = 20;
			//sprite = materials [posicion];
		break;
		case 4:
			//nombre = "GREY";
			//precio = 20;
			//sprite = materials [posicion];
		break;
		default:
			//nombre = "DEFAULT";
			//precio = 0;
		break;
		}
	}
}
