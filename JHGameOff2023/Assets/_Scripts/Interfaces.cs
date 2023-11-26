﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreSystem
{
    void Increment(int amount);
}

public interface IAnimatable
{
    void SetAnim(string animName);
}
