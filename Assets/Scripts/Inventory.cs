using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton
	public static Inventory instance;

	void Awake(){
		instance = this;
	}
	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallBack;

	public int space = 10;

	public List<Item> items = new List<Item>();

	public bool Add(Item item){

		if (items.Count >= space) {
			Debug.Log ("Not enough space in the inventory");	
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