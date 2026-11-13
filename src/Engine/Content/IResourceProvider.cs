using System;
using System.Collections.Generic;
using System.Text;
using Engine.Content;
using Engine.Content.Defaults;

namespace Engine.Content
{
    public interface IResourceProvider : IDisposable
    {
        DefaultResources Defaults { get; }
        internal void InitializeDefaults();
        T Load<T>(string path) where T : class;
        bool Exists(string path);
    }
}
