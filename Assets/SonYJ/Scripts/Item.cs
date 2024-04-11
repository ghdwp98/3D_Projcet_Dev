using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : MonoBehaviour
{
    public enum Type { Key, Coin, HP };
    public Type type;
    public string itemName;

    public Item(string name)
    {
        itemName = name;
    }
}
