﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SlimDX;
using SlimDX.Direct2D;
using SmartSweepersSlimDX.Rendering;
using System.Diagnostics;

namespace SmartSweepersSlimDX
{
    internal class SmartSweepers2D : SmartSweepers
    {
        private SolidColorBrush brush;

        /// <summary>Disposes of object resources.</summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected override void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                brush.Dispose();
            }

            base.Dispose(disposeManagedResources);
        }

        /// <summary>In a derived class, implements logic to initialize the instance.</summary>
        protected override void OnInitialize()
        {
            DeviceSettings2D settings = new DeviceSettings2D
            {
                Width = WindowWidth,
                Height = WindowHeight
            };

            InitializeDevice(settings);

            brush = new SolidColorBrush(Context2D.RenderTarget, brushColor);
        }

        /// <summary>
        /// In a derived class, implements logic that should occur before all
        /// other rendering.
        /// </summary>
        protected override void OnRenderBegin()
        {
            brush = new SolidColorBrush(Context2D.RenderTarget, brushColor);

            if (!controller.FastRender())
            {
                controller.Update();
            }

            Context2D.RenderTarget.BeginDraw();
            Context2D.RenderTarget.Transform = Matrix3x2.Identity;
            Context2D.RenderTarget.Clear(new Color4(System.Drawing.Color.Black));
        }

        /// <summary>In a derived class, implements logic to render the instance.</summary>
        protected override void OnRender()
        {
            controller.Render();
        }

        /// <summary>
        /// In a derived class, implements logic that should occur after all
        /// other rendering.
        /// </summary>
        protected override void OnRenderEnd()
        {
            Context2D.RenderTarget.EndDraw();
        }
    }
}
