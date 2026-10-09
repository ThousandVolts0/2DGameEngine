using Engine.Graphics;
using Engine.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Silk.NET.Windowing;

namespace Engine.Core
{
    public class GameServices
    {
        public IGraphicsDevice Graphics => _provider.GetRequiredService<IGraphicsDevice>();
        public RenderPipeline RenderPipeline => _provider.GetRequiredService<RenderPipeline>();
        public WorldRegistry Worlds => _provider.GetRequiredService<WorldRegistry>();
        public IWindow Window => _provider.GetRequiredService<IWindow>();

        private IServiceProvider _provider;

        public GameServices(IServiceProvider provider)
        {
            _provider = provider;
        }
    }
}
