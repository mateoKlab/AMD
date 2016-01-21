using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Database<T> where T : IDatabaseItem {

	public DatabaseNode<T> rootNode = new DatabaseNode<T> ();
	
	public void AddItem (Type itemType, T item) {

		// Search for the node to put this item into.
		DatabaseNode<T> itemNode = GetNode (itemType);
		
		itemNode.items.Add (item.itemId, item);
	}

	public List<T> GetItems (Type itemType)
	{
		DatabaseNode<T> itemNode = GetNode (itemType);

		return itemNode.items.Values.ToList ();
	}


	#region Private Methods
	private DatabaseNode<T> GetNode (Type itemType) 
	{
		Stack<Type> typeStack = GetTypeStack (itemType);
		
		// Search starts from the base class. Reverse the stack.
		typeStack.Reverse ();
		
		DatabaseNode<T> currentNode = rootNode;
		
		while (typeStack.Count > 0) {
			Type branchType = typeStack.Pop ();
			
			if (currentNode.branches.ContainsKey (branchType)) {
				currentNode = currentNode.branches [branchType];
			} else {
				currentNode.branches.Add (branchType, new DatabaseNode<T> ());
				currentNode = currentNode.branches [branchType];
			}
		}
		
		return currentNode;
	}
	
	private Stack<Type> GetTypeStack (Type itemType)
	{
		Type currentType = itemType;
		
		Stack<Type> typeStack = new Stack<Type> ();
		
		while (currentType.IsNested) {
			
			typeStack.Push (currentType);
			currentType = currentType.BaseType;
		}
		
		return typeStack;
	}
	#endregion
}

public class DatabaseNode<T>
{
	public SerializableDictionary<string, T> items = new SerializableDictionary<string, T> ();
	public SerializableDictionary<Type, DatabaseNode<T>> branches = new SerializableDictionary<Type, DatabaseNode<T>> ();
	

}

public interface IDatabaseItem
{	
	string itemId { get; set; }
}



