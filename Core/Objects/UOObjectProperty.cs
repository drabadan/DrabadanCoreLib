using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.Objects
{
    public enum UoPropertyStateEnum : byte
    {
        NotInitialized,
        Initialized
    }

    public abstract class UoObjectProperty
    {
        public static Action<string> Messanger { get; set; }
    }

    public class UoObjectProperty<T> : UoObjectProperty
    {
        //Null avoiding constructor
        public UoObjectProperty(T value, bool updatable)
        {
            Value = value;
            Updatable = updatable;
            CurrentState = UoPropertyStateEnum.NotInitialized;
        }
        //main constructor
        public UoObjectProperty(T value, bool updatable, UoPropertyStateEnum state, [CallerMemberName] string caller = "PropertyName")
        {
            Value = value;
            Updatable = updatable;
            CurrentState = state;

            //for debug messages
            //if (caller != ".ctor")
            //    Messanger?.Invoke($"Intializing property: {caller}");
        }
        

        public T Value { get; set; }
        public bool Updatable { get; private set; }
        public UoPropertyStateEnum CurrentState { get; set; }
        
        public static async Task<UoObjectProperty<T>> SetPropertyValueAsync(Func<Task<UoObjectProperty<T>>> setter)
        {
            return await setter();
        }
    }
    
}
