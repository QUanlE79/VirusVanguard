using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class used for all stats where we want to be able to add/remove modifiers */

[System.Serializable]
public class Stat {

	[SerializeField]
	private int baseValue;	// Starting value

	// List of modifiers that change the baseValue
	private List<int> modifiers = new List<int>();

	// Get the final value after applying modifiers
	public int GetValue ()
	{
		int finalValue = baseValue;
		modifiers.ForEach(x => finalValue += x);
		return finalValue;
	}

	// Add new modifier
	public void AddModifier (int modifier)
	{
		if (modifier != 0)
			modifiers.Add(modifier);
	}

	// Remove a modifier
	public void RemoveModifier (int modifier)
	{
		if (modifier != 0)
			modifiers.Remove(modifier);
	}

    public List<int> GetModifiers()
    {
        return modifiers;
    }
    public void SetModifiers(List<int> newModifiers)
    {
        ClearModifiers();
        AddModifiers(newModifiers);
    }
    public void AddModifiers(List<int> newModifiers)
    {
        newModifiers.ForEach(x => AddModifier(x));
    }

    // Clear all modifiers
    public void ClearModifiers()
    {
        modifiers.Clear();
    }

}
