using System;

namespace Bring.Runtime
{
    public class Emitter
    {
        private readonly RefEmitter _refEmitter = new();
        private readonly ValueEmitter _valueEmitter = new();

        public void AddRefAction<T>(Action<T> signalAction) where T : class, IRefSignal
        {
            _refEmitter.AddAction(signalAction);
        }

        public void AddValueAction<T>(Action<T> signalAction) where T : struct, IValueSignal
        {
            _valueEmitter.AddAction(signalAction);
        }


        public void RemoveRefAction<T>(Action<T> signalAction) where T : class, IRefSignal
        {
            _refEmitter.RemoveAction(signalAction);
        }

        public void RemoveValueAction<T>(Action<T> signalAction) where T : struct, IValueSignal
        {
            _valueEmitter.RemoveAction(signalAction);
        }


        public void EmitRef<T>(T signal) where T : class, IRefSignal
        {
            _refEmitter.Emit(signal);
        }

        public void EmitValue<T>(T signal) where T : struct, IValueSignal
        {
            _valueEmitter.Emit(signal);
        }
    }
}