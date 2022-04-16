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
        Random rand = new Random();
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

        public Brush BackgroundBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Background.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush BlueMotorBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "tron-blue.bmp"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush YellowMotorBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "tron-yellow.bmp"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush BuggieBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "tron-buggie.bmp"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush RandomColor
        {
            get
            {
                return new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 256), (byte)rand.Next(0, 256), (byte)rand.Next(0, 256)));
            }
        } 

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (logic != null && area.Width > 0 && area.Height > 0)
            {
                drawingContext.DrawRectangle(BackgroundBrush, null, new Rect(0, 0, area.Width, area.Height));

                foreach (var motor in logic.Motors)
                {
                    drawingContext.PushTransform(new RotateTransform(motor.Orientation, motor.Placement.X, motor.Placement.Y));
                    if (motor == logic.Motors[0])
                    {
                        drawingContext.DrawGeometry(BlueMotorBrush, null, motor.Area);
                    }
                    else if (motor == logic.Motors[1])
                    {
                        drawingContext.DrawGeometry(YellowMotorBrush, null, motor.Area);
                    }
                    else if (motor == logic.Motors[2])
                    {
                        drawingContext.DrawGeometry(BuggieBrush, null, motor.Area);
                    }
                    drawingContext.Pop();
                }

                // a motorok altal huzott vektoroknak a sorc erteke == "MotorVector"

                foreach (var vector in logic.Vectors)
                {
                    drawingContext.DrawGeometry(RandomColor, null, vector.Lines);
                    drawingContext.DrawGeometry(RandomColor, null, vector.Barriers);
                }
            }
        }
    }
}
