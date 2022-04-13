using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Z_Lab_TronOnDrugs_.Logic;

namespace Z_Lab_TronOnDrugs_.Renderer
{
    internal class Display : FrameworkElement
    {
        Size area;
        IGameLogic logic;
        public void SetupSizes(Size area)
        {
            this.area = area;
            this.InvalidateVisual();
        }

        public void SetupLogic(IGameLogic logic)
        {
            this.logic = logic;
            this.logic.Change += (sender, eventargs) => this.InvalidateVisual();
        }

        public Brush MotorBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "tron-blue.bmp"), UriKind.RelativeOrAbsolute)));
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (logic != null && area.Width > 0 && area.Height > 0)
            {
                drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, area.Width, area.Height));

                foreach (var item in logic.Motors)
                {
                    drawingContext.PushTransform(new RotateTransform(item.Orientation, item.Placement.X+25,item.Placement.Y+25));
                    drawingContext.DrawGeometry(MotorBrush, null, item.Area);
                    drawingContext.Pop();
                }

                // a motorok altal huzott vektoroknak a sorc erteke == "MotorVector"

                foreach (var item in logic.Vectors)
                {
                    drawingContext.DrawGeometry(Brushes.Red, null, item.Wall);
                    drawingContext.DrawGeometry(Brushes.SkyBlue, null, item.Lines);
                }
            }
        }
    }
}
