using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMemberVariable : ISearchOption
{
    public object obj   {get;set;}

    public List<GameObject> SearchGameObject(List<GameObject> gameObjects, bool first)
    {
        if (obj != null)
        {
            if (first)
            {
                Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
                foreach (GameObject item in objects)
                {
                    bool contains = false;
                    Component[] components = item.GetComponents<Component>();
                    foreach (Component component in components)
                    {
                        foreach (var member in component.GetType().GetMembers())
                        {
                            if (member.Name == (obj as string) && member.MemberType.ToString() == "Property")
                            {
                                contains = true;
                            }
                        }
                    }
                    if (contains)
                    {
                        gameObjects.Add(item as GameObject);
                    }
                }
            }
            else
            {
                List<GameObject> removeItems = new List<GameObject>();
                foreach (GameObject item in gameObjects)
                {
                    bool contains = false;
                    Component[] components = item.GetComponents<Component>();
                    foreach (Component component in components)
                    {
                        foreach (var member in component.GetType().GetMembers())
                        {
                            if (member.Name == (obj as string) && member.MemberType.ToString() == "Property")
                            {
                                contains = true;
                            }
                        }
                    }
                    if (!contains)
                    {
                        removeItems.Add(item as GameObject);
                    }
                }
                foreach (GameObject item in removeItems)
                {
                    gameObjects.Remove(item);
                }
            }
        }

        return gameObjects;
    }
}
