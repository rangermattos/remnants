using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

// ReSharper disable once CheckNamespace
namespace Remnants
{
    public class ScalingViewportAdapter : ViewportAdapter
    {
        public ScalingViewportAdapter(GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight)
            : base(graphicsDevice)
        {
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;
        }

        public override int VirtualWidth { get; }
        public override int VirtualHeight { get; }
        public override int ViewportWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public override int ViewportHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public override Matrix GetScaleMatrix()
        {
            var scaleX = (float)ViewportWidth / VirtualWidth;
            var scaleY = (float)ViewportHeight / VirtualHeight;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}