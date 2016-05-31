using System;
using System.Drawing;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public partial class AnimationControl : UserControl
    {
        private bool currentlyAnimating = false;

        public AnimationControl()
        {
            InitializeComponent();
        }

        public Bitmap AnimatedImage
        {
            get;
            set;
        }

        public void AnimateImage()
        {
            if (AnimatedImage != null && !currentlyAnimating)
            {
                //Begin the animation only once.
                ImageAnimator.Animate(AnimatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        public void StopAnimate()
        {
            currentlyAnimating = false;
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (AnimatedImage != null)
            {
                if (currentlyAnimating)
                {
                    //Begin the animation.
                    AnimateImage();

                    //Get the next frame ready for rendering.
                    ImageAnimator.UpdateFrames();

                    //Draw the next frame in the animation.
                    e.Graphics.DrawImage(AnimatedImage, new Point(0, 0));
                }
                else
                {
                    e.Graphics.DrawImage(AnimatedImage, new Point(0, 0));
                }
            }
        }
    }
}
