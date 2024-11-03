using System;
using System.Collections.Generic;

namespace Bring.Runtime
{
    public class RefEmitter
    {
        private readonly Dictionary<Type, object> _signalsActions = new();

        public void AddAction<T>(Action<T> signalAction) where T : class, IRefSignal
        {
            var signalKey = typeof(T);
            if (!_signalsActions.TryGetValue(signalKey, out var signalActions))
            {
                _signalsActions[signalKey] = signalAction;
                return;
            }

            var existingActions = (Action<T>)signalActions;
            existingActions += signalAction;
            _signalsActions[signalKey] = existingActions;
        }

        public void RemoveAction<T>(Action<T> signalAction) where T : class, IRefSignal
        {
            var signalKey = typeof(T);
            if (!_signalsActions.TryGetValue(signalKey, out var signalActions))
            {
                return;
            }

            var existingActions = (Action<T>)signalActions;
            existingActions -= signalAction;
            _signalsActions[signalKey] = existingActions;
        }

        public void Emit<T>(T signal) where T : class, IRefSignal
        {
            var signalKey = typeof(T);
            if (_signalsActions.TryGetValue(signalKey, out var signalActions))
            {
                ((Action<T>)signalActions)?.Invoke(signal);
            }
        }
    }
}