using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface for injecting a pool reference into a helper script that will then assign it to a dependent object.
//e.g. providing an arrow pool reference to a character that needs pooled arrows to shoot.
public interface IPoolInitializable
{
    public void Initialize(GameObjectPool pool);
}
