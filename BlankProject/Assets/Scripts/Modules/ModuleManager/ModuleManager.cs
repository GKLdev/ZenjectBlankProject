using System;
using System.Collections;
using System.Collections.Generic;
using Modules.ModuleManager_Public;
using UnityEngine;
using Zenject;

namespace Modules.ModuleManager
{

    public class ModuleManager : LogicBase, IModuleManager
    {
        [SerializeField]
        private State state = new ();

        public DiContainer Container => state.installer.RelatedContainer;
        
        // *****************************
        // InitModule
        // *****************************
        public void InitModule()
        {
            CompInit.Init(state, this);
        }

        // *****************************
        // AddToUpdateSequence
        // *****************************
        public void AddToUpdateSequence(IModuleUpdate _entry)
        {
            CompAddOrRemoveToUpdateSequence.AddToUpdate(state, _entry);
        }

        // *****************************
        // RemoveFromUpdateSequence
        // *****************************
        public void RemoveFromUpdateSequence(IModuleUpdate _entry)
        {
            CompAddOrRemoveToUpdateSequence.RemoveFromUpdate(state, _entry);
        }

        // *****************************
        // Update
        // *****************************
        private void Update()
        {
            if (!state.dynamicData.isInitialised)
            {
                return;
            }
            
            state.dynamicData.updateSequenceRuntime.ForEach(x =>
            {
                x.OnUpdate();
            });
        }
    }

    // *****************************
    // State
    // *****************************
    [System.Serializable]
    public class State
    {
        [Header("Modules")] 
        public CompInstaller   installer;
        public SceneContext    context;
        public List<LogicBase> updateSequence;

        public DynamicData dynamicData = new DynamicData();

        public class DynamicData
        {
            public List<IModuleUpdate>  updateSequenceRuntime   = new ();
            public bool                 isInitialised           = false;
        }
        
        [System.Serializable]
        public class ModuleContainer
        {
            public LogicBase instance;
            public string bindName;
        }
    } 
}