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
        static public bool medium;
        static public bool hard;
        private double lineWidth = 5;
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

                #region Walls
                GeometryGroup walls = new GeometryGroup();
                walls.Children.Add(new RectangleGeometry(new Rect(0, 0, area.Width, lineWidth)));
                walls.Children.Add(new RectangleGeometry(new Rect(0, 0, lineWidth, area.Height)));
                walls.Children.Add(new RectangleGeometry(new Rect(area.Width - lineWidth, 0, lineWidth, area.Height)));
                walls.Children.Add(new RectangleGeometry(new Rect(0, (area.Height - 100) - lineWidth, area.Width, lineWidth)));
                drawingContext.DrawGeometry(RandomColor, null, walls);
                #endregion

                if (logic.Motors.Count == 1)
                {
                    //motor1
                    drawingContext.DrawRectangle(BlueMotorBrush, null, new Rect(5, area.Height - 100, 100, 100));
                    if (logic.Motors[0].special != null)
                    {
                        // ha felvette az abilityt
                        drawingContext.DrawEllipse(Brushes.White, null, new Point(area.Width - (area.Width - 125), area.Height - 47.5), 25, 25);
                    }
                }
                else if (logic.Motors.Count == 2)
                {
                    //motor1
                    drawingContext.DrawRectangle(BlueMotorBrush, null, new Rect(5, area.Height - 100, 100, 100));
                    if (logic.Motors[0].special != null)
                    {
                        // ha felvette az abilityt
                        drawingContext.DrawEllipse(Brushes.White, null, new Point(area.Width - (area.Width - 125), area.Height - 47.5), 25, 25);
                    }
                    //motor2
                    drawingContext.DrawRectangle(YellowMotorBrush, null, new Rect(area.Width / 2 - 100, area.Height - 100, 100, 100));
                    if (logic.Motors[1].special != null)
                    {
                        // ha felvette az abilityt
                        drawingContext.DrawEllipse(Brushes.White, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                    }
                }
                else if(logic.Motors.Count == 3)
                {
                    //motor1
                    drawingContext.DrawRectangle(BlueMotorBrush, null, new Rect(5, area.Height - 100, 100, 100));
                    if (logic.Motors[0].special != null)
                    {
                        // ha felvette az abilityt
                        drawingContext.DrawEllipse(Brushes.White, null, new Point(area.Width - (area.Width - 125), area.Height - 47.5), 25, 25);
                    }
                    //motor2
                    drawingContext.DrawRectangle(YellowMotorBrush, null, new Rect(area.Width / 2 - 100, area.Height - 100, 100, 100));
                    if (logic.Motors[1].special != null)
                    {
                        // ha felvette az abilityt
                        drawingContext.DrawEllipse(Brushes.White, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                    }
                    //motor3
                    drawingContext.DrawRectangle(BuggieBrush, null, new Rect(area.Width - 205, area.Height - 100, 100, 100));
                    if (logic.Motors[2].special != null)
                    {
                        // ha felvette az abilityt
                        drawingContext.DrawEllipse(Brushes.White, null, new Point(area.Width - 65, area.Height - 47.5), 25, 25);
                    }
                }
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
                    if (medium == true)
                    {
                        drawingContext.DrawGeometry(RandomColor, null, vector.MediumBarriers);
                    }
                    else if (hard == true)
                    {
                        drawingContext.DrawGeometry(RandomColor, null, vector.HardBarriers);
                    }
                }

                foreach (var ability in logic.Abilities)
                {
                    drawingContext.DrawGeometry(Brushes.Red, null, ability.Ability);
                }
            }
        }
    }
}
