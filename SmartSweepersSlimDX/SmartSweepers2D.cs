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
    class SmartSweepers2D : SmartSweepers
    {
        private PathGeometry geometry;
        private SolidColorBrush brush;

        /// <summary>Disposes of object resources.</summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected override void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                brush.Dispose();
                geometry.Dispose();
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

            geometry = new PathGeometry(Context2D.RenderTarget.Factory);

            using (GeometrySink sink = geometry.Open())
            {
                PointF p0 = new PointF(0.50f * WindowWidth, 0.25f * WindowHeight);
                PointF p1 = new PointF(0.75f * WindowWidth, 0.75f * WindowHeight);
                PointF p2 = new PointF(0.25f * WindowWidth, 0.75f * WindowHeight);

                sink.BeginFigure(p0, FigureBegin.Filled);
                sink.AddLine(p1);
                sink.AddLine(p2);
                sink.EndFigure(FigureEnd.Closed);

                // Note that Close() and Dispose() are not equivalent like they are for
                // some other IDisposable() objects.
                sink.Close();
            }

            brush = new SolidColorBrush(Context2D.RenderTarget, new Color4(0.93f, 0.40f, 0.08f));
        }

        /// <summary>
        /// In a derived class, implements logic that should occur before all
        /// other rendering.
        /// </summary>
        protected override void OnRenderBegin()
        {
            Context2D.RenderTarget.BeginDraw();
            Context2D.RenderTarget.Transform = Matrix3x2.Identity;
            Context2D.RenderTarget.Clear(new Color4(0.3f, 0.3f, 0.3f));
        }

        /// <summary>In a derived class, implements logic to render the instance.</summary>
        protected override void OnRender()
        {
            Context2D.RenderTarget.FillGeometry(geometry, brush);
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