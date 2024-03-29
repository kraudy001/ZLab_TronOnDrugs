﻿using System;
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
        #region Variables
        private double lineWidth = 5;
        public static int r;
        public static int g;
        public static int b;

        Random rand = new Random();
        Size area;
        IGameLogic logic;
        #endregion

        #region Constructors
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

        public void RandomRGB()
        {
            g = rand.Next(0, 256);
            b = rand.Next(0, 256);
            r = rand.Next(0, 256);
        }
        #endregion

        #region Brushes
        public Brush BackgroundBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Background.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush StoneBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "stone.png"), UriKind.RelativeOrAbsolute)));
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
        public Brush SpeedAbilityBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "speed.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush WallAbilityBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "wall.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush GhostAbilityBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "ghost.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush RandomColor
        {
            get
            {
                return new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
            }
        }
        #endregion

        #region OnRender
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (logic != null && area.Width > 0 && area.Height > 0)
            {
                #region Draw Background
                drawingContext.DrawRectangle(BackgroundBrush, null, new Rect(0, 0, area.Width, area.Height));
                #endregion

                #region Walls
                GeometryGroup walls = new GeometryGroup();
                walls.Children.Add(new RectangleGeometry(new Rect(0, 0, area.Width, lineWidth)));
                walls.Children.Add(new RectangleGeometry(new Rect(0, 0, lineWidth, area.Height)));
                walls.Children.Add(new RectangleGeometry(new Rect(area.Width - lineWidth, 0, lineWidth, area.Height)));
                walls.Children.Add(new RectangleGeometry(new Rect(0, (area.Height - 100) - lineWidth, area.Width, lineWidth)));
                drawingContext.DrawGeometry(RandomColor, null, walls);
                #endregion

                #region Motor Properties
                foreach (var motor in logic.Motors)
                {
                    if (logic.Motors.Count == 1 && motor.name == "Player 1")
                    {
                        drawingContext.DrawRectangle(BlueMotorBrush, null, new Rect(area.Width / 2 - 100, area.Height - 100, 100, 100));
                        if (motor.special != null)
                        {
                            // ha felvette az abilityt
                            if (motor.special.Name == "Ghost")
                            {
                                drawingContext.DrawEllipse(GhostAbilityBrush, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                            }
                            else if (motor.special.Name == "Speed")
                            {
                                drawingContext.DrawEllipse(SpeedAbilityBrush, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                            }
                            else if (motor.special.Name == "Wall")
                            {
                                drawingContext.DrawEllipse(WallAbilityBrush, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                            }
                        }
                    }
                    else
                    {
                        //motor1
                        if (motor.name == "Player 1")
                        {
                            drawingContext.DrawRectangle(BlueMotorBrush, null, new Rect(area.Width - 205, area.Height - 100, 100, 100));
                        }
                        if (motor.name == "Player 1" && motor.special != null)
                        {
                            // ha felvette az abilityt
                            if (motor.name == "Player 1" && motor.special.Name == "Ghost")
                            {
                                drawingContext.DrawEllipse(GhostAbilityBrush, null, new Point(area.Width - 65, area.Height - 47.5), 25, 25);
                            }
                            else if (motor.name == "Player 1" && motor.special.Name == "Speed")
                            {
                                drawingContext.DrawEllipse(SpeedAbilityBrush, null, new Point(area.Width - 65, area.Height - 47.5), 25, 25);
                            }
                            else if (motor.name == "Player 1" && motor.special.Name == "Wall")
                            {
                                drawingContext.DrawEllipse(WallAbilityBrush, null, new Point(area.Width - 65, area.Height - 47.5), 25, 25);
                            }
                        }
                        //motor2
                        if (motor.name == "Player 2")
                        {
                            drawingContext.DrawRectangle(YellowMotorBrush, null, new Rect(5, area.Height - 100, 100, 100));
                        }
                        if (motor.name == "Player 2" && motor.special != null)
                        {
                            // ha felvette az abilityt
                            if (motor.name == "Player 2" && motor.special.Name == "Ghost")
                            {
                                drawingContext.DrawEllipse(GhostAbilityBrush, null, new Point(area.Width - (area.Width - 125), area.Height - 47.5), 25, 25);
                            }
                            else if (motor.name == "Player 2" && motor.special.Name == "Speed")
                            {
                                drawingContext.DrawEllipse(SpeedAbilityBrush, null, new Point(area.Width - (area.Width - 125), area.Height - 47.5), 25, 25);
                            }
                            else if (motor.name == "Player 2" && motor.special.Name == "Wall")
                            {
                                drawingContext.DrawEllipse(WallAbilityBrush, null, new Point(area.Width - (area.Width - 125), area.Height - 47.5), 25, 25);
                            }
                        }
                        //motor3
                        if (motor.name == "Player 3")
                        {
                            drawingContext.DrawRectangle(BuggieBrush, null, new Rect(area.Width / 2 - 100, area.Height - 100, 100, 100));
                        }
                        if (motor.name == "Player 3" && motor.special != null)
                        {
                            // ha felvette az abilityt
                            if (motor.name == "Player 3" && motor.special.Name == "Ghost")
                            {
                                drawingContext.DrawEllipse(GhostAbilityBrush, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                            }
                            else if (motor.name == "Player 3" && motor.special.Name == "Speed")
                            {
                                drawingContext.DrawEllipse(SpeedAbilityBrush, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                            }
                            else if (motor.name == "Player 3" && motor.special.Name == "Wall")
                            {
                                drawingContext.DrawEllipse(WallAbilityBrush, null, new Point(area.Width - (area.Width / 2 - 20), area.Height - 47.5), 25, 25);
                            }
                        }
                    }
                }
                #endregion

                #region Draw Motors
                foreach (var motor in logic.Motors)
                {
                    drawingContext.PushTransform(new RotateTransform(motor.Orientation, motor.Placement.X, motor.Placement.Y));
                    if (motor.name == "Player 1")
                    {
                        drawingContext.DrawGeometry(BlueMotorBrush, null, motor.Area);
                    }
                    else if (motor.name == "Player 2")
                    {
                        drawingContext.DrawGeometry(YellowMotorBrush, null, motor.Area);
                    }
                    else if (motor.name == "Player 3")
                    {
                        drawingContext.DrawGeometry(BuggieBrush, null, motor.Area);
                    }
                    drawingContext.Pop();
                }
                #endregion

                #region Draw Vectors
                // a Stones vektoranak a sorce erteke == "stone"
                foreach (var vector in logic.Vectors)
                {
                    if (vector.sorce == "stone")
                    {
                        drawingContext.DrawGeometry(StoneBrush, null, vector.Stones);
                    }
                    else
                    {
                        drawingContext.DrawGeometry(RandomColor, null, vector.Lines);
                    }
                }
                #endregion

                #region Draw Abilities
                foreach (var ability in logic.Abilities)
                {
                    if (ability.Name == "Ghost")
                    {
                        drawingContext.DrawGeometry(GhostAbilityBrush, null, ability.Ability);
                    }
                    else if (ability.Name == "Speed")
                    {
                        drawingContext.DrawGeometry(SpeedAbilityBrush, null, ability.Ability);
                    }
                    else if (ability.Name == "Wall")
                    {
                        drawingContext.DrawGeometry(WallAbilityBrush, null, ability.Ability);
                    }
                }
                #endregion
            }
        }
        #endregion
    }
}
