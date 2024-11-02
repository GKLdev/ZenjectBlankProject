using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.TestModule_Public
{
    public interface ITestModule : IModuleInit, IModuleUpdate
    {
        void TestMethod();
    }
}