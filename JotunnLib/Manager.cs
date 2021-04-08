﻿using UnityEngine;

namespace JotunnLib
{
    /// <summary>
    ///     The base class for all the library's various Managers
    /// </summary>
    public abstract class Manager : MonoBehaviour
    {
        /// <summary>
        ///     Initialize manager class after all manager scripts have been added to the root game object
        /// </summary>
        internal virtual void Init() { }
    }
}
