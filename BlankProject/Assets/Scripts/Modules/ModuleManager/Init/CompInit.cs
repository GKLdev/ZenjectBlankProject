using Modules.ModuleManager;
using Modules.ModuleManager_Public;
using UnityEngine;
using Zenject;

namespace Modules.ModuleManager
{
    public static class CompInit
    {
        // *****************************
        // Init
        // *****************************
        public static void Init(State _state, IModuleManager _self)
        {
            bool alreadyInitialized = _state.dynamicData.isInitialised;
            if (alreadyInitialized)
            {
                return;
            }
            
            _state.installer.self = _self;
            InitModulesSequence(_state);
            InitRuntimeUpdateSequence(_state);
            
            _state.dynamicData.isInitialised = true;
        }

        // *****************************
        // InitModulesSequence
        // *****************************
        static void InitModulesSequence(State _state)
        {
            // init context and bind modules //
            _state.context.Run();

            // initialize sequence //
            for (int i = 0; i < _state.installer.initializationSequence.Count; i++)
            {
                IModuleInit module = (_state.installer.initializationSequence[i].instance as IModuleInit);
                module?.InitModule();
            }
        }


        // *****************************
        // InitRuntimeUpdateSequence
        // *****************************
        static void InitRuntimeUpdateSequence(State _state)
        {
            for (int i = 0; i < _state.updateSequence.Count; i++)
            {
                IModuleUpdate module = _state.updateSequence[i] as IModuleUpdate;

                bool castFailed = module == null;
                if (castFailed)
                {
                    Debug.LogWarning($"Module cannot be added since it does not implement IModuleUpdate or its NULL!");
                    continue;
                }

                CompAddOrRemoveToUpdateSequence.AddToUpdateInternal(_state, module);
            }
        }
    }
}
