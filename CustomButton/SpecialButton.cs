using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomButton
{
    public class SpecialButton: Control
    {
        private enum StateButton
        {
            on,
            off
        }

        private static SpecialButton actualButton = null;
        private StateButton _state = StateButton.off; 
        private Image _backgroundImage = null;
        private bool _hovering = false;

        public new Image BackgroundImage
        {
            get { return _backgroundImage; }
            set {
                if (value != _backgroundImage)
                {
                    _backgroundImage = value;
                    Invalidate();
                }
            }
        }

        public Color HoverColor
        { get; set; }
        
        public Color PressedColor
        { get; set; }
        
        private bool Hovering
        {
            get { return _hovering; }
            set
            {
                if (value == _hovering)
                {
                    return;
                }
                _hovering = value;
                Invalidate();
            }
        }

        private StateButton State
        {
            get { return _state; }
            set
            {
                if (value == _state)
                {
                    return;
                }
                _state = value;
                Invalidate();
            }
        }

        public SpecialButton()
        {
            //InitializeComponent();
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Hovering = true;
        }       

        protected override void OnMouseLeave(EventArgs e)
        {
            Hovering = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (actualButton == null)
            {
                actualButton = this;
                State = StateButton.on;
            }
            else
            {
                if (actualButton != this)
                {
                    actualButton.State = StateButton.off;
                    actualButton = this;
                    State = StateButton.on;
                }
            }            
        }        

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics gfx = pe.Graphics;
            Rectangle rc = ClientRectangle;
            Rectangle rc2 = ClientRectangle;
            rc.Width -= 1;
            rc.Height -= 1;
            Color fill;
            if (State == StateButton.on)
            {
                fill = PressedColor;
            }
            else
            {
                if (Hovering)
                {
                    fill = HoverColor;
                }
                else
                {
                    fill = Color.Transparent;
                }
            }

            rc.X = 0;
            gfx.FillRectangle(new SolidBrush(fill), rc);
      
            rc2.X = 1;
            rc2.Y = 1;
            rc2.Width -= 2;
            rc2.Height -= 2;

            gfx.FillRectangle(new SolidBrush(Parent.BackColor), rc);
            if (BackgroundImage != null)
            {
                gfx.DrawImage(BackgroundImage, rc2, new Rectangle(0, 0, BackgroundImage.Width, BackgroundImage.Height), GraphicsUnit.Pixel);
            }
            gfx.DrawRectangle(new Pen(new SolidBrush(fill), 1), rc);
        }
    }
}
