using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Key, Coin, HP };
    public Type type;
    public int value;
}
