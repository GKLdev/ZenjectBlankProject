using Modules.ModuleManager_Public;
using Modules.TestModule_Public;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Modules.TestModule
{
    public class TestModule : LogicBase, ITestModule
    {
        [Inject]
        IModuleManager moduleMng;

        [SerializeField]
        private State _state;

        // *****************************
        // InitModule
        // *****************************
        public void InitModule()
        {
            Debug.Log("Test module initialized!");
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

            if (_state.logUpdate)
            {
                Debug.Log("Test module Update!");
            }
        }

        // *****************************
        // TestMethod
        // *****************************
        public void TestMethod()
        {
            LibModuleExceptions.ExceptionIfNotInitialized(_state.dynamicData.initialised);
            Debug.Log("Test module Method!");
        }
    }

    [System.Serializable]
    class State
    {
        public bool logUpdate = false;

        public DynamicData dynamicData = new();


        // *****************************
        // DynamicData
        // *****************************
        public class DynamicData
        {
            public bool initialised = false; 
        }
    }
}