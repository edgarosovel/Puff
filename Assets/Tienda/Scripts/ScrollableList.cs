using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab, prefabCoins;
    public int itemCount = 10, columnCount = 1;
	string skins_comprados;
	int[]skins_array;
	string[]s_skins_array;
	int selected, estado;

	void obtener_comprados(){
		//skins_comprados = ZPlayerPrefs.GetString ("skins_comprados");
		//s_skins_array = skins_comprados.Split (',');
		//skins_array = Array.ConvertAll(s_skins_array, s=>int.Parse(s));
	}

    void Start()
    {
		if (!ZPlayerPrefs.HasKey("skin")) ZPlayerPrefs.SetInt ("skin",0);
		selected = ZPlayerPrefs.GetInt ("skin");
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();

        //calculate the width and height of each child item.
        float width = containerRectTransform.rect.width / columnCount;
        float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height * ratio;
        int rowCount = itemCount / columnCount;
        if (itemCount % rowCount > 0)
            rowCount++;

        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
		containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight);
		containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, containerRectTransform.offsetMax.y);

		//Get list of bought items
		//obtener_comprados();
		int aux = 0;
        int j = 0;
        for (int i = 0; i < itemCount; i++)
        {
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
                j++;

            //create a new item, name it, and set the parent
			//GameObject newItem = (i<3)?Instantiate(prefabCoins) as GameObject:Instantiate(itemPrefab) as GameObject;
			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = "item"+i;
			newItem.transform.SetParent (gameObject.transform, false);

            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

			float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = containerRectTransform.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);

			//if(i>=3){
			//INTIALIZE EACH BUTTON
				//if (skins_array [aux] == i) {
			estado = selected == i ? 1 : 0;
					//if(aux<skins_array.Length-1){
						//aux++;
					//}
				//} else {
					//estado ="0";
				//}
				newItem.GetComponent<store_button>().initButton(estado, i, ratio);
			//}
			//else{
			//	newItem.GetComponent<Store_button_coins>().initButton("",i);
			//}
        }
    }

}
