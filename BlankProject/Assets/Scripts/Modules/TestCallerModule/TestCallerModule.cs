using Modules.ModuleManager_Public;
using Modules.TestModule_Public;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Modules.TestCallerModule_Public
{
    public class TestCallerModule : LogicBase, ITestCallerModule
    {
        // simplest way of getting a dependency
        // DOWNSIDE: target MUST be binded when this script is being loaded
        // this could be ahievend by using Unity's 'Script Execution Order'
        [Inject]
        IModuleManager moduleMng;

        // In case you want to instantite prefabs AND want them to supoort Zenject attributes like [Inject] (they will not by default) use this:
        // ITestCallerModule instance   = container.InstantiatePrefabForComponent<ITestCallerModule>(prefab);

        [SerializeField]
        private State _state;

        // *****************************
        // InitModule
        // *****************************
        public void InitModule()
        {
            // getting dependecy through bind.
            // requires more code and 'Container' access, but completely under your control by default
            _state.dynamicData.target = moduleMng.Container.Resolve<ITestModule>();

            _state.dynamicData.initialised = true;
        }

        // *****************************
        // OnUpdate
        // *****************************
        public void OnUpdate()
        {
            if (!_state.dynamicData.initialised)
            {
                return;
            }

            if (_state.testCall && _state.dynamicData.target != null)
            {
                _state.dynamicData.target.TestMethod();
            }
        }


        [System.Serializable]
        class State
        {
            public bool testCall = false;

            public DynamicData dynamicData = new();


            // *****************************
            // DynamicData
            // *****************************
            public class DynamicData
            {
                public ITestModule target; 
                public bool initialised = false;
            }
        }
    }
}