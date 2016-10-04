using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanSDK.Core.Objects
{
    public enum UOPropertyStateEnum : byte
    {
        Initialized,
        NotInitialized
    }

    public abstract class UOObjectProperty
    {
        public static Action<string> Messanger { get; set; }        
    }

    public class UOObjectProperty<T> : UOObjectProperty
    {
        //Null avoiding constructor
        public UOObjectProperty(T value, bool updatable)
        {
            Value = value;
            Updatable = updatable;
            CurrentState = UOPropertyStateEnum.NotInitialized;
        }
        //main constructor
        public UOObjectProperty(T value, bool updatable, UOPropertyStateEnum state, [CallerMemberName] string caller = "PropertyName")
        {
            Value = value;
            Updatable = updatable;
            CurrentState = state;

            //for debug messages
            //if(caller != ".ctor")
            //    Messanger?.Invoke($"Intializing property: {caller}");
        }

        public UOObjectProperty(Func<T> getter, bool updatable, UOPropertyStateEnum state, [CallerMemberName] string caller = "PropertyName")
        {
            Value = default(T);
            Updatable = updatable;
            CurrentState = state;

            _getter = getter;
            //for debug messages
            //if(caller != ".ctor")
            //    Messanger?.Invoke($"Intializing property: {caller}");
        }

        private Func<T> _getter { get; set; };

        public T Value { get; set; }
        public bool Updatable { get; private set; }
        public UOPropertyStateEnum CurrentState { get; set; }
        
        public async Task<T> GetValueAsync()
        {
            if (_getter != null)
                return await Task.Run(() => _getter());
            else
                throw new ArgumentNullException("getter");
        }

        public static async Task<UOObjectProperty<T>> SetPropertyValueAsync(Func<Task<UOObjectProperty<T>>> setter)
        {
            return await setter();
        }
    }    
}
