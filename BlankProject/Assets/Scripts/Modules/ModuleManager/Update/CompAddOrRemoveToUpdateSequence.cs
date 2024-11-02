using UnityEngine;

namespace Modules.ModuleManager
{
    public static class CompAddOrRemoveToUpdateSequence
    {
        // *****************************
        // AddToUpdate
        // *****************************
        public static void AddToUpdate(State _state, IModuleUpdate _module)
        {
            LibModuleExceptions.ExceptionIfNotInitialized(_state.dynamicData.isInitialised);
            AddToUpdateInternal(_state, _module);
        }

        // *****************************
        // RemoveFromUpdate
        // *****************************
        public static void RemoveFromUpdate(State _state, IModuleUpdate _module)
        {
            LibModuleExceptions.ExceptionIfNotInitialized(_state.dynamicData.isInitialised);
            RemoveFromUpdateInternal(_state, _module);
        }

        // *****************************
        // AddToUpdateExternal
        // *****************************
        public static void AddToUpdateInternal(State _state, IModuleUpdate _module)
        {
            bool error = _module == null;
            if (error)
            {
                Debug.LogWarning($"Module cannot be added since it does not implement IModuleUpdate or its NULL!");
                return;
            }

            _state.dynamicData.updateSequenceRuntime.Add(_module);
        }


        // *****************************
        // RemoveFromUpdateInternal
        // *****************************
        public static void RemoveFromUpdateInternal(State _state, IModuleUpdate _module)
        {
            bool error = _module == null;
            if (error)
            {
                Debug.LogWarning($"Module cannot be removed since it does not implement IModuleUpdate or its NULL!");
                return;
            }

            _state.dynamicData.updateSequenceRuntime.Remove(_module);
        }
    }
}
