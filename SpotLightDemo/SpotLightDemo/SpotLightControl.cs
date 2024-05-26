using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotLightDemo
{
    /// <summary>
    /// 走马灯控件
    /// </summary>
    public class SpotLightControl : Control
    {
        private Timer AnimationTimer
        {
            get; set;
        }
        public SpotLightControl()
        {
            this.Font = new System.Drawing.Font("Arial Black", 50F, System.Drawing.FontStyle.Bold);
            this.DoubleBuffered = true;
            this.Padding = new Padding(10);

            this.InitAnimationTimer();
        }

        private void InitAnimationTimer()
        {
            this.AnimationTimer = new Timer();
            this.AnimationTimer.Interval = 100;
            this.AnimationTimer.Tick += (s, e) =>
            {
                this.ChangeOffset();
            };
        }

        public int OffsetX
        {
            get; set;
        }
        private float TextWidth
        {
            get; set;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            float initX = this.Padding.Left;
            float initY = this.Padding.Top;
            var x = initX;
            var y = initY;
            var text = "WinFormDevelopers";

            // Step1：绘制灰色文字
            var grayBrush = new SolidBrush(ColorTranslator.FromHtml("gray"));
            g.DrawString(text, this.Font, grayBrush, x, y);

            //<GradientStop Color="#FF9C1031" Offset="0.1"/>
            //<GradientStop Color="#FFBE0E20" Offset="0.2"/>
            //<GradientStop Color="#FF9C12AC" Offset="0.7"/>
            //<GradientStop Color="#FF0A8DC3" Offset="0.8"/>
            //<GradientStop Color="#FF1AEBCC" Offset="1"/>

            var textSize = g.MeasureString(text, this.Font, 1000);
            var height = (int)textSize.Height + 1;
            var step = textSize.Width / 10F;
            this.TextWidth = textSize.Width;

            // 演示渐变绘制裁剪
            x = initX;
            y = 100;
            var width = (2 * step);

            {
                // 第一段
                var rect = new RectangleF(x, y, width, height);
                var colorFrom = "#FF9C1031";
                var colorTo = "#FFBE0E20";
                using (var linearGradientBrush = new LinearGradientBrush(rect, ColorTranslator.FromHtml(colorFrom), ColorTranslator.FromHtml(colorTo), LinearGradientMode.Horizontal))
                {
                    // 设置裁剪区域
                    g.SetClip(rect);
                    g.DrawString(text, this.Font, linearGradientBrush, initX, y);
                    x += width;
                }

                // 第二段不绘制
                width = (5 * step);
                x += width;

                // 第三段
                width = (1 * step);
                rect = new RectangleF(x, y, width, height);
                colorFrom = "#FF9C12AC";
                colorTo = "#FF0A8DC3";
                using (var linearGradientBrush = new LinearGradientBrush(rect, ColorTranslator.FromHtml(colorFrom), ColorTranslator.FromHtml(colorTo), LinearGradientMode.Horizontal))
                {
                    //// 不截取的效果
                    g.SetClip(rect);
                    g.DrawString(text, this.Font, linearGradientBrush, initX, y);
                    x += width;
                }
            }


            // Step2：绘制渐变色文字
            x = initX;
            y = 200;

            width = (2 * step);
            x = Draw(g, text, initX, x, y, width, height, "#FF9C1031", "#FFBE0E20");

            width = (5 * step);
            x = Draw(g, text, initX, x, y, width, height, "#FFBE0E20", "#FF9C12AC");

            width = (1 * step);
            x = Draw(g, text, initX, x, y, width, height, "#FF9C12AC", "#FF0A8DC3");

            width = (2 * step);
            x = Draw(g, text, initX, x, y, width, height, "#FF0A8DC3", "#FF1AEBCC");

            // Step3：绘制走马灯文字（需要实现Step1，Step2的功能，因为文字要移动因此还采用了交叉剪辑的功能）
            x = initX;
            y = 300;
            g.ResetClip();

            // 先绘制灰色文字底图
            g.DrawString(text, this.Font, grayBrush, x, y);

            // 再绘制渐变色文字
            width = (2 * step);
            var moveRect = new RectangleF(initX + OffsetX, y, 100, height);
            x = Draw(g, text, initX, x, y, width, height, "#FF9C1031", "#FFBE0E20", moveRect);

            width = (5 * step);
            x = Draw(g, text, initX, x, y, width, height, "#FFBE0E20", "#FF9C12AC", moveRect);

            width = (1 * step);
            x = Draw(g, text, initX, x, y, width, height, "#FF9C12AC", "#FF0A8DC3", moveRect);

            width = (2 * step);
            x = Draw(g, text, initX, x, y, width, height, "#FF0A8DC3", "#FF1AEBCC", moveRect);

            // 释放资源
            grayBrush.Dispose();
        }

        /// <summary>
        /// 绘制渐变色文字
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="initX"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="colorFrom"></param>
        /// <param name="colorTo"></param>
        /// <returns></returns>
        private float Draw(Graphics g, string text, float initX, float x, float y, float width, int height, string colorFrom, string colorTo, RectangleF moveRect = default)
        {
            var rect = new RectangleF(x, y, width, height);
            using (var linearGradientBrush = new LinearGradientBrush(rect, ColorTranslator.FromHtml(colorFrom), ColorTranslator.FromHtml(colorTo), LinearGradientMode.Horizontal))
            {
                if (moveRect == default)
                {
                    g.SetClip(rect);
                }
                else
                {
                    //绘制走马灯文字（交叉剪辑）
                    g.SetClip(moveRect);
                    g.SetClip(rect, CombineMode.Intersect);
                }
                g.DrawString(text, this.Font, linearGradientBrush, initX, y);
                x += width;
                return x;
            }
        }

        private bool IsLeftToRight
        {
            get; set;
        } = true;

        /// <summary>
        /// 通过OffsetX，在X上加上偏移量模拟移动效果
        /// 
        /// </summary>
        private void ChangeOffset()
        {
            if (this.OffsetX >= (this.TextWidth - 100))
            {
                this.IsLeftToRight = false;
            }
            else if (this.OffsetX <= 0)
            {
                this.IsLeftToRight = true;
            }

            if (this.IsLeftToRight)
            {
                this.OffsetX += 10;
            }
            else
            {
                this.OffsetX -= 10;
            }

            this.Invalidate();
        }

        /// <summary>
        /// 开始动画
        /// </summary>
        public void StartAnimation()
        {
            this.AnimationTimer.Start();
        }

        /// <summary>
        /// 停止动画
        /// </summary>
        public void StopAnimation()
        {
            this.AnimationTimer.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.AnimationTimer?.Stop();
                this.AnimationTimer?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
