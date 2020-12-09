using System;
using UnityEngine;

namespace Manager
{
    public class GameWonAction : MonoBehaviour
    {
        /// <summary>
        /// Add what ever method that should run when the player wins. 
        /// Todo: Ex. OnEnable() => GameWomAction.gameWon += "methodName";
        /// It's good practice to add OnDisable() => GameWonAction.gameWon -= "methodName";
        /// 
        /// Todo: Use either to call this action.
        /// GameWon?.Invoke();
        /// 
        ///        or
        /// 
        /// if(GameWon != Null)
        ///     GameWon();
        ///
        /// Btw it's good practice to check if it's Null.
        /// </summary>
        public static Action GameWon;
    }
}
