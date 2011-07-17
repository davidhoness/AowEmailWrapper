using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AowEmailWrapper.Controls
{
    public class IconMenuItem : MenuItem
    {
        private Image _startImage;
        private Image _endImage;
        private bool _showEndImage;
        private Font _font;
        private const int _menuPaddingY = 2;
        private const int _menuPaddingX = 0;

        public bool ShowEndImage
        {
            get { return _showEndImage; }
            set { _showEndImage = value; }
        }

        public Image StartImage
        {
            get { return _startImage; }
            set { _startImage = value; }
        }

        public Image EndImage
        {
            get { return _endImage; }
            set { _endImage = value; }
        }

        public IconMenuItem(string text, Image startImage, Image endImage)
            : this(text, startImage, endImage, SystemInformation.MenuFont)
        { }

        public IconMenuItem(string text, Image startImage, Image endImage, Font font)
            : base(text)
        {
            _startImage = startImage;
            _endImage = endImage;
            _font = font;

            this.OwnerDraw = true;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {            
            using (e.Graphics)
            {
                base.OnDrawItem(e);

                SolidBrush menuBrush = null;

                if (!this.Enabled)
                {
                    menuBrush = new SolidBrush(SystemColors.GrayText);
                }
                else
                {
                    if ((e.State & DrawItemState.Selected) != 0)
                    {
                        // Text color when selected (highlighted)
                        menuBrush = new SolidBrush(SystemColors.HighlightText);
                    }
                    else
                    {
                        // Text color during normal drawing
                        menuBrush = new SolidBrush(SystemColors.MenuText);
                    }
                }

                if ((e.State & DrawItemState.Selected) != 0)
                {
                    // Selected color
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds);
                }

                GraphicsUnit pageUnits = e.Graphics.PageUnit;
                
                //Get bounds of Start Image
                RectangleF startImageBounds = _startImage.GetBounds(ref pageUnits);

                //Get bounds of End Image
                RectangleF endImageBounds = _endImage.GetBounds(ref pageUnits);

                //Get bounds of Text String
                StringFormat strfmt = new StringFormat();
                strfmt.Alignment = StringAlignment.Center;
                SizeF textSize = e.Graphics.MeasureString(this.Text, _font, 1000, strfmt);
                textSize.Height = (textSize.Height > startImageBounds.Height) ? textSize.Height : startImageBounds.Height;
                RectangleF textBounds = new RectangleF(e.Bounds.Location, textSize);

                //Set their locations relative to e.Bounds
                int startY = (e.Bounds.Height - (int)startImageBounds.Height) / 2;
                startY += e.Bounds.Y;

                startImageBounds.Location = new PointF(e.Bounds.X, startY);
                textBounds.Location = GetRelativeLocation(startImageBounds);
                endImageBounds.Location = GetRelativeLocation(textBounds, 2);

                //Draw everything
                e.Graphics.DrawImage(_startImage, startImageBounds.Location);
                e.Graphics.DrawString(this.Text, _font, menuBrush, textBounds, strfmt);
                if (_showEndImage)
                {
                    e.Graphics.DrawImage(_endImage, endImageBounds.Location);
                }
            }
        }

        private PointF GetRelativeLocation(RectangleF relativeTo)
        {
            return GetRelativeLocation(relativeTo, 0);
        }

        private PointF GetRelativeLocation(RectangleF relativeTo, int Xpad)
        {
            return new PointF((int)Math.Ceiling(relativeTo.X + relativeTo.Width) + Xpad, relativeTo.Y);
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            Font menuFont = _font;

            StringFormat strfmt = new StringFormat();

            SizeF sizef = e.Graphics.MeasureString(this.Text, menuFont, 1000, strfmt);

            e.ItemWidth = (int)Math.Ceiling(sizef.Width) + _startImage.Width + _menuPaddingX;

            if (_showEndImage)
            {
                e.ItemWidth += _endImage.Width;
            }

            int menuHeight = (int)Math.Ceiling(sizef.Height);
            int imageHeight = _startImage.Height;

            e.ItemHeight = ((menuHeight >= imageHeight) ? menuHeight : imageHeight) + _menuPaddingY;
        }
    }
}
