using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy inherits this for damage taken when hit
public interface Damage
{
    void ApplyDamage(float amount);
}
