using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAction
{
    //sát thương nhân vật và giá trị sát thương
    public static UnityAction<GameObject, int> characterDamaged;
    // giá trị chữa lành và số lượng chữa lành 
    public static UnityAction<GameObject, int> characterHeal;
}