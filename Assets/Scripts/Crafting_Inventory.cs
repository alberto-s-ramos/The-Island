using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting_Inventory : MonoBehaviour
{

	#region Singleton
	public static Crafting_Inventory instance;

	void Awake(){
		instance = this;
	}
	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallBack;

	public int space = 3;

	public List<Item> items = new List<Item>();

	public bool SearchName(string nome){
		foreach (Item i in items) {
			if (i.name.Equals (nome))
				return true;
		}
		return false;
	}

	public bool Add(Item item){

		if (items.Count >= space) {
			return false;
		}
		else{
			items.Add (item);
			if (onItemChangedCallBack != null) {
				onItemChangedCallBack.Invoke ();
			}
			return true;
		}
	}

	public void Remove(Item item){
		items.Remove(item);

		if (onItemChangedCallBack != null) {
			onItemChangedCallBack.Invoke ();
		}
	}
}
