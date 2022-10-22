﻿using System;

namespace tetryds.Tools.Internal
{
    internal class Transition<T>
    {
        public T Target { get; }
        private Action trigger;

        public Transition(T target, Action trigger)
        {
            Target = target;
            this.trigger = trigger;
        }

        public void Trigger() => trigger?.Invoke();
    }
}