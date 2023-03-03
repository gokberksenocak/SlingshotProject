using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private int _gateIndex;
    public int GateIndex 
    {
        get {return _gateIndex; }
        set {_gateIndex=value; }
    }
}
