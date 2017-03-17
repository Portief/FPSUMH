using UnityEngine;
using System.Collections;

interface BaseClass {
    void Q(bool silenced);
    void E(bool silenced);
    void D(bool silenced);
    void SI(bool silenced);
    bool Cooldown(int hability);
}
