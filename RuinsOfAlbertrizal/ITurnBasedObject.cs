﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Any object that has to reply to EndTurn and/or StartTurn
    /// </summary>
    public interface ITurnBasedObject
    {
        void EndTurn();

        void StartTurn();
    }
}
