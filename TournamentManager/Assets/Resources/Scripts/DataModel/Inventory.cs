using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot]
public class ItemDatabase<T> {

	public ItemNode<T> rootNode;

	public List<T> GetItems (ItemType<T> itemType, bool recursive)
	{
		return rootNode.GetItems (itemType, recursive);
	}

	public void Save ()
	{
		// TODO: Fix LoadDatabase<T> filename.
		XmlHelper.Save<ItemDatabase<T>> (this, typeof(ItemDatabase<T>).ToString ()); //(this, typeof (T).ToString () + "Database");
	}
}

public class ItemNode<T>
{
	public SerializableDictionary<string, Item<T>> items;
	public SerializableDictionary<ItemType<T>, ItemNode<T>> branch; 

	// TODO: Get quantity of item: args: itemId.
	// TODO: Get item with Id.

	public List<T> GetItems (ItemType<T> itemType, bool recursive)
	{
		List<T> itemList = null;
		
		ItemNode<T> node = GetNode (itemType);
		
		if (node != null) {
			List<Item<T>> tempList = GetItems (node, recursive);
			itemList = tempList.Select(item => item.item).ToList();
		}
		
		return itemList;
	}

	private ItemNode<T> GetNode (ItemType<T> itemType) 
	{
		ItemNode<T> currentNode = this;
		Stack<ItemType<T>> typeStack = GetTypeStack (itemType);
		
		while (typeStack.Count > 0) {
			ItemType<T> branchType = typeStack.Pop ();
		
			if (currentNode.branch.ContainsKey (branchType)) {
				currentNode = currentNode.branch [branchType];
			} else {
//				Debug.LogError (T + "Database: " + branchType.typeName + " does not exist in database.");
				return null;
			}
		}

		return currentNode;
	}

	private List<Item<T>> GetItems (ItemNode<T> itemNode, bool recursive)
	{
		List<Item<T>> itemList = new List<Item<T>> ();
		
		if (itemNode.items != null) {
			itemList.AddRange (itemNode.items.Values.ToList ());
		}
		
		// Recursively search through nested databases.
		if (recursive && itemNode.branch != null) {
			
			foreach (ItemNode<T> nestedNode in itemNode.branch.Values) {
				itemList.AddRange (GetItems (nestedNode, recursive));
			}
		}
		
		return itemList;
	}

	private static Stack<ItemType<T>> GetTypeStack (ItemType<T> itemType)
	{
		ItemType<T> currentType = itemType;
		
		Stack<ItemType<T>> typeStack = new Stack<ItemType<T>> ();
		typeStack.Push (currentType);
		
		while (currentType.parentType != null) {
			
			typeStack.Push (currentType.parentType);
			
			currentType = itemType.parentType;
		}
		
		return typeStack;
	}
}


public class Item<T>
{
	public T item;
	public int itemQuantity;
}


// Derive from this class and define your item hierarchy.
[XmlRoot]
[XmlInclude(typeof(Equipment.Type))]
public abstract class ItemType<T>
{
	[XmlElement ("TypeName")]
	public string typeName;
	public Type type { get { return typeof (T); } }

	public virtual ItemType<T> parentType { get { return null; } }

	// Override Equals and GetHashCode methods to determine type equality.
	public override bool Equals(object obj)
	{
		if (obj == null) {
			return false;
		}
		
		return typeName == (obj as ItemType<T>).typeName;
	}
	
	public override int GetHashCode()
	{
		return typeName.GetHashCode();
	}
}


