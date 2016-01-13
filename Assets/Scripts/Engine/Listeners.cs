using UnityEngine;
using System.Collections;


public delegate void Listener();
public delegate void Listener<T>(T p1);
public delegate void Listener<T, U>(T p1, U p2);
