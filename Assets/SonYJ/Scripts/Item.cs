using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    /*public enum Type { Key, Coin, HP };
    public Type type;
    public int value;*/

    public string itemName;
    public Sprite itemImage;
    public int itemCount;
}
